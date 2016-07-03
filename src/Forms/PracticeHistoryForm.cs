using System;
using System.Collections.Generic;
using ATMobile.Objects;
using Xamarin.Forms;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Constants;

namespace ATMobile.Forms
{
    public partial class PracticeHistoryForm : ContentPage
    {
        private StackLayout m_OutsideLayout;
        private Button m_btnAdd;
        private Picker m_ArcherPicker;
        private PracticeHistoryListView m_PracticeHistory;
        private List<Archer> m_Archers;
        private bool m_Loading;

        public PracticeHistoryForm ()
        {
            m_Loading = true;

            Title = "Practice History";

            //Icon = "settings.png";
            BackgroundColor = Color.FromHex (UIConstants.FormBackgroundColor);

            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            m_btnAdd = new Button {
                Text = "Add Practice"
            };
            m_btnAdd.Clicked += OnAdd;
            m_OutsideLayout.Children.Add (m_btnAdd);

            //Load Archers and setup picker
            m_Archers = ATManager.GetInstance ().GetArchers ();

            m_ArcherPicker = new Picker ();
            foreach (var archer in m_Archers) {
                m_ArcherPicker.Items.Add (archer.FullName);
            }
            m_ArcherPicker.SelectedIndexChanged += OnArcherPicked;
            m_OutsideLayout.Children.Add (m_ArcherPicker);

            //Add the sight settings listview
            m_PracticeHistory = new PracticeHistoryListView ();
            m_PracticeHistory.ItemSelected += OnSelected;
            m_OutsideLayout.Children.Add (m_PracticeHistory);

            GetCurrentArcher ();

            Content = m_OutsideLayout;

            m_Loading = false;
        }

        void GetCurrentArcher ()
        {
            Guid? currentArcher = ATManager.GetInstance ().GetGuidSetting (SettingConstants.CurrentArcher);

            if (currentArcher != null) {
                for (int i = 0; i < m_Archers.Count; i++) {
                    Archer archer = m_Archers [i];

                    if (archer.Id == currentArcher.Value) {
                        m_ArcherPicker.SelectedIndex = i;
                        break;
                    }
                }
            } else {
                if (m_Archers.Count > 0) {
                    m_ArcherPicker.SelectedIndex = 0;
                }
            }
        }

        Archer GetSelectedArcher ()
        {
            if (m_ArcherPicker.SelectedIndex >= 0) {
                return m_Archers [m_ArcherPicker.SelectedIndex];
            }

            return null;
        }

        void OnArcherPicked (object sender, EventArgs e)
        {
            if (!m_Loading) {
                if (m_ArcherPicker.SelectedIndex != -1) {
                    Archer archer = m_Archers [m_ArcherPicker.SelectedIndex];
                    ATManager.GetInstance ().SetSetting (
                        SettingConstants.CurrentArcher,
                        archer.Id);
                }
            }

            if (m_ArcherPicker.SelectedIndex >= 0) {
                m_btnAdd.IsEnabled = true;
            } else {
                m_btnAdd.IsEnabled = false;
            }

            RefreshList ();
        }

        void OnAdd (object sender, EventArgs e)
        {
            Archer selected = GetSelectedArcher ();

            if (selected != null) {
                PracticeForm addPractice = new PracticeForm ();
                addPractice.SetupForm (selected, null);

                Navigation.PushAsync (addPractice);
            }
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            Practice practice = (Practice)e.SelectedItem;

            Archer selected = GetSelectedArcher ();

            if (selected != null) {
                PracticeEndsForm practiceEnds = new PracticeEndsForm ();
                practiceEnds.SetupForm (selected, practice);
                Navigation.PushAsync (practiceEnds);
            }
        }

        protected override void OnAppearing ()
        {
            RefreshList ();
        }

        private void RefreshList ()
        {
            if (m_ArcherPicker.SelectedIndex == -1) {
                m_PracticeHistory.ClearList ();
            } else {
                Archer archer = m_Archers [m_ArcherPicker.SelectedIndex];
                m_PracticeHistory.RefreshList (archer.Id);
            }
        }
    }
}

