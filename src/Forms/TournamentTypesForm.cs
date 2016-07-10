﻿using ATMobile.Controls;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentTypesForm : AbstractListForm
    {
        private StackLayout m_OutsideLayout;
        private Button m_btnAddType;
        private TournamentTypeListView m_TournamentTypes;

        public TournamentTypesForm () : base ("Tournament Types")
        {
            m_TournamentTypes = new TournamentTypeListView ();
            m_TournamentTypes.ItemSelected += OnSelected;
            ListFrame.Content = m_TournamentTypes;
        }

        public override void Add ()
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
