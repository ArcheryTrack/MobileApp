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
            return dao.GetAll ();
        }

        public void Persist (Archer archer)
        {
            ArcherDao dao = new ArcherDao (m_Database);

            dao.Persist (archer);
        }


        #endregion


        #region Sight Settings

        public List<SightSetting> GetSightSettings (Guid _archerGuid)
        {
            SightSettingDao dao = new SightSettingDao (m_Database);
            return dao.GetSightSettings (_archerGuid);
        }

        #endregion

    }
}

