using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class SightSettingListView : AbstractListView
    {
        private List<SightSetting> m_SightSettings;

        public SightSettingListView ()
        {
            ItemTemplate = new DataTemplate (typeof (SightSettingCell));
            //RowHeight = 60;
            HasUnevenRows = true;
        }

        public void RefreshList (Guid _archerGuid)
        {
            ATManager manager = ATManager.GetInstance ();
            m_SightSettings = manager.GetSightSettings (_archerGuid);
            ItemsSource = m_SightSettings;
        }

        public void ClearList ()
        {
            ItemsSource = null;
        }
    }
}

