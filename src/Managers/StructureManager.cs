using System;
using ATMobile.Daos;
using LiteDB;

namespace ATMobile.Managers
{
    public class StructureManager : IDisposable
    {
        private LiteDatabase m_Database;

        public StructureManager (LiteDatabase _database)
        {
            m_Database = _database;
        }

        public void BuildStructure ()
        {
            ArcherDao archerDao = new ArcherDao (m_Database);
            archerDao.BuildIndexes ();

            ChartEntryDao chartEntryDao = new ChartEntryDao (m_Database);
            chartEntryDao.BuildIndexes ();

            CountryDao countryDao = new CountryDao (m_Database);
            countryDao.BuildIndexes ();

            JournalEntryDao journalEntryDao = new JournalEntryDao (m_Database);
            journalEntryDao.BuildIndexes ();

            PracticeDao practiceDao = new PracticeDao (m_Database);
            practiceDao.BuildIndexes ();

            PracticeEndDao practiceEndDao = new PracticeEndDao (m_Database);
            practiceEndDao.BuildIndexes ();

            PracticeTypeDao practiceTypeDao = new PracticeTypeDao (m_Database);
            practiceTypeDao.BuildIndexes ();

            QueueEntryDao queueEntryDao = new QueueEntryDao (m_Database);
            queueEntryDao.BuildIndexes ();

            RangeDao rangeDao = new RangeDao (m_Database);
            rangeDao.BuildIndexes ();

            RoundDao roundDao = new RoundDao (m_Database);
            roundDao.BuildIndexes ();

            RoundTypeDao roundTypeDao = new RoundTypeDao (m_Database);
            roundTypeDao.BuildIndexes ();

            SettingDao settingDao = new SettingDao (m_Database);
            settingDao.BuildIndexes ();

            SightSettingDao sightSettingDao = new SightSettingDao (m_Database);
            sightSettingDao.BuildIndexes ();

            StateDao stateDao = new StateDao (m_Database);
            stateDao.BuildIndexes ();

            TargetFaceDao targetFaceDao = new TargetFaceDao (m_Database);
            targetFaceDao.BuildIndexes ();

            TournamentDao tournamentDao = new TournamentDao (m_Database);
            tournamentDao.BuildIndexes ();

            TournamentEndDao tournamentEndDao = new TournamentEndDao (m_Database);
            tournamentEndDao.BuildIndexes ();

            TournamentTypeDao tournamentTypeDao = new TournamentTypeDao (m_Database);
            tournamentTypeDao.BuildIndexes ();
        }

        public void Dispose ()
        {
            m_Database = null;
        }
    }
}

