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

        private StackLayout m_ArchersLayout;
        private TournamentArcherListView m_Archers;
        private Button m_btnAddArcher;
        private ObservableCollection<Archer> m_ArchersList;


        public TournamentForm () : base ("Tournament")
        {
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

            InsideLayout.Children.Add (m_ArchersLayout);
        }

        public override void Save ()
        {

            Navigation.PopAsync (true);
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

