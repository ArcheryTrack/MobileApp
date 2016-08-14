using System;
using Xamarin.Forms;
using ATMobile.Objects;
using ATMobile.Managers;
using System.Text;

namespace ATMobile.Forms
{
    public class ArcherForm : AbstractEntryForm
    {
        private Archer m_Archer;
        private Entry m_txtFirstName;
        private Entry m_txtLastName;
        private Label m_lblBirthDate;
        private Label m_lblStartedArchery;
        private DatePicker m_datBirthdate;
        private DatePicker m_datStartedArchery;

        public ArcherForm () : base ("Archer")
        {
            m_txtFirstName = new Entry ();
            m_txtFirstName.Placeholder = "First Name";
            InsideLayout.Children.Add (m_txtFirstName);

            m_txtLastName = new Entry ();
            m_txtLastName.Placeholder = "Last Name";
            InsideLayout.Children.Add (m_txtLastName);

            m_lblBirthDate = new Label ();
            m_lblBirthDate.Text = "Birth Date";
            InsideLayout.Children.Add (m_lblBirthDate);

            m_datBirthdate = new DatePicker ();
            InsideLayout.Children.Add (m_datBirthdate);

            m_lblStartedArchery = new Label ();
            m_lblStartedArchery.Text = "Started Archery On";
            InsideLayout.Children.Add (m_lblStartedArchery);

            m_datStartedArchery = new DatePicker ();
            InsideLayout.Children.Add (m_datStartedArchery);
        }

        public void SetArcher (Archer _archer)
        {
            m_Archer = _archer;

            m_txtFirstName.Text = _archer.FirstName;
            m_txtLastName.Text = _archer.LastName;
            m_datBirthdate.Date = _archer.BirthDate;
            m_datStartedArchery.Date = _archer.StartedArchery;
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
            m_Archer.BirthDate = m_datBirthdate.Date;
            m_Archer.StartedArchery = m_datStartedArchery.Date;

            ATManager.GetInstance ().Persist (m_Archer);
        }
    }
}

