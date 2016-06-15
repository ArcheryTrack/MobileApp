using System;
using System.Collections.Generic;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public partial class SightSettingForm : ContentPage
    {
        private Archer m_Archer;
        private SightSetting m_SightSetting;
        private StackLayout m_OutsideLayout;
        private StackLayout m_InsideLayout;
        private Button m_btnSave;
        private DatePicker m_datSettingDate;
        private Label m_lblSettingDate;
        private Entry m_txtDistance;
        private Picker m_pickUnits;
        private Entry m_txtSetting;

        //TODO - Default the units to last used.

        public SightSettingForm ()
        {
            Title = "Sight Setting";

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

            m_lblSettingDate = new Label ();
            m_lblSettingDate.Text = "Setting Date";
            m_InsideLayout.Children.Add (m_lblSettingDate);

            m_datSettingDate = new DatePicker ();
            m_datSettingDate.Date = DateTime.Now.Date;
            m_InsideLayout.Children.Add (m_datSettingDate);

            m_txtDistance = new Entry ();
            m_txtDistance.Keyboard = Keyboard.Numeric;
            m_txtDistance.Placeholder = "Distance";
            m_InsideLayout.Children.Add (m_txtDistance);

            m_pickUnits = new Picker ();
            m_pickUnits.Items.Add ("Yards");
            m_pickUnits.Items.Add ("Meters");
            m_pickUnits.SelectedIndex = 0;
            m_InsideLayout.Children.Add (m_pickUnits);

            m_txtSetting = new Entry ();
            m_txtSetting.Keyboard = Keyboard.Numeric;
            m_txtSetting.Placeholder = "Setting";
            m_InsideLayout.Children.Add (m_txtSetting);

            Content = m_OutsideLayout;
        }

        public void SetSightSetting (Archer _archer, SightSetting _setting)
        {
            m_Archer = _archer;
            m_SightSetting = _setting;

            if (m_SightSetting != null) {
                if (m_SightSetting.Distance != null) {
                    if (m_SightSetting.Distance.Units == Enums.DistanceUnits.Yards) {
                        m_pickUnits.SelectedIndex = 0;
                    } else {
                        m_pickUnits.SelectedIndex = 1;
                    }

                    m_txtDistance.Text = Convert.ToString (m_SightSetting.Distance.Measurement);
                }

                m_txtSetting.Text = Convert.ToString (m_SightSetting.Setting);
            }
        }

        private void OnSave (object sender, EventArgs e)
        {
            if (m_SightSetting == null) {
                m_SightSetting = new SightSetting ();
                m_SightSetting.Id = Guid.NewGuid ();
                m_SightSetting.ParentId = m_Archer.Id;
            }

            m_SightSetting.DateTime = m_datSettingDate.Date;

            if (m_pickUnits.SelectedIndex == 0) {
                m_SightSetting.Distance = new Distance () { Units = Enums.DistanceUnits.Yards };
            } else {
                m_SightSetting.Distance = new Distance () { Units = Enums.DistanceUnits.Meters };
            }

            m_SightSetting.Distance.Measurement = Convert.ToDouble (m_txtSetting.Text);

            ATManager.GetInstance ().Persist (m_SightSetting);

            Navigation.PopAsync ();
        }
    }


}

