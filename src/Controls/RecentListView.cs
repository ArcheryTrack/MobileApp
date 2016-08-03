using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class RecentListView : AbstractListView
    {
        private List<RecentItem> m_RecentItems;

        public RecentListView ()
        {
            ItemTemplate = new DataTemplate (typeof (RecentCell));
            //RowHeight = 60;
            HasUnevenRows = true;
        }

        public void RefreshList ()
        {
            ATManager manager = ATManager.GetInstance ();
            m_RecentItems = manager.GetRecentItems ();
            ItemsSource = m_RecentItems;
        }

        public void ClearList ()
        {
            ItemsSource = null;
        }
    }
}

