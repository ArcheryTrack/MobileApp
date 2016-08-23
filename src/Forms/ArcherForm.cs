using System;
using Xamarin.Forms;
using ATMobile.Objects;
using ATMobile.Managers;
using System.Text;
using ATMobile.Controls;

namespace ATMobile.Forms
{
    public class ArcherForm : AbstractEntryForm
    {
        private Archer m_Archer;
        private Entry m_txtFirstName;
        private Entry m_txtLastName;
        private ATDatePicker m_dpBirthDate;
        private ATDatePicker m_dpStartedArchery;

        public ArcherForm () : base ("Archer")
        {
            m_txtFirstName = new Entry ();
            m_txtFirstName.Placeholder = "First Name";
            InsideLayout.Children.Add (m_txtFirstName);

            m_txtLastName = new Entry ();
            m_txtLastName.Placeholder = "Last Name";
            InsideLayout.Children.Add (m_txtLastName);

            m_dpBirthDate = new ATDatePicker ("Birth Date", "Select the Archer's birth date", _showDecade: true);
            InsideLayout.Children.Add (m_dpBirthDate);

            m_dpStartedArchery = new ATDatePicker ("Started Archery", "Select approximate date started archery.", _showDecade: true);
            InsideLayout.Children.Add (m_dpStartedArchery);

        }

        public void SetArcher (Archer _archer)
        {
            m_Archer = _archer;

            m_txtFirstName.Text = _archer.FirstName;
            m_txtLastName.Text = _archer.LastName;

            m_dpBirthDate.SelectedDate = _archer.BirthDate;
            m_dpStartedArchery.SelectedDate = _archer.StartedArchery;
        }

        public override void ValidateForm (StringBuilder _sb)
        {
            if (m_txtFirstName.Text == null
                && m_txtLastName.Text == null) {
                _sb.AppendLine ("First or Last Name are required");
            }
        }

        public override void Save ()
        {
            if (m_Archer == null) {
                m_Archer = new Archer ();
                m_Archer.Id = Guid.NewGuid ();
            }

            m_Archer.FirstName = m_txtFirstName.Text;
            m_Archer.LastName = m_txtLastName.Text;

            m_Archer.BirthDate = m_dpBirthDate.SelectedDate;
            m_Archer.StartedArchery = m_dpStartedArchery.SelectedDate;

            ATManager.GetInstance ().Persist (m_Archer);
        }
    }
}

