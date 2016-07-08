using System;
using ATMobile.Constants;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public partial class TournamentTypeForm : ContentPage
    {
        private TournamentType m_TournamentType;

        private StackLayout m_OutsideLayout;
        private StackLayout m_InsideLayout;

        private Button m_btnSave;

        private Entry m_txtName;
        private Entry m_txtDescription;
        private RoundTypeListView m_RoundTypes;
        private Button m_btnAddRound;

        public TournamentTypeForm ()
        {
            Title = "Tournament Type Setup";
            BackgroundColor = Color.FromHex (UIConstants.FormBackgroundColor);

            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            m_btnSave = new Button {
                Text = "Save"
            };
            m_btnSave.Clicked += OnSave;
            m_OutsideLayout.Children.Add (m_btnSave);

            m_InsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };
            m_OutsideLayout.Children.Add (m_InsideLayout);

            m_txtName = new Entry {
                Placeholder = "Name"
            };
            m_InsideLayout.Children.Add (m_txtName);

            m_txtDescription = new Entry {
                Placeholder = "Description"
            };
            m_InsideLayout.Children.Add (m_txtDescription);

            m_btnAddRound = new Button {
                Text = "Add Round"
            };
            m_btnAddRound.Clicked += OnAddRound;
            m_InsideLayout.Children.Add (m_btnAddRound);

            m_RoundTypes = new RoundTypeListView ();
            m_RoundTypes.ItemSelected += OnSelectedRound;
            Frame frame = new Frame {
                HasShadow = false
            };
            frame.Content = m_RoundTypes;
            m_InsideLayout.Children.Add (frame);

            Content = m_OutsideLayout;
        }

        public void SetupForm (TournamentType _tournamentType)
        {
            if (_tournamentType == null) {
                m_TournamentType = new TournamentType ();
            } else {
                m_TournamentType = _tournamentType;
            }

            m_txtName.Text = m_TournamentType.Name;
            m_txtDescription.Text = m_TournamentType.Description;

            m_RoundTypes.RefreshList (m_TournamentType.Id);
        }

        void OnSelectedRound (object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void SaveTournamentType ()
        {
            m_TournamentType.Name = m_txtName.Text;
            m_TournamentType.Description = m_txtDescription.Text;

            ATManager.GetInstance ().Persist (m_TournamentType);
        }

        void OnAddRound (object sender, EventArgs e)
        {
            SaveTournamentType ();

            RoundType roundType = new RoundType ();
            roundType.ParentId = m_TournamentType.Id;

            RoundTypeForm form = new RoundTypeForm ();
            form.SetupForm (roundType);
            Navigation.PushAsync (form);
        }

        void OnSave (object sender, EventArgs e)
        {
            SaveTournamentType ();

            Navigation.PopAsync (true);
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            m_RoundTypes.RefreshList (m_TournamentType.Id);
        }
    }
}

