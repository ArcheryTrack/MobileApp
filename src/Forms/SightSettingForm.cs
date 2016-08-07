using System;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class SightSettingForm : AbstractEntryForm
    {
        private Archer m_Archer;
        private SightSetting m_SightSetting;

        private DatePicker m_datSettingDate;
        private Label m_lblSettingDate;
        private Entry m_txtDistance;
        private Picker m_pickUnits;
        private Entry m_txtSetting;

        //TODO - Default the units to last used.

        public SightSettingForm () : base ("Sight Setting")
        {
            m_lblSettingDate = new Label ();
            m_lblSettingDate.Text = "Setting Date";
            InsideLayout.Children.Add (m_lblSettingDate);

            m_datSettingDate = new DatePicker ();
            m_datSettingDate.Date = DateTime.Now.Date;
            InsideLayout.Children.Add (m_datSettingDate);

            m_txtDistance = new Entry ();
            m_txtDistance.Keyboard = Keyboard.Numeric;
            m_txtDistance.Placeholder = "Distance";
            InsideLayout.Children.Add (m_txtDistance);

            m_pickUnits = new Picker ();
            m_pickUnits.Items.Add ("Yards");
            m_pickUnits.Items.Add ("Meters");
            m_pickUnits.SelectedIndex = 0;
            InsideLayout.Children.Add (m_pickUnits);

            m_txtSetting = new Entry ();
            m_txtSetting.Keyboard = Keyboard.Numeric;
            m_txtSetting.Placeholder = "Setting";
            InsideLayout.Children.Add (m_txtSetting);
        }

        public void SetSightSetting (Archer _archer, SightSetting _setting)
        {
            m_Archer = _archer;
            m_SightSetting = _setting;

            if (m_SightSetting != null) {
                m_datSettingDate.Date = m_SightSetting.DateTime;

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

        public override void Save ()
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

            m_SightSetting.Distance.Measurement = Convert.ToDouble (m_txtDistance.Text);
            m_SightSetting.Setting = Convert.ToDouble (m_txtSetting.Text);

            ATManager.GetInstance ().Persist (m_SightSetting);
        }
    }
}

