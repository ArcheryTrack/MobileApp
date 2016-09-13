using System;
using ATMobile.Cells;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentHistoryForm : AbstractListForm
    {
        private TournamentListView m_Tournaments;

        public TournamentHistoryForm () : base ("Tournaments")
        {
            m_Tournaments = new TournamentListView ();
            m_Tournaments.ItemSelected += OnSelected;
            ListFrame.Content = m_Tournaments;
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

            PublishActionMessage ("Tournament Edit Selected");

            Navigation.PushModalAsync (editTournament);
        }

        public async void DeleteTournament (Tournament _tournament)
        {
            PublishActionMessage ("Tournament Delete Selected");

            bool result = await DisplayAlert ("ArcheryTrack", "Are you sure you want to delete this tournament.  All associated records will also be deleted", "Delete", "Cancel");

            if (result) {
                ATManager.GetInstance ().DeleteTournament (_tournament.Id);
                m_Tournaments.RefreshList ();
            }
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            Tournament tournament = (Tournament)e.SelectedItem;

            TournamentRoundsForm form = new TournamentRoundsForm ();
            form.SetupForm (tournament);

            PublishActionMessage ("Tournament Selected");

            Navigation.PushAsync (form, true);
        }

        protected override void OnAppearing ()
        {
            m_Tournaments.RefreshList ();

            TournamentHistoryCell.TournamentEditClicked += EditTournament;
            TournamentHistoryCell.TournamentDeleteClicked += DeleteTournament;
        }

        public void Dispose ()
        {
            TournamentHistoryCell.TournamentEditClicked -= EditTournament;
            TournamentHistoryCell.TournamentDeleteClicked -= DeleteTournament;
        }
    }
}

