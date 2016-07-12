using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using ATMobile.PickerForms;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentForm : AbstractEntryForm
    {
        private Tournament m_Tournament;

        private Entry m_txtName;

        private Grid m_Layout;
        private Label m_lblTournamentType;
        private TournamentType m_TournamentType;
        private Button m_btnPickTournamentType;

        private Label m_lblArchers;
        private StackLayout m_ArchersLayout;
        private TournamentArcherListView m_Archers;
        private Button m_btnAddArcher;
        private ObservableCollection<Archer> m_ArchersList;


        public TournamentForm () : base ("Tournament")
        {
            InsideLayout.Spacing = 5;

            m_txtName = new Entry {
                Placeholder = "Name of the Tournament"
            };
            InsideLayout.Children.Add (m_txtName);

            //Add the controls for the Tournament Type
            m_Layout = new Grid {
                Padding = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute)
                    },
                    new RowDefinition {
                        Height = new GridLength(140, GridUnitType.Absolute)
                    }
                },
                ColumnDefinitions = {
                    new ColumnDefinition {
                        Width = new GridLength(1, GridUnitType.Star)
                    },
                    new ColumnDefinition {
                        Width = new GridLength(80, GridUnitType.Absolute)
                    }
                }
            };

            m_lblTournamentType = new Label {
                Text = "Select the type of tournament.",
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            m_Layout.Children.Add (m_lblTournamentType, 0, 0);

            m_btnPickTournamentType = new Button {
                Text = "Pick",
                WidthRequest = 80,
                HeightRequest = 40,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End
            };
            m_btnPickTournamentType.Clicked += PickTournamentType;
            m_Layout.Children.Add (m_btnPickTournamentType, 1, 0);

            StackLayout m_ArchersLayout = new StackLayout {
                Orientation = StackOrientation.Vertical
            };
            m_lblArchers = new Label {
                Text = "Archers"
            };
            m_ArchersLayout.Children.Add (m_lblArchers);

            Frame archersFrame = new Frame {
                HasShadow = false
            };

            m_Archers = new TournamentArcherListView ();
            m_Archers.HeightRequest = 100;
            archersFrame.Content = m_Archers;
            m_ArchersLayout.Children.Add (archersFrame);

            m_Layout.Children.Add (m_ArchersLayout, 0, 1);

            m_btnAddArcher = new Button {
                Text = "Add",
                WidthRequest = 80,
                BorderWidth = 1,
                HeightRequest = 40,
                VerticalOptions = LayoutOptions.Start
            };
            m_btnAddArcher.Clicked += AddArcher;
            m_Layout.Children.Add (m_btnAddArcher, 1, 1);

            InsideLayout.Children.Add (m_Layout);
        }

        public override void Save ()
        {


            Navigation.PopAsync (true);
        }

        async private void PickTournamentType (object sender, EventArgs e)
        {
            TournamentTypePicker picker = new TournamentTypePicker ();
            picker.ItemPicked += TournamentTypePicked;

            await Navigation.PushModalAsync (picker);
        }

        async private void AddArcher (object sender, EventArgs e)
        {
            ArcherPicker picker = new ArcherPicker ();
            picker.ItemPicked += ArcherPicked;

            await Navigation.PushModalAsync (picker);
        }

        private void TournamentTypePicked (TournamentType _tournamentType)
        {
            m_TournamentType = _tournamentType;
            m_lblTournamentType.Text = m_TournamentType.Name;
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

