using System;
using System.IO;
using ATMobile.Objects;
using ATMobile.Helpers;
using System.Collections.Generic;
using ATMobile.Daos;
using LiteDB;
using System.Linq;
using System.Threading.Tasks;

namespace ATMobile.Managers
{
    public class ATManager : IDisposable
    {
        private static ATManager m_Instance;

        private string m_DataFolder;
        private string m_DatabaseFile;
        private LiteDatabase m_Database;
        private ChartingManager m_ChartingManager;

        private ATManager ()
        {
            m_DataFolder = App.DataFolder;
            m_DatabaseFile = Path.Combine (m_DataFolder, "ATMobile.db");
            m_Database = new LiteDatabase (m_DatabaseFile);

            BuildIndexes (m_Database);
        }

        public void BuildIndexes (LiteDatabase _database)
        {
            StructureManager sm = new StructureManager (_database);

            Task.Run (() => { sm.BuildStructure (); });
        }

        public ChartingManager ChartingManager {
            get {
                if (m_ChartingManager == null) {
                    m_ChartingManager = new ChartingManager (this);
                }

                return m_ChartingManager;
            }
        }

        public static ATManager GetInstance ()
        {
            if (m_Instance == null) {
                m_Instance = new ATManager ();
            }

            return m_Instance;
        }

        #region Archers

        public Archer GetArcher (Guid _archerGuid)
        {
            ArcherDao dao = new ArcherDao (m_Database);
            return dao.Get (_archerGuid);
        }

        public List<Archer> GetArchers ()
        {
            ArcherDao dao = new ArcherDao (m_Database);
            return dao.GetArchers ();
        }

        public string GetArcherName (Guid archerId)
        {
            Archer archer = GetArcher (archerId);

            if (archer != null) {
                return archer.FullName;
            }

            return null;
        }

        public List<string> GetArcherNames (List<Guid> archerIds)
        {
            List<string> output = new List<string> ();

            foreach (var archerId in archerIds) {
                Archer archer = GetArcher (archerId);
                if (archer != null) {
                    output.Add (archer.FullName);
                }
            }

            return output;
        }

        public void Persist (Archer archer)
        {
            ArcherDao dao = new ArcherDao (m_Database);

            dao.Persist (archer);
        }

        #endregion

        #region ChartEntries

        public ChartEntry GetChartEntry (Guid _id)
        {
            ChartEntryDao dao = new ChartEntryDao (m_Database);
            return dao.Get (_id);
        }

        public void Persist (ChartEntry _chartEntry)
        {
            ChartEntryDao dao = new ChartEntryDao (m_Database);
            dao.Persist (_chartEntry);
        }

        public List<ChartEntry> GetChartEntriesForPractice (Guid _practiceId, Guid _archerId)
        {
            ChartEntryDao dao = new ChartEntryDao (m_Database);
            return dao.GetChartEntriesForEnds (_practiceId, _archerId);
        }

        public List<ChartEntry> GetChartEntriesForRound (Guid _roundId, Guid _archerId)
        {
            ChartEntryDao dao = new ChartEntryDao (m_Database);
            return dao.GetChartEntriesForEnds (_roundId, _archerId);
        }


        #endregion

        #region Countries

        public List<Country> GetCountries ()
        {
            CountryDao dao = new CountryDao (m_Database);
            return dao.GetCountries ();
        }

        public void Persist (Country _country)
        {
            CountryDao dao = new CountryDao (m_Database);
            dao.Persist (_country);
        }

        #endregion

        #region JournalEntry

        public void Persist (JournalEntry _entry)
        {
            JournalEntryDao dao = new JournalEntryDao (m_Database);
            dao.Persist (_entry);
        }

        public JournalEntry GetJournalEntry (Guid _entryId)
        {
            JournalEntryDao dao = new JournalEntryDao (m_Database);
            return dao.Get (_entryId);
        }

        public List<JournalEntry> GetJournalEntries (Guid _archerId)
        {
            JournalEntryDao dao = new JournalEntryDao (m_Database);
            return dao.GetJournalEntries (_archerId);
        }

        public List<JournalEntry> GetJournalEntries (Guid _archerId, int start, int count)
        {
            JournalEntryDao dao = new JournalEntryDao (m_Database);
            return dao.GetJournalEntries (_archerId, start, count);
        }

        #endregion

        #region PracticeEnds

        public List<PracticeEnd> GetPracticeEnds (Guid _practiceId)
        {
            PracticeEndDao dao = new PracticeEndDao (m_Database);
            return dao.GetPracticeEnds (_practiceId);
        }

