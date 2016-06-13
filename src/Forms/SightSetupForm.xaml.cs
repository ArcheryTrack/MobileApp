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
        private Button m_Add;
        private Picker m_ArcherPicker;
        private SightSettingListView m_SightSettings;
        private List<Archer> m_Archers;
        private bool m_Loading;

        public SightSetupForm ()
        {
            InitializeComponent ();

            m_Loading = true;

            Title = "Sight Setup";

            //Icon = "settings.png";
            BackgroundColor = Color.FromHex ("EEEEEE");

            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            m_Add = new Button {
                Text = "Add Setting"
            };
            m_Add.Clicked += OnAdd;
            m_OutsideLayout.Children.Add (m_Add);

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

            RefreshList ();
        }

        void OnAdd (object sender, EventArgs e)
        {
            SightSettingForm addSetting = new SightSettingForm ();
            Navigation.PushAsync (addSetting);
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            SightSetting setting = (SightSetting)e.SelectedItem;

            SightSettingForm addSetting = new SightSettingForm ();
            addSetting.SetSightSetting (setting);
            Navigation.PushAsync (addSetting);
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

