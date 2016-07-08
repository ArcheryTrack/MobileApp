using System;
using ATMobile.Constants;
using ATMobile.Controls;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentTypesForm : ContentPage
    {
        private StackLayout m_OutsideLayout;
        private Button m_btnAddType;
        private TournamentTypeListView m_TournamentTypes;

        public TournamentTypesForm ()
        {
            Title = "Tournament Types";
            BackgroundColor = Color.FromHex (UIConstants.FormBackgroundColor);

            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            m_btnAddType = new Button {
                Text = "Add Tournament Types"
            };
            m_btnAddType.Clicked += OnAdd;
            m_OutsideLayout.Children.Add (m_btnAddType);

            //Add the sight settings listview
            m_TournamentTypes = new TournamentTypeListView ();
            m_TournamentTypes.ItemSelected += OnSelected;
            m_OutsideLayout.Children.Add (m_TournamentTypes);

            Content = m_OutsideLayout;
        }

        void OnAdd (object sender, EventArgs e)
        {
            TournamentTypeForm addTournamentType = new TournamentTypeForm ();

            addTournamentType.SetupForm (null);
            Navigation.PushAsync (addTournamentType);
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            TournamentType tournamentType = (TournamentType)e.SelectedItem;

            TournamentTypeForm editTournamentType = new TournamentTypeForm ();
            editTournamentType.SetupForm (tournamentType);
            Navigation.PushAsync (editTournamentType);
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            m_TournamentTypes.RefreshList ();
        }
    }
}

