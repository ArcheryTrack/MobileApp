using System;
namespace ATMobile.Managers
{
    public class InitialDataManager
    {
        private static ATManager m_Manager;

        public InitialDataManager (ATManager _manager)
        {
            m_Manager = _manager;
        }

        public void PopulateData ()
        {
            PopulateLocations ();
        }

        private void PopulateLocations ()
        {

        }
    }
}

