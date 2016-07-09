using ATMobile.Controls;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentHistoryForm : AbstractListForm
    {
        private TournamentListView m_Tournaments;

        private bool m_Loading;

        public TournamentHistoryForm () : base ("Tournaments")
        {
            m_Loading = true;

            m_Tournaments = new TournamentListView ();
            m_Tournaments.ItemSelected += OnSelected;
            ListFrame.Content = m_Tournaments;

            m_Loading = false;
        }

        public override void Add ()
        {
            TournamentForm editTournament = new TournamentForm ();
            editTournament.SetupForm (null);
            Navigation.PushAsync (editTournament);
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            Tournament tournament = (Tournament)e.SelectedItem;

            TournamentForm addTournament = new TournamentForm ();
            addTournament.SetupForm (tournament);
            Navigation.PushAsync (addTournament);
        }

        protected override void OnAppearing ()
        {
            m_Tournaments.RefreshList ();
        }
    }
}

