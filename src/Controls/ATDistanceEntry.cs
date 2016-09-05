using System;
using ATMobile.Constants;
using ATMobile.Data;
using ATMobile.Enums;
using ATMobile.Objects;
using ATMobile.PickerForms;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class ATDistanceEntry : ContentView
    {
        private Entry m_txtDistance;
        private Label m_lblTitle;
        private Label m_lblUnits;
        private Button m_btnPick;
        private Grid m_gridLayout;
        private DistanceUnit m_DistanceUnit;
        private StackLayout m_StackLayout;

        public ATDistanceEntry ()
        {
            m_DistanceUnit = DistanceUnitData.FindDistanceUnit (DistanceUnits.Meters);

            //Setup grid to hold the controls
            m_gridLayout = new Grid {
                Padding = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition {
                        Height = new GridLength (40, GridUnitType.Absolute) //Name
                    }
                },
                ColumnDefinitions = {
                    new ColumnDefinition {
                        Width = new GridLength(1, GridUnitType.Star)
                    },
                    new ColumnDefinition {
                        Width = new GridLength(80, GridUnitType.Absolute)
                    }
                }
            };
            Content = m_gridLayout;

            m_StackLayout = new StackLayout {
                Orientation = StackOrientation.Horizontal
            };
            m_gridLayout.Children.Add (m_StackLayout, 0, 0);

            m_lblTitle = new ATLabel {
                Text = "Distance",
                VerticalTextAlignment = TextAlignment.Center
            };
            m_StackLayout.Children.Add (m_lblTitle);

            m_txtDistance = new Entry {
                Keyboard = Keyboard.Numeric,
                WidthRequest = 70
            };
            m_StackLayout.Children.Add (m_txtDistance);

            m_lblUnits = new ATLabel {
                VerticalTextAlignment = TextAlignment.Center
            };
            m_StackLayout.Children.Add (m_lblUnits);

            m_btnPick = new Button {
                Text = "Units",
                WidthRequest = 80,
                HeightRequest = 40,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End
            };
            m_btnPick.Clicked += PickDistanceUnits;
            m_gridLayout.Children.Add (m_btnPick, 1, 0);

            SetUnitsText ();
        }

        async private void PickDistanceUnits (object sender, EventArgs e)
        {
            DistanceUnitsPicker picker = new DistanceUnitsPicker ();
            picker.ItemPicked += DistanceUnitsPicked;

            await Navigation.PushModalAsync (picker);
        }


        private void DistanceUnitsPicked (DistanceUnit _units)
        {
            m_DistanceUnit = _units;
            SetUnitsText ();
        }

        private void SetUnitsText ()
        {
            m_lblUnits.Text = m_DistanceUnit.Name;
        }

        public Distance Distance {
            get {
                double result = 0;

                Double.TryParse (m_txtDistance.Text, out result);

                Distance d = new Distance ();
                d.Units = m_DistanceUnit.UnitOfMeasure;
                d.Measurement = result;

                return d;
            }

            set {
                Distance d = value;

                if (d == null) {
                    m_txtDistance.Text = null;
                    m_DistanceUnit = DistanceUnitData.FindDistanceUnit (DistanceUnits.Meters);
                } else {
                    m_txtDistance.Text = Convert.ToString (d.Measurement);
                    m_DistanceUnit = DistanceUnitData.FindDistanceUnit (d.Units);
                }
            }
        }

        public new bool IsEnabled {
            get {
                return base.IsEnabled;
            }

            set {
                base.IsEnabled = value;
                m_txtDistance.IsEnabled = value;

                if (base.IsEnabled) {
                    m_txtDistance.BackgroundColor = Color.FromHex (UIConstants.TextBackgroundEnabled);
                } else {
                    m_txtDistance.BackgroundColor = Color.FromHex (UIConstants.TextBackgroundDisabled);
                }
            }
        }
    }
}

