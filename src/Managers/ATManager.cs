using System;
using System.IO;
using ATMobile.Objects;
using ATMobile.Helpers;
using System.Collections.Generic;
using ATMobile.Daos;
using LiteDB;
using System.Linq;
using System.Threading.Tasks;
using ATMobile.Interfaces;
using ATMobile.Constants;

namespace ATMobile.Managers
{
    public class ATManager : IDisposable
    {
        private static ATManager m_Instance;

        private string m_DataFolder;
        private string m_DatabaseFile;
        private LiteDatabase m_Database;
        private ChartingManager m_ChartingManager;
        private PluginManager m_PluginManager;
        private MessagingManager m_MessagingManager;
        private SettingManager m_SettingManager;
        private MenuManager m_MenuManager;

        private ATManager ()
        {
            m_DataFolder = App.DataFolder;
            m_DatabaseFile = Path.Combine (m_DataFolder, "ATMobile.db");
            m_Database = new LiteDatabase (m_DatabaseFile);

            BuildIndexes (m_Database);
        }

        public LiteDatabase Database {
            get {
                return m_Database;
            }
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

        public PluginManager PluginManager {
            get {
                if (m_PluginManager == null) {
                    m_PluginManager = new PluginManager (this);
                }

                return m_PluginManager;
            }
        }

        public MessagingManager MessagingManager {
            get {
                if (m_MessagingManager == null) {
                    m_MessagingManager = new MessagingManager (this);
                }

                return m_MessagingManager;
            }
        }

        public SettingManager SettingManager {
            get {
                if (m_SettingManager == null) {
                    m_SettingManager = new SettingManager (this);
                }

                return m_SettingManager;
            }
        }

        public MenuManager MenuManager {
            get {
                if (m_MenuManager == null) {
                    m_MenuManager = new MenuManager (this);
                }

                return m_MenuManager;
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

        public List<Archer> GetArchers (List<Guid> archerIds)
        {
            List<Archer> output = new List<Archer> ();

            foreach (var archerId in archerIds) {
                Archer archer = GetArcher (archerId);
                output.Add (archer);
            }

            return output;
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

        public void DeleteChartEntry (Guid _id)
        {
            ChartEntryDao dao = new ChartEntryDao (m_Database);
            dao.Delete (_id);
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

        #region Matches

        public void Persist (Match _match)
        {
            MatchDao dao = new MatchDao (m_Database);
            dao.Persist (_match);
        }

        public List<Match> GetMatches (Guid _roundId)
        {
            MatchDao dao = new MatchDao (m_Database);
            return dao.GetMatches (_roundId);
        }

        public void DeleteMatch (Guid _matchId)
        {
            MatchDao dao = new MatchDao (m_Database);
            dao.Delete (_matchId);
            DeleteChartEntry (_matchId);
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

        public void DeletePracticeEnd (Guid _practiceEndId)
        {
            PracticeEndDao dao = new PracticeEndDao (m_Database);
            dao.Delete (_practiceEndId);
            DeleteChartEntry (_practiceEndId);
        }

        public void RenumberPracticeEnds (Guid _practiceId)
        {
            List<PracticeEnd> ends = GetPracticeEnds (_practiceId);

            int i = 1;

            foreach (var end in ends) {
                end.EndNumber = i;
                Persist (end);

                i++;
            }
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

        public List<Practice> GetRecentPractices (DateTime start, DateTime end)
        {
            PracticeDao dao = new PracticeDao (m_Database);
            return dao.GetPractices (start, end);
        }

        public void Persist (Practice _practice)
        {
            PracticeDao dao = new PracticeDao (m_Database);
            dao.Persist (_practice);
        }

        /// <summary>
        /// Deletes the practice and cascade deletes practice ends.
        /// </summary>
        /// <param name="_practiceId">Practice identifier.</param>
        public void DeletePractice (Guid _practiceId)
        {
            List<PracticeEnd> ends = GetPracticeEnds (_practiceId);

            foreach (var end in ends) {
                DeletePracticeEnd (end.Id);
            }

            PracticeDao dao = new PracticeDao (m_Database);
            dao.Delete (_practiceId);
            DeleteChartEntry (_practiceId);
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

            List<Tournament> tournaments = GetRecentTournaments (start, end);
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

        /// <summary>
        /// Deletes the round.  Cascade Deletes Ends and Matches
        /// </summary>
        /// <param name="_roundId">Round identifier.</param>
        public void DeleteRound (Guid _roundId)
        {
            RoundDao roundDao = new RoundDao (m_Database);

            List<Match> matches = GetMatches (_roundId);
            foreach (var match in matches) {
                DeleteMatch (match.Id);
            }

            List<TournamentEnd> ends = GetTournamentEnds (_roundId);
            foreach (var end in ends) {
                DeleteTournamentEnd (end.Id);
            }

            roundDao.Delete (_roundId);
            DeleteChartEntry (_roundId);
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

        public List<Tournament> GetRecentTournaments (DateTime start, DateTime end)
        {
            TournamentDao dao = new TournamentDao (m_Database);
            return dao.GetRecentTournaments (start, end);
        }

        public void Persist (Tournament _tournament)
        {
            TournamentDao dao = new TournamentDao (m_Database);
            dao.Persist (_tournament);
        }

        /// <summary>
        /// Deletes the tournament.  Cascade Deletes Rounds, Matches and ends
        /// </summary>
        /// <param name="_tournamentId">Tournament identifier.</param>
        public void DeleteTournament (Guid _tournamentId)
        {
            List<Round> rounds = GetRounds (_tournamentId);

            foreach (var round in rounds) {
                DeleteRound (round.Id);
            }

            TournamentDao dao = new TournamentDao (m_Database);
            dao.Delete (_tournamentId);
            DeleteChartEntry (_tournamentId);
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

        public List<TournamentEnd> GetTournamentEnds (Guid _roundId)
        {
            TournamentEndDao dao = new TournamentEndDao (m_Database);
            return dao.GetTournamentEnds (_roundId);
        }

        public void Persist (TournamentEnd _tournamentEnd)
        {
            TournamentEndDao dao = new TournamentEndDao (m_Database);
            dao.Persist (_tournamentEnd);
        }

        public void DeleteTournamentEnd (Guid _id)
        {
            TournamentEndDao dao = new TournamentEndDao (m_Database);
            dao.Delete (_id);
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

