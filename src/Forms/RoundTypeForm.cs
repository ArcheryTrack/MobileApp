using System;
using System.Collections.Generic;
using ATMobile.Constants;
using ATMobile.Enums;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class RoundTypeForm : ContentPage
    {
        private RoundType m_RoundType;

        private StackLayout m_OutsideLayout;
        private Button m_btnSave;

        private StackLayout m_InsideLayout;
        private Entry m_txtName;
        private Entry m_txtDescription;
        private Entry m_txtNumberOfEnds;
        private Entry m_txtArrowsPerEnd;
        private Entry m_txtDistance;
        private Picker m_pickUnits;

        public RoundTypeForm ()
        {
            Title = "Round Types";

            BackgroundColor = Color.FromHex (UIConstants.FormBackgroundColor);

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

            m_txtName = new Entry {
                Placeholder = "Name"
            };
            m_InsideLayout.Children.Add (m_txtName);

            m_txtDescription = new Entry {
                Placeholder = "Description"
            };
            m_InsideLayout.Children.Add (m_txtDescription);

            m_txtNumberOfEnds = new Entry {
                Placeholder = "Number of Ends",
                Keyboard = Keyboard.Numeric
            };
            m_InsideLayout.Children.Add (m_txtNumberOfEnds);

            m_txtArrowsPerEnd = new Entry {
                Placeholder = "Arrows per End",
                Keyboard = Keyboard.Numeric
            };
            m_InsideLayout.Children.Add (m_txtArrowsPerEnd);

            m_txtDistance = new Entry {
                Placeholder = "Distance",
                Keyboard = Keyboard.Numeric
            };
            m_InsideLayout.Children.Add (m_txtDistance);

            m_pickUnits = new Picker ();
            m_pickUnits.Items.Add ("Yards");
            m_pickUnits.Items.Add ("Meters");
            m_InsideLayout.Children.Add (m_pickUnits);
            m_pickUnits.SelectedIndex = 0;

            Content = m_OutsideLayout;
        }

        public void SetupForm (RoundType _roundType)
        {
            m_RoundType = _roundType;

            m_txtName.Text = m_RoundType.Name;
            m_txtDescription.Text = m_RoundType.Description;

            if (m_RoundType.NumberOfEnds >= 1) {
                m_txtNumberOfEnds.Text = Convert.ToString (m_RoundType.NumberOfEnds);
            }

            if (m_RoundType.ArrowsPerEnd >= 1) {
                m_txtArrowsPerEnd.Text = Convert.ToString (m_RoundType.ArrowsPerEnd);
            }

            if (m_RoundType.Distance != null) {
                m_txtDistance.Text = Convert.ToString (m_RoundType.Distance.Measurement);

                if (m_RoundType.Distance.Units == DistanceUnits.Yards) {
                    m_pickUnits.SelectedIndex = 0;
                } else {
                    m_pickUnits.SelectedIndex = 1;
                }
            }
        }

        async private void OnSave (object sender, EventArgs e)
        {
            m_RoundType.Name = m_txtName.Text;
            m_RoundType.Description = m_txtDescription.Text;

            if (!string.IsNullOrEmpty (m_txtNumberOfEnds.Text)) {
                m_RoundType.NumberOfEnds = Convert.ToInt32 (m_txtNumberOfEnds.Text);
            }

            if (!string.IsNullOrEmpty (m_txtArrowsPerEnd.Text)) {
                m_RoundType.ArrowsPerEnd = Convert.ToInt32 (m_txtArrowsPerEnd.Text);
            }

            int selectedUnit = m_pickUnits.SelectedIndex;
            DistanceUnits units;
            if (selectedUnit == 0) {
                units = DistanceUnits.Yards;
            } else {
                units = DistanceUnits.Meters;
            }

            double distanceValue = Convert.ToDouble (m_txtDistance.Text);
            m_RoundType.Distance = new Distance { Measurement = distanceValue, Units = units };

            ATManager.GetInstance ().Persist (m_RoundType);

            await Navigation.PopAsync (true);
        }
    }
}

