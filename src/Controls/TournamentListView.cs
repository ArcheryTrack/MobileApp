using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class TournamentListView : AbstractListView
    {
        private List<Tournament> m_Tournaments;

        public TournamentListView ()
        {
            ItemTemplate = new DataTemplate (typeof (TournamentHistoryCell));
            RowHeight = 60;
        }

        public void RefreshList ()
        {
            ATManager manager = ATManager.GetInstance ();
            m_Tournaments = manager.GetTournaments ();
            ItemsSource = m_Tournaments;
        }

        public void ClearList ()
        {
            ItemsSource = null;
        }
    }
}

