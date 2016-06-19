using System;
using ATMobile.Constants;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class RangeForm : ContentPage
    {
        private Range m_Range;
        private StackLayout m_OutsideLayout;
        private StackLayout m_InsideLayout;
        private Entry m_txtName;
        private Entry m_txtAddress1;
        private Entry m_txtAddress2;
        private Entry m_txtCity;
        private Picker m_pickState;
        private Picker m_pickCountry;

        private Button m_btnSave;

        public RangeForm ()
        {
            Title = "Range";

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

            m_txtName = new Entry ();
            m_txtName.Placeholder = "Name";
            m_InsideLayout.Children.Add (m_txtName);

            m_txtAddress1 = new Entry ();
            m_txtAddress1.Placeholder = "Address 1";
            m_InsideLayout.Children.Add (m_txtAddress1);

            m_txtAddress2 = new Entry ();
            m_txtAddress2.Placeholder = "Address 2";
            m_InsideLayout.Children.Add (m_txtAddress2);

            m_txtCity = new Entry ();
            m_txtCity.Placeholder = "City";
            m_InsideLayout.Children.Add (m_txtCity);

            m_pickState = new Picker ();
            foreach (var state in LocationConstants.USA_States) {
                m_pickState.Items.Add (state);
            }
            m_InsideLayout.Children.Add (m_pickState);

            m_pickCountry = new Picker ();
            foreach (var country in LocationConstants.Countries) {
                m_pickCountry.Items.Add (country);
            }
            m_pickCountry.SelectedIndex = 0;

            m_InsideLayout.Children.Add (m_pickCountry);

            Content = m_OutsideLayout;
        }

        public void SetupForm (Range _range)
        {
            m_Range = _range;

            if (m_Range != null) {
                m_txtName.Text = m_Range.Name;
                m_txtAddress1.Text = m_Range.Address1;
                m_txtAddress2.Text = m_Range.Address2;
                m_txtCity.Text = m_Range.City;

            }
        }

        private void OnSave (object sender, EventArgs e)
        {
            if (m_Range == null) {
                m_Range = new Range ();
                m_Range.Id = Guid.NewGuid ();
            }

            m_Range.Name = m_txtName.Text;
            m_Range.Address1 = m_txtAddress1.Text;
            m_Range.Address2 = m_txtAddress2.Text;
            m_Range.City = m_txtCity.Text;



            ATManager.GetInstance ().Persist (m_Range);

            Navigation.PopAsync ();
        }
    }
}

