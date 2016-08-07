using System;
using ATMobile.Cells;
using ATMobile.Controls;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentHistoryForm : AbstractListForm, IDisposable
    {
        private TournamentListView m_Tournaments;

        public TournamentHistoryForm () : base ("Tournaments")
        {
            m_Tournaments = new TournamentListView ();
            m_Tournaments.ItemSelected += OnSelected;
            ListFrame.Content = m_Tournaments;

            TournamentHistoryCell.TournamentEditClicked += EditTournament;
        }

        public override void Add ()
        {
            TournamentForm addTournament = new TournamentForm ();
            addTournament.SetupForm (null);
            Navigation.PushModalAsync (addTournament);
        }

        public void EditTournament (Tournament _tournament)
        {
            TournamentForm editTournament = new TournamentForm ();
            editTournament.SetupForm (_tournament);
            Navigation.PushModalAsync (editTournament);
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            Tournament tournament = (Tournament)e.SelectedItem;

            TournamentRoundsForm form = new TournamentRoundsForm ();
            form.SetupForm (tournament);
            Navigation.PushAsync (form, true);
        }

        protected override void OnAppearing ()
        {
            m_Tournaments.RefreshList ();
        }

        public void Dispose ()
        {
            TournamentHistoryCell.TournamentEditClicked -= EditTournament;
        }
    }
}

