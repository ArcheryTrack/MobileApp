using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class TournamentRoundsListView : AbstractListView
    {
        public List<Round> Rounds;

        public TournamentRoundsListView ()
        {
            ItemTemplate = new DataTemplate (typeof (TournamentRoundCell));
            //RowHeight = 30;
            HasUnevenRows = true;
        }

        public void RefreshList (Guid _tournamentId)
        {
            ATManager manager = ATManager.GetInstance ();
            Rounds = manager.GetRounds(_tournamentId);

            ItemsSource = Rounds;
        }

        public void ClearList ()
        {
            ItemsSource = null;
        }
    }
}

