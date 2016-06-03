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
		private string m_DataFolder;
		private string m_DatabaseFile;
		private LiteDatabase m_Database;

		public ATManager(string _dataFolder)
		{
			m_DataFolder = _dataFolder;
			m_DatabaseFile = Path.Combine(m_DataFolder, "ATMobile.db");
			m_Database = new LiteDatabase(m_DatabaseFile);
		}

		public List<SightSetting> GetSiteSettings() 
		{
			SightSettingDao dao = new SightSettingDao(m_Database);
			return dao.GetAll();
		}
	}
}

