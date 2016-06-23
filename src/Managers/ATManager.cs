using System;
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

        public void Persist (Range _range)
        {
            RangeDao dao = new RangeDao (m_Database);
            dao.Persist (_range);
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

    }
}

