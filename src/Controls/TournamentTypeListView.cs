using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class TournamentTypeListView : AbstractListView
    {
        private List<TournamentType> m_Tournaments;

        public TournamentTypeListView ()
        {
            ItemTemplate = new DataTemplate (typeof (TournamentTypeCell));
            RowHeight = 60;
        }

        public void RefreshList ()
        {
            ATManager manager = ATManager.GetInstance ();
            m_Tournaments = manager.GetTournamentTypes ();
            ItemsSource = m_Tournaments;
        }

        public void ClearList ()
        {
            ItemsSource = null;
        }
    }
}

