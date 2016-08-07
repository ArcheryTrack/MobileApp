using System;
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

        private Grid m_EntryGrid;
        private Grid m_PickerGrid;

        private Label m_lblName;
        private Entry m_txtName;
        private Label m_lblDescription;
        private Entry m_txtDescription;
        private Label m_lblEnds;
        private Entry m_txtNumberOfEnds;
        private Label m_lblArrows;
        private Entry m_txtArrowsPerEnd;
        private Label m_lblDistance;
        private Entry m_txtDistance;

        private Label m_lblUnits;
        private Button m_btnPickUnits;
        private DistanceUnit m_DistanceUnit;

        private Label m_lblTargetFace;
        private Button m_btnPickTargetFace;
        private TargetFace m_TargetFace;


        public RoundTypeForm () : base ("Round Types")
        {
            //Setup grid to hold the controls
            m_EntryGrid = new Grid {
                Padding = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute) //Name
                    },
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute) //Description
                    },
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute) //Ends
                    },
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute) //Arrows
                    },
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute) //Distance
                    }
                },
                ColumnDefinitions = {
                    new ColumnDefinition {
                        Width = new GridLength(90, GridUnitType.Absolute)
                    },
                    new ColumnDefinition {
                        Width = new GridLength(1, GridUnitType.Star)
                    }
                }
            };
            InsideLayout.Children.Add (m_EntryGrid);

            //Setup the Name
            m_lblName = new Label {
                Text = "Name",
                VerticalTextAlignment = TextAlignment.Center
            };
            m_EntryGrid.Children.Add (m_lblName, 0, 0);

            m_txtName = new Entry {
                Placeholder = "Enter the name"
            };
            m_EntryGrid.Children.Add (m_txtName, 1, 0);

            //Setup Description
            m_lblDescription = new Label {
                Text = "Description",
                VerticalTextAlignment = TextAlignment.Center
            };
            m_EntryGrid.Children.Add (m_lblDescription, 0, 1);

            m_txtDescription = new Entry {
                Placeholder = "Enter the description"
            };
            m_EntryGrid.Children.Add (m_txtDescription, 1, 1);

            //Setup Number of Ends
            m_lblEnds = new Label {
                Text = "Ends",
                VerticalTextAlignment = TextAlignment.Center
            };
            m_EntryGrid.Children.Add (m_lblEnds, 0, 2);

            m_txtNumberOfEnds = new Entry {
                Placeholder = "Ends per round",
                Keyboard = Keyboard.Numeric
            };
            m_EntryGrid.Children.Add (m_txtNumberOfEnds, 1, 2);

            //Setup Number of Arrows
            m_lblArrows = new Label {
                Text = "Arrows",
                VerticalTextAlignment = TextAlignment.Center
            };
            m_EntryGrid.Children.Add (m_lblArrows, 0, 3);

            m_txtArrowsPerEnd = new Entry {
                Placeholder = "Arrows per end",
                Keyboard = Keyboard.Numeric
            };
            m_EntryGrid.Children.Add (m_txtArrowsPerEnd, 1, 3);

            //Setup Number of Distance
            m_lblDistance = new Label {
                Text = "Distance",
                VerticalTextAlignment = TextAlignment.Center
            };
            m_EntryGrid.Children.Add (m_lblDistance, 0, 4);

            m_txtDistance = new Entry {
                Placeholder = "Enter the distance",
                Keyboard = Keyboard.Numeric
            };
            m_EntryGrid.Children.Add (m_txtDistance, 1, 4);

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
            m_lblUnits = new Label {
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
            m_lblTargetFace = new Label {
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

