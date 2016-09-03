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
        private ATTextEntry m_txtFirstName;
        private ATTextEntry m_txtLastName;
        private ATDateEntry m_dpBirthDate;
        private ATDateEntry m_dpStartedArchery;

        public ArcherForm (bool initialArcher = false) : base ("Archer")
        {
            if (initialArcher) {
                ATLabel instruction = new ATLabel {
                    Text = "Please create an initial archer.  To login to existing account hit cancel and pick login.",
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Margin = new Thickness (0, 0, 0, 25),
                    FontAttributes = FontAttributes.Bold
                };
                InsideLayout.Children.Add (instruction);

                //CancelButton.IsVisible = false;
            }

            m_txtFirstName = new ATTextEntry {
                Title = "First Name",
                Placeholder = "Enter the first name of the archer"
            };
            InsideLayout.Children.Add (m_txtFirstName);

            m_txtLastName = new ATTextEntry {
                Title = "Last Name",
                Placeholder = "Enter the last name of the archer"
            };
            InsideLayout.Children.Add (m_txtLastName);

            m_dpBirthDate = new ATDateEntry ("Birth Date", "Select the Archer's birth date", _showDecade: true);
            InsideLayout.Children.Add (m_dpBirthDate);

            m_dpStartedArchery = new ATDateEntry ("Started Archery", "Select approximate date started archery.", _showDecade: true);
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

