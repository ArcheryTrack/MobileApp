using System;
using System.Collections.Generic;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class SightSettingListView : ListView
    {
        private List<SightSetting> m_SightSettings;

        public SightSettingListView ()
        {
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.Transparent;

            var cell = new DataTemplate (typeof (ImageCell));
            cell.SetBinding (TextCell.TextProperty, "FullName");

            ItemTemplate = cell;
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

