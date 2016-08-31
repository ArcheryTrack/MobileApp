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
        private ATTextWithLabel m_txtName;
        private ATTextWithLabel m_txtAddress1;
        private ATTextWithLabel m_txtAddress2;
        private ATTextWithLabel m_txtCity;
        private ATTextWithLabel m_txtState;
        private ATTextWithLabel m_txtCountry;

        public RangeForm () : base ("Range")
        {
            m_txtName = new ATTextWithLabel {
                Title = "Name",
                Placeholder = "Enter the name of the range."
            };
            InsideLayout.Children.Add (m_txtName);

            m_txtAddress1 = new ATTextWithLabel () {
                Title = "Address 1",
                Placeholder = "Enter primary address"
            };
            InsideLayout.Children.Add (m_txtAddress1);

            m_txtAddress2 = new ATTextWithLabel {
                Title = "Address 2",
                Placeholder = "Enter secondary address"
            };
            InsideLayout.Children.Add (m_txtAddress2);

            m_txtCity = new ATTextWithLabel {
                Title = "City",
                Placeholder = "Enter the city name"
            };
            InsideLayout.Children.Add (m_txtCity);

            m_txtState = new ATTextWithLabel {
                Title = "State",
                Placeholder = "Enter the state abbreviation"
            };
            InsideLayout.Children.Add (m_txtState);

            m_txtCountry = new ATTextWithLabel {
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

