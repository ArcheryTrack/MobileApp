using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class TournamentTypeRoundsListView : AbstractListView
    {
        private List<RoundType> m_Rounds;

        public TournamentTypeRoundsListView ()
        {
            ItemTemplate = new DataTemplate (typeof (TournamentTypeRoundCell));
            RowHeight = 60;

            RefreshList ();
        }

        public void RefreshList ()
        {
            ATManager manager = ATManager.GetInstance ();
            //m_Tournaments = manager.GetTournamentTypes ();
            //ItemsSource = m_Tournaments;
        }

        public void ClearList ()
        {
            ItemsSource = null;
        }
    }
}

