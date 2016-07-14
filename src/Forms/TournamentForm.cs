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

        private Label m_lblLocation;
        private Button m_btnPickLocation;
        private Range m_Location;

        private Label m_lblTournamentType;
        private Button m_btnPickTournamentType;
        private TournamentType m_TournamentType;

        private Label m_lblStartDate;
        private Button m_btnPickStart;
        private bool m_bolStartPicked;

        private Label m_lblEndDate;
        private Button m_btnPickEnd;
        private bool m_bolEndPicked;

        private Label m_lblArchers;
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

            //Setup grid to hold the controls
            m_Layout = new Grid {
                Padding = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute) //Location
                    },
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute) //Type
                    },
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute) //Start Date
                    },
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute) //End Date
                    },
                    new RowDefinition {
                        Height = new GridLength(25, GridUnitType.Absolute) //Archers Label
                    },
                    new RowDefinition {
                        Height = new GridLength(140, GridUnitType.Absolute) //Archers List and button
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

            //Setup Location
            m_lblLocation = new Label {
                Text = "Select location",
                HeightRequest = 40,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            m_Layout.Children.Add (m_lblLocation, 0, 0);

            m_btnPickLocation = new Button {
                Text = "Pick",
                WidthRequest = 80,
                HeightRequest = 40,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End
            };
            m_btnPickLocation.Clicked += PickLocation;
            m_Layout.Children.Add (m_btnPickLocation, 1, 0);

            //Setup the Tournament Type
            m_lblTournamentType = new Label {
                Text = "Select tournament type",
                HeightRequest = 40,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            m_Layout.Children.Add (m_lblTournamentType, 0, 1);

            m_btnPickTournamentType = new Button {
                Text = "Pick",
                WidthRequest = 80,
                HeightRequest = 40,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End
            };
            m_btnPickTournamentType.Clicked += PickTournamentType;
            m_Layout.Children.Add (m_btnPickTournamentType, 1, 1);

            //Setup the StartDate
            m_lblStartDate = new Label {
                Text = "Start Date:",
                HeightRequest = 40,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            m_Layout.Children.Add (m_lblStartDate, 0, 2);

            m_btnPickStart = new Button {
                Text = "Pick",
                WidthRequest = 80,
                HeightRequest = 40,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End
            };
            m_btnPickStart.Clicked += PickStart;
            m_Layout.Children.Add (m_btnPickStart, 1, 2);

            //Setup the EndDate
            m_lblEndDate = new Label {
                Text = "End Date:",
                HeightRequest = 40,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            m_Layout.Children.Add (m_lblEndDate, 0, 3);

            m_btnPickEnd = new Button {
                Text = "Pick",
                WidthRequest = 80,
                HeightRequest = 40,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End
            };
            m_btnPickEnd.Clicked += PickEnd;
            m_Layout.Children.Add (m_btnPickEnd, 1, 3);


            //Add the archers label
            m_lblArchers = new Label {
                Text = "Selected Archers",
                VerticalTextAlignment = TextAlignment.End
            };
            m_Layout.Children.Add (m_lblArchers, 0, 4);

            //Add the Archers list and button
            Frame archersFrame = new Frame {
                HasShadow = false
            };
            m_Archers = new TournamentArcherListView ();
            m_Archers.HeightRequest = 100;
            archersFrame.Content = m_Archers;
            m_Layout.Children.Add (archersFrame, 0, 5);

            m_btnAddArcher = new Button {
                Text = "Add",
                WidthRequest = 80,
                BorderWidth = 1,
                HeightRequest = 40,
                VerticalOptions = LayoutOptions.Start
            };
            m_btnAddArcher.Clicked += AddArcher;
            m_Layout.Children.Add (m_btnAddArcher, 1, 5);

            InsideLayout.Children.Add (m_Layout);
        }

        public override void Save ()
        {


            Navigation.PopAsync (true);
        }

        async private void PickStart (object sender, EventArgs e)
        {
            DatePickerForm picker = new DatePickerForm ("Select Tournament Start");
            await Navigation.PushModalAsync (picker);
        }

        async private void PickEnd (object sender, EventArgs e)
        {
            DatePickerForm picker = new DatePickerForm ("Select Tournament End");
            await Navigation.PushModalAsync (picker);
        }

        async private void PickLocation (object sender, EventArgs e)
        {
            RangePicker picker = new RangePicker ();
            picker.ItemPicked += RangePicked;

            await Navigation.PushModalAsync (picker);
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

        private void RangePicked (Range _range)
        {
            m_Location = _range;
            m_lblLocation.Text = m_Location.Name;
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

