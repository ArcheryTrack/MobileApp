using System;
using System.Text;
using ATMobile.Controls;
using ATMobile.Data;
using ATMobile.Enums;
using ATMobile.Managers;
using ATMobile.Objects;
using ATMobile.PickerForms;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class RoundTypeForm : AbstractEntryForm
    {
        private RoundType m_RoundType;

        private Grid m_PickerGrid;

        private ATTextEntry m_txtName;
        private ATTextEntry m_txtDescription;
        private ATTextEntry m_txtNumberOfEnds;
        private ATTextEntry m_txtArrowsPerEnd;
        private ATToggleEntry m_togCountX;
        private ATTextEntry m_txtDistance;

        private ATLabel m_lblUnits;
        private Button m_btnPickUnits;
        private DistanceUnit m_DistanceUnit;

        private ATLabel m_lblTargetFace;
        private Button m_btnPickTargetFace;
        private TargetFace m_TargetFace;


        public RoundTypeForm () : base ("Round Types")
        {
            m_txtName = new ATTextEntry {
                Title = "Name",
                Placeholder = "Enter the name"
            };
            InsideLayout.Children.Add (m_txtName);

            m_txtDescription = new ATTextEntry {
                Title = "Description",
                Placeholder = "Enter the description"
            };
            InsideLayout.Children.Add (m_txtDescription);

            m_txtNumberOfEnds = new ATTextEntry {
                Title = "Ends",
                Placeholder = "Ends per round",
                Keyboard = Keyboard.Numeric
            };
            InsideLayout.Children.Add (m_txtNumberOfEnds);

            m_txtArrowsPerEnd = new ATTextEntry {
                Title = "Arrows",
                Placeholder = "Arrows per end",
                Keyboard = Keyboard.Numeric
            };
            InsideLayout.Children.Add (m_txtArrowsPerEnd);

            m_togCountX = new ATToggleEntry {
                Title = "Count Xs?",
                IsToggled = false
            };
            InsideLayout.Children.Add (m_togCountX);

            m_txtDistance = new ATTextEntry {
                Title = "Distance",
                Placeholder = "Enter the distance",
                Keyboard = Keyboard.Numeric
            };
            InsideLayout.Children.Add (m_txtDistance);

            //Setup grid to hold the controls
            m_PickerGrid = new Grid {
                Padding = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute) //Distance Units
                    },
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute) //Target Face
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
            InsideLayout.Children.Add (m_PickerGrid);

            //Add the Distance Units
            m_lblUnits = new ATLabel {
                Text = "Select Distance Units",
                VerticalTextAlignment = TextAlignment.Center
            };
            m_PickerGrid.Children.Add (m_lblUnits, 0, 0);

            m_btnPickUnits = new Button {
                Text = "Pick",
                WidthRequest = 80,
                HeightRequest = 40,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End
            };
            m_btnPickUnits.Clicked += PickDistanceUnits;
            m_PickerGrid.Children.Add (m_btnPickUnits, 1, 0);

            //Add the Target Face at the botton.
            m_lblTargetFace = new ATLabel {
                Text = "Select Target Face",
                VerticalTextAlignment = TextAlignment.Center
            };
            m_PickerGrid.Children.Add (m_lblTargetFace, 0, 1);

            m_btnPickTargetFace = new Button {
                Text = "Pick",
                WidthRequest = 80,
                HeightRequest = 40,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End
            };
            m_btnPickTargetFace.Clicked += PickTargetFace;
            m_PickerGrid.Children.Add (m_btnPickTargetFace, 1, 1);
        }

        public void SetupForm (RoundType _roundType)
        {
            m_RoundType = _roundType;

            m_txtName.Text = m_RoundType.Name;
            m_txtDescription.Text = m_RoundType.Description;
            m_togCountX.IsToggled = m_RoundType.CountX;

            if (m_RoundType.NumberOfEnds >= 1) {
                m_txtNumberOfEnds.Text = Convert.ToString (m_RoundType.NumberOfEnds);
            }

            if (m_RoundType.ArrowsPerEnd >= 1) {
                m_txtArrowsPerEnd.Text = Convert.ToString (m_RoundType.ArrowsPerEnd);
            }

            if (m_RoundType.Distance != null) {
                m_txtDistance.Text = Convert.ToString (m_RoundType.Distance.Measurement);

                m_DistanceUnit = DistanceUnitData.FindDistanceUnit (m_RoundType.Distance.Units);
                SetUnitsText ();
            }

            if (m_RoundType.TargetFaceId != Guid.Empty) {
                m_TargetFace = TargetFaceData.FindTarget (m_RoundType.TargetFaceId);
                SetTargetFaceText ();
            }
        }

        async private void PickTargetFace (object sender, EventArgs e)
        {
            TargetFacePicker picker = new TargetFacePicker ();
            picker.ItemPicked += TargetFacePicked;

            await Navigation.PushModalAsync (picker);
        }

        private void TargetFacePicked (TargetFace _targetFace)
        {
            m_TargetFace = _targetFace;
            SetTargetFaceText ();
        }

        private void SetTargetFaceText ()
        {
            m_lblTargetFace.Text = m_TargetFace.Name;
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

        public override void ValidateForm (StringBuilder _sb)
        {
            if (string.IsNullOrEmpty (m_txtName.Text)) {
                _sb.AppendLine ("You must specify a name.");
            }

            if (string.IsNullOrEmpty (m_txtNumberOfEnds.Text)) {
                _sb.AppendLine ("You must specify the number of ends");
            }

            if (string.IsNullOrEmpty (m_txtArrowsPerEnd.Text)) {
                _sb.AppendLine ("You must specify the number of arrows per end");
            }

            if (m_TargetFace == null) {
                _sb.AppendLine ("You must specify a target face.");
            }
        }

        public override void Save ()
        {
            m_RoundType.Name = m_txtName.Text;
            m_RoundType.Description = m_txtDescription.Text;

            if (!string.IsNullOrEmpty (m_txtNumberOfEnds.Text)) {
                m_RoundType.NumberOfEnds = Convert.ToInt32 (m_txtNumberOfEnds.Text);
            }

            if (!string.IsNullOrEmpty (m_txtArrowsPerEnd.Text)) {
                m_RoundType.ArrowsPerEnd = Convert.ToInt32 (m_txtArrowsPerEnd.Text);
            }

            if (m_TargetFace != null) {
                m_RoundType.TargetFaceId = m_TargetFace.Id;
            }

            double distanceValue = Convert.ToDouble (m_txtDistance.Text);
            m_RoundType.Distance = new Distance { Measurement = distanceValue, Units = m_DistanceUnit.UnitOfMeasure };

            ATManager.GetInstance ().Persist (m_RoundType);
        }
    }
}

