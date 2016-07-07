using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ATMobile.Cells;
using ATMobile.Constants;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using ATMobile.PickerForms;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentForm : ContentPage
    {
        private Tournament m_Tournament;

        private StackLayout m_OutsideLayout;
        private StackLayout m_InsideLayout;

        private Button m_btnSave;

        private StackLayout m_ArchersLayout;
        private TournamentArcherListView m_Archers;
        private Button m_btnAddArcher;
        private ObservableCollection<Archer> m_ArchersList;


        public TournamentForm ()
        {
            Title = "Tournament";

            //Icon = "settings.png";
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

            m_ArchersLayout = new StackLayout {
                Spacing = 15,
                Orientation = StackOrientation.Horizontal,
                Padding = 5
            };

            Frame archersFrame = new Frame {
                HasShadow = false
            };

            m_Archers = new TournamentArcherListView ();
            m_Archers.HeightRequest = 100;
            archersFrame.Content = m_Archers;

            m_ArchersLayout.Children.Add (archersFrame);

            m_btnAddArcher = new Button {
                Text = "Add Archer",
                WidthRequest = 100,
                BorderWidth = 1
            };
            m_btnAddArcher.Clicked += AddArcher;
            m_ArchersLayout.Children.Add (m_btnAddArcher);

            m_InsideLayout.Children.Add (m_ArchersLayout);

            Content = m_OutsideLayout;
        }

        async private void OnSave (object sender, EventArgs e)
        {

            await Navigation.PopAsync (true);
        }

        async private void AddArcher (object sender, EventArgs e)
        {
            ArcherPicker picker = new ArcherPicker ();
            picker.ItemPicked += ArcherPicked;

            await Navigation.PushModalAsync (picker);
        }

        private void ArcherPicked (Archer _archer)
        {
            m_ArchersList.Add (_archer);
        }

        public void SetupForm (Tournament _tournament)
        {
            if (_tournament == null) {
                m_Tournament = new Tournament ();
            } else {
                m_Tournament = _tournament;
            }

            FillList (m_Tournament.Archers);
        }

        public void FillList (List<Guid> _archerGuids)
        {
            ATManager manager = ATManager.GetInstance ();
            m_ArchersList = new ObservableCollection<Archer> ();

            foreach (var guid in _archerGuids) {
                var archer = manager.GetArcher (guid);

                if (archer != null) {
                    m_ArchersList.Add (archer);
                }
            }

            m_Archers.ItemsSource = m_ArchersList;
        }
    }
}

