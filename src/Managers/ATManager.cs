﻿using System;
using System.IO;
using ATMobile.Objects;
using System.Collections.Generic;
using ATMobile.Daos;
using LiteDB;


namespace ATMobile.Managers
{
    public class ATManager
    {
        private static ATManager m_Instance;

        private string m_DataFolder;
        private string m_DatabaseFile;
        private LiteDatabase m_Database;

        private ATManager ()
        {
            m_DataFolder = App.DataFolder;
            m_DatabaseFile = Path.Combine (m_DataFolder, "ATMobile.db");
            m_Database = new LiteDatabase (m_DatabaseFile);
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

        public void Persist (Archer archer)
        {
            ArcherDao dao = new ArcherDao (m_Database);

            dao.Persist (archer);
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
        }

        #endregion

        #region PracticeHistory

        public List<Practice> GetPractices (Guid _archerId)
        {
            PracticeDao dao = new PracticeDao (m_Database);
            return dao.GetPractices (_archerId);
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

        #region Rounds

        public List<Round> GetRounds (Guid _tournamentId)
        {
            RoundDao dao = new RoundDao (m_Database);
            return dao.GetRounds (_tournamentId);
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


        public List<Tournament> GetTournaments ()
        {
            TournamentDao dao = new TournamentDao (m_Database);
            return dao.GetAll ();
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

    }
}

