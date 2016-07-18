using System;
using System.Collections.Generic;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class TournamentEndsListView : AbstractListView
    {
        public List<TournamentEnd> Ends;

        public TournamentEndsListView ()
        {
            ItemTemplate = new DataTemplate (typeof (TournamentEnd));
            //RowHeight = 30;
            HasUnevenRows = true;
        }

        public void RefreshList (Guid _tournamentId, Guid _archerId)
        {
            ATManager manager = ATManager.GetInstance ();
            Ends = manager.GetTournamentEnds (_tournamentId, _archerId);

            ItemsSource = Ends;
        }

        public void ClearList ()
        {
            ItemsSource = null;
        }
    }
}

