using System;
using System.Collections.Generic;
using ATMobile.Constants;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class SightSetupForm : AbstractListForm
    {
        private bool m_Loading;
        private List<Archer> m_Archers;

        private Picker m_ArcherPicker;
        private SightSettingListView m_SightSettings;


        public SightSetupForm () : base ("Sight Setup")
        {
            m_Loading = true;

            //Load Archers and setup picker
            m_Archers = ATManager.GetInstance ().GetArchers ();

            m_ArcherPicker = new Picker ();
            foreach (var archer in m_Archers) {
                m_ArcherPicker.Items.Add (archer.FullName);
            }
            m_ArcherPicker.SelectedIndexChanged += OnArcherPicked;
            OutsideLayout.Children.Insert (1, m_ArcherPicker);

            m_SightSettings = new SightSettingListView ();
            m_SightSettings.ItemSelected += OnSelected;
            ListFrame.Content = m_SightSettings;

            GetCurrentArcher ();

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
                AddButton.IsEnabled = true;
            } else {
                AddButton.IsEnabled = false;
            }

            RefreshList ();
        }

        public override void Add ()
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

