using System;
using System.Collections.Generic;
using ATMobile.Constants;
using ATMobile.Data;
using ATMobile.Objects;

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
            PopulateTargetFaces ();
        }

        private void PopulateLocations ()
        {

        }

        private void PopulateTargetFaces ()
        {
            bool loaded = m_Manager.SettingManager.GetTargetFaceDataLoaded ();
            if (!loaded) {
                List<TargetFace> ranges = TargetFaceData.GetData ();

                foreach (var item in ranges) {
                    m_Manager.Persist (item);
                }
            }

            m_Manager.SettingManager.SetTargetFaceDataLoaded (true);
        }
    }
}

