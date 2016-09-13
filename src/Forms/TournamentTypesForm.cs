using System;
using ATMobile.Cells;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentTypesForm : AbstractListForm
    {
        private TournamentTypeListView m_TournamentTypes;

        public TournamentTypesForm () : base ("Tournament Templates")
        {
            m_TournamentTypes = new TournamentTypeListView ();
            m_TournamentTypes.ItemSelected += OnSelected;
            ListFrame.Content = m_TournamentTypes;
        }

        public override void Add ()
        {
            TournamentTypeForm addTournamentType = new TournamentTypeForm ();

            addTournamentType.SetupForm (null);
            Navigation.PushModalAsync (addTournamentType);
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            TournamentType tournamentType = (TournamentType)e.SelectedItem;

            TournamentTypeForm editTournamentType = new TournamentTypeForm ();
            editTournamentType.SetupForm (tournamentType);

            PublishActionMessage ("TournamentType Selected");

            Navigation.PushModalAsync (editTournamentType);
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            TournamentTypeCell.TournamentTypeDeleteClicked += DeleteClicked;
            m_TournamentTypes.RefreshList ();
        }

        async void DeleteClicked (TournamentType _tournamentType)
        {
            PublishActionMessage ("Tournament Type Delete Selected");

            bool result = await DisplayAlert ("ArcheryTrack", "Are you sure you want to delete this Tournament Type.  All associated records will also be deleted.  This will not delete actual tournaments.", "Delete", "Cancel");

            if (result) {
                ATManager manager = ATManager.GetInstance ();

                manager.DeleteTournamentType (_tournamentType.Id);

                m_TournamentTypes.RefreshList ();
            }
        }

        protected override void OnDisappearing ()
        {
            base.OnDisappearing ();
            TournamentTypeCell.TournamentTypeDeleteClicked -= DeleteClicked;
        }
    }
}

