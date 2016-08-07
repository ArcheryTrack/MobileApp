using System;
using ATMobile.Interfaces;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class RangeForm : AbstractEntryForm, IValidatedPage
    {
        private Range m_Range;
        private Entry m_txtName;
        private Entry m_txtAddress1;
        private Entry m_txtAddress2;
        private Entry m_txtCity;
        private Entry m_txtState;
        private Entry m_txtCountry;

        public RangeForm () : base ("Range")
        {
            m_txtName = new Entry ();
            m_txtName.Placeholder = "Name";
            InsideLayout.Children.Add (m_txtName);

            m_txtAddress1 = new Entry ();
            m_txtAddress1.Placeholder = "Address 1";
            InsideLayout.Children.Add (m_txtAddress1);

            m_txtAddress2 = new Entry ();
            m_txtAddress2.Placeholder = "Address 2";
            InsideLayout.Children.Add (m_txtAddress2);

            m_txtCity = new Entry ();
            m_txtCity.Placeholder = "City";
            InsideLayout.Children.Add (m_txtCity);

            m_txtState = new Entry ();
            m_txtState.Placeholder = "State";
            InsideLayout.Children.Add (m_txtState);

            m_txtCountry = new Entry ();
            m_txtCountry.Placeholder = "Country";
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