        public void Persist (PracticeEnd _end)
        {
            PracticeEndDao dao = new PracticeEndDao (m_Database);
            dao.Persist (_end);

            ChartingManager.ProcessEnd (_end);
        }

        #endregion

        #region PracticeHistory

        public Practice GetPractice (Guid _practiceId)
        {
            PracticeDao dao = new PracticeDao (m_Database);
            return dao.Get (_practiceId);
        }

        public List<Practice> GetPractices (Guid _archerId)
        {
            PracticeDao dao = new PracticeDao (m_Database);
            return dao.GetPractices (_archerId);
        }

        public List<Practice> GetPractices (DateTime start, DateTime end)
        {
            PracticeDao dao = new PracticeDao (m_Database);
            return dao.GetPractices (start, end);
        }

        public void Persist (Practice _practice)
        {
            PracticeDao dao = new PracticeDao (m_Database);
            dao.Persist (_practice);
        }

        #endregion

        #region Ranges

        public List<Range> GetRanges ()
        {
            RangeDao dao = new RangeDao (m_Database);
            return dao.GetRanges ();
        }

        public Range GetRange (Guid rangeId)
        {
            RangeDao dao = new RangeDao (m_Database);
            return dao.Get (rangeId);
        }

        public void Persist (Range _range)
        {
            RangeDao dao = new RangeDao (m_Database);
            dao.Persist (_range);
        }

        #endregion

        #region Recent

        public List<RecentItem> GetRecentItems ()
        {
            DateTime end = DateTime.Now.Date.AddDays (1);
            DateTime start = DateTime.Now.Date.AddDays (-30);

            List<RecentItem> items = new List<RecentItem> ();

            List<Tournament> tournaments = GetTournaments (start, end);
            foreach (var tournament in tournaments) {
                RecentItem newItem = new RecentItem {
                    ItemType = "Tournament",
                    Id = tournament.Id
                };

                List<string> archers = GetArcherNames (tournament.Archers);
                newItem.Archers = string.Join (", ", archers);
                newItem.ArcherCount = archers.Count;
                newItem.Date = tournament.StartDateTime;

                items.Add (newItem);
            }

            List<Practice> practices = GetPractices (start, end);
            foreach (var practice in practices) {

                RecentItem newItem = new RecentItem {
                    ItemType = "Practice",
                    Id = practice.Id,
                    Archers = GetArcherName (practice.ParentId),
                    Date = practice.DateTime,
                    ArcherCount = 1
                };

                items.Add (newItem);
            }

            List<RecentItem> sorted = items.OrderByDescending (x => x.Date).ToList ();
            return sorted;
        }

        #endregion

        #region Rounds

        public List<Round> GetRounds (Guid _tournamentId)
        {
            RoundDao dao = new RoundDao (m_Database);
            return dao.GetRounds (_tournamentId);
        }

        public Round GetRound (Guid _roundId)
        {
            RoundDao dao = new RoundDao (m_Database);
            return dao.Get (_roundId);
        }

        public void Persist (Round _round)
        {
            RoundDao dao = new RoundDao (m_Database);
            dao.Persist (_round);
        }

        #endregion 

        #region RoundTypes

        public List<RoundType> GetRoundTypes (Guid _tournamentTypeId)
        {
            RoundTypeDao dao = new RoundTypeDao (m_Database);
            return dao.GetRoundTypes (_tournamentTypeId);
        }

        public RoundType GetRoundType (Guid _roundTypeId)
        {
            RoundTypeDao dao = new RoundTypeDao (m_Database);
            return dao.Get (_roundTypeId);
        }

        public void Persist (RoundType roundType)
        {
            RoundTypeDao dao = new RoundTypeDao (m_Database);
            dao.Persist (roundType);
        }

        #endregion

        #region Sight Settings

        public List<SightSetting> GetSightSettings (Guid _archerGuid)
        {
            SightSettingDao dao = new SightSettingDao (m_Database);
            var settings = dao.GetSightSettings (_archerGuid);
            return settings;
        }

        public void Persist (SightSetting _sightSetting)
        {
            SightSettingDao dao = new SightSettingDao (m_Database);
            dao.Persist (_sightSetting);
        }

        #endregion

        #region Setting

        public List<Setting> GetSettings ()
        {
            SettingDao dao = new SettingDao (m_Database);
            return dao.GetAll ();
        }

        public void Persist (Setting _setting)
        {
            SettingDao dao = new SettingDao (m_Database);
            dao.Persist (_setting);
        }

        public Setting GetSetting (string _name)
        {
            SettingDao dao = new SettingDao (m_Database);
            return dao.GetSetting (_name);
        }

