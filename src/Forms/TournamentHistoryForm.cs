using System;
using System.Collections.Generic;
using ATMobile.Constants;
using ATMobile.Controls;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public partial class TournamentHistoryForm : ContentPage
    {
        private StackLayout m_OutsideLayout;
        private Button m_btnAdd;
        private TournamentListView m_Tournaments;

        private bool m_Loading;

        public TournamentHistoryForm ()
        {
            Title = "Tournaments";
            m_Loading = true;

            BackgroundColor = Color.FromHex (UIConstants.FormBackgroundColor);

            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            m_btnAdd = new Button {
                Text = "Add Tournament"
            };
            m_btnAdd.Clicked += OnAdd;
            m_OutsideLayout.Children.Add (m_btnAdd);

            //Add the sight settings listview
            m_Tournaments = new TournamentListView ();
            m_Tournaments.ItemSelected += OnSelected;
            m_OutsideLayout.Children.Add (m_Tournaments);

            Content = m_OutsideLayout;

            m_Loading = false;
        }

        void OnAdd (object sender, EventArgs e)
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

