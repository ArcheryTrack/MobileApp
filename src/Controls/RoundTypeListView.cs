using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class RoundTypeListView : AbstractListView
    {
        private List<RoundType> m_RoundTypes;

        public RoundTypeListView ()
        {
            ItemTemplate = new DataTemplate (typeof (RoundTypeCell));
            RowHeight = 60;
        }

        public void RefreshList (Guid _tournamentTypeId)
        {
            ATManager manager = ATManager.GetInstance ();
            m_RoundTypes = manager.GetRoundTypes (_tournamentTypeId);
            ItemsSource = m_RoundTypes;
        }

        public void ClearList ()
        {
            ItemsSource = null;
        }
    }
}