        public string GetSettingValue (string _name)
        {
            SettingDao dao = new SettingDao (m_Database);
            Setting setting = dao.GetSetting (_name);

            if (setting != null) {
                return setting.Value;
            }

            return null;
        }

        public void SetSetting (string _name, string _value)
        {
            SettingDao dao = new SettingDao (m_Database);
            Setting setting = dao.GetSetting (_name);

            if (setting == null) {
                setting = new Setting ();
                setting.Name = _name;
            }

            setting.Value = _value;
            dao.Persist (setting);
        }

        public void SetSetting (string _name, Guid _value)
        {
            SetSetting (_name, _value.ToString ());
        }

        public void SetSetting (string _name, bool _value)
        {
            SetSetting (_name, _value.ToString ());
        }

        public bool GetBoolSetting (string _name)
        {
            Setting setting = GetSetting (_name);

            if (setting == null) return false;

            bool output;
            if (bool.TryParse (setting.Value, out output)) {
                return output;
            }

            return false;
        }

        public Guid? GetGuidSetting (string _name)
        {
            Setting setting = GetSetting (_name);

            if (setting == null) return null;

            Guid output;
            if (Guid.TryParse (setting.Value, out output)) {
                return output;
            }

            return null;
        }

        #endregion

        #region States

        public List<State> GetStates (Guid _countryId)
        {
            StateDao dao = new StateDao (m_Database);
            return dao.GetStates (_countryId);
        }

        public void Persist (State _state)
        {
            StateDao dao = new StateDao (m_Database);
            dao.Persist (_state);
        }

        #endregion

        #region TargetFaces

        public List<TargetFace> GetTargetFaces ()
        {
            TargetFaceDao dao = new TargetFaceDao (m_Database);
            return dao.GetTargetFaces ();
        }

        public TargetFace GetTargetFace (Guid _targetFaceId)
        {
            TargetFaceDao dao = new TargetFaceDao (m_Database);
            return dao.Get (_targetFaceId);
        }

        public void Persist (TargetFace _targetFace)
        {
            TargetFaceDao dao = new TargetFaceDao (m_Database);
            dao.Persist (_targetFace);
        }

        #endregion

        #region Tournament

        public Tournament GetTournament (Guid tournamentId)
        {
            TournamentDao dao = new TournamentDao (m_Database);
            return dao.Get (tournamentId);
        }

        public List<Tournament> GetTournaments ()
        {
            TournamentDao dao = new TournamentDao (m_Database);
            return dao.GetAll ();
        }

        public List<Tournament> GetTournaments (DateTime start, DateTime end)
        {
            TournamentDao dao = new TournamentDao (m_Database);
            return dao.GetTournaments (start, end);
        }

        public void Persist (Tournament _tournament)
        {
            TournamentDao dao = new TournamentDao (m_Database);
            dao.Persist (_tournament);
        }

        #endregion

        #region TournamentEnds

        public TournamentEnd GetTournamentEnd (Guid _roundId, Guid _archerId, int _endNumber)
        {
            TournamentEndDao dao = new TournamentEndDao (m_Database);
            return dao.GetTournamentEnd (_roundId, _archerId, _endNumber);
        }

        public List<TournamentEnd> GetTournamentEnds (Guid _roundId, Guid _archerId)
        {
            TournamentEndDao dao = new TournamentEndDao (m_Database);
            return dao.GetTournamentEnds (_roundId, _archerId);
        }

        public void Persist (TournamentEnd _tournamentEnd)
        {
            TournamentEndDao dao = new TournamentEndDao (m_Database);
            dao.Persist (_tournamentEnd);
        }

        #endregion

        #region TournamentTypes

        public List<TournamentType> GetTournamentTypes ()
        {
            TournamentTypeDao dao = new TournamentTypeDao (m_Database);
            return dao.GetAll ();
        }

        public void Persist (TournamentType _tournamentType)
        {
            TournamentTypeDao dao = new TournamentTypeDao (m_Database);
            dao.Persist (_tournamentType);
        }

        public TournamentType GetTournamentType (Guid _tournamentTypeId)
        {
            TournamentTypeDao dao = new TournamentTypeDao (m_Database);
            return dao.Get (_tournamentTypeId);
        }


        #endregion

        #region Logging

        public void LogException (Exception ex)
        {
            //TODO
        }

        public void LogWarning (string message, params string [] values)
        {
            //TODO
        }

        public void LogInfo (string message, params string [] values)
        {
            //TODO
        }

        public void LogUserAction (string form, string button, string info = null)
        {
            //TODO
        }

        #endregion


        public void Dispose ()
        {
            m_ChartingManager.Dispose ();
            m_Instance = null;
        }
    }
}

