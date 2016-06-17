using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class PracticeHistoryListView : ListView
    {
        private List<Practice> m_Practices;

        public PracticeHistoryListView ()
        {
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.Transparent;

            var cell = new DataTemplate (typeof (PracticeHistoryCell));
            ItemTemplate = cell;

            RowHeight = 60;
        }

        public void RefreshList (Guid _archerGuid)
        {
            ATManager manager = ATManager.GetInstance ();
            m_Practices = manager.GetPractices (_archerGuid);
            ItemsSource = m_Practices;
        }

        public void ClearList ()
        {
            ItemsSource = null;
        }
    }
}

