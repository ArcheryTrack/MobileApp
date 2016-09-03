using System;
using System.Text;
using ATMobile.Controls;
using ATMobile.Interfaces;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class RangeForm : AbstractEntryForm, IValidatedPage
    {
        private Range m_Range;
        private ATTextEntry m_txtName;
        private ATTextEntry m_txtAddress1;
        private ATTextEntry m_txtAddress2;
        private ATTextEntry m_txtCity;
        private ATTextEntry m_txtState;
        private ATTextEntry m_txtCountry;

        public RangeForm () : base ("Range")
        {
            m_txtName = new ATTextEntry {
                Title = "Name",
                Placeholder = "Enter the name of the range."
            };
            InsideLayout.Children.Add (m_txtName);

            m_txtAddress1 = new ATTextEntry () {
                Title = "Address 1",
                Placeholder = "Enter primary address"
            };
            InsideLayout.Children.Add (m_txtAddress1);

            m_txtAddress2 = new ATTextEntry {
                Title = "Address 2",
                Placeholder = "Enter secondary address"
            };
            InsideLayout.Children.Add (m_txtAddress2);

            m_txtCity = new ATTextEntry {
                Title = "City",
                Placeholder = "Enter the city name"
            };
            InsideLayout.Children.Add (m_txtCity);

            m_txtState = new ATTextEntry {
                Title = "State",
                Placeholder = "Enter the state abbreviation"
            };
            InsideLayout.Children.Add (m_txtState);

            m_txtCountry = new ATTextEntry {
                Title = "Country",
                Placeholder = "Enter the country abbreviation"
            };
            InsideLayout.Children.Add (m_txtCountry);
        }

        public void SetupForm (Range _range)
        {
            m_Range = _range;

            if (m_Range != null) {
                m_txtName.Text = m_Range.Name;
                m_txtAddress1.Text = m_Range.Address1;
                m_txtAddress2.Text = m_Range.Address2;
                m_txtCity.Text = m_Range.City;
                m_txtState.Text = m_Range.State;
                m_txtCountry.Text = m_Range.Country;
            }
        }

        public override void ValidateForm (StringBuilder _sb)
        {
            if (string.IsNullOrEmpty (m_txtName.Text)) {
                _sb.AppendLine ("You must specify the name of the range.");
            }
        }

        public override void Save ()
        {
            if (m_Range == null) {
                m_Range = new Range ();
                m_Range.Id = Guid.NewGuid ();
            }

            m_Range.Name = m_txtName.Text;
            m_Range.Address1 = m_txtAddress1.Text;
            m_Range.Address2 = m_txtAddress2.Text;
            m_Range.City = m_txtCity.Text;
            m_Range.State = m_txtState.Text;
            m_Range.Country = m_txtCountry.Text;

            ATManager.GetInstance ().Persist (m_Range);
        }

        public bool ValidatePage ()
        {
            if (m_txtName != null) {
                return true;
            }

            return false;
        }
    }
}

