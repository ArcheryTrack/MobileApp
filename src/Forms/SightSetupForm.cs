using System;
using System.Collections.Generic;
using ATMobile.Constants;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public partial class SightSetupForm : ContentPage
    {
        private StackLayout m_OutsideLayout;
        private Button m_btnAdd;
        private Picker m_ArcherPicker;
        private SightSettingListView m_SightSettings;
        private List<Archer> m_Archers;
        private bool m_Loading;

        public SightSetupForm ()
        {
            m_Loading = true;

            Title = "Sight Setup";

            //Icon = "settings.png";
            BackgroundColor = Color.FromHex ("EEEEEE");

            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            m_btnAdd = new Button {
                Text = "Add Setting"
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
            m_SightSettings = new SightSettingListView ();
            m_SightSettings.ItemSelected += OnSelected;
            m_OutsideLayout.Children.Add (m_SightSettings);

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
                SightSettingForm addSetting = new SightSettingForm ();
                addSetting.SetSightSetting (selected, null);

                Navigation.PushAsync (addSetting);
            }
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            SightSetting setting = (SightSetting)e.SelectedItem;

            Archer selected = GetSelectedArcher ();

            if (selected != null) {
                SightSettingForm addSetting = new SightSettingForm ();
                addSetting.SetSightSetting (selected, setting);
                Navigation.PushAsync (addSetting);
            }
        }

        protected override void OnAppearing ()
        {
            RefreshList ();
        }

        private void RefreshList ()
        {
            if (m_ArcherPicker.SelectedIndex == -1) {
                m_SightSettings.ClearList ();
            } else {
                Archer archer = m_Archers [m_ArcherPicker.SelectedIndex];
                m_SightSettings.RefreshList (archer.Id);
            }
        }
    }
}

