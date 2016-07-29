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

        private Entry m_txtName;
        private Entry m_txtDescription;
        private Entry m_txtNumberOfEnds;
        private Entry m_txtArrowsPerEnd;
        private Entry m_txtDistance;
        private Picker m_pickUnits;

        private StackLayout m_layoutTarget;
        private Label m_lblTargetFace;
        private Button m_btnPickTargetFace;
        private TargetFace m_TargetFace;

        public RoundTypeForm () : base ("Round Types")
        {
            m_txtName = new Entry {
                Placeholder = "Name"
            };
            InsideLayout.Children.Add (m_txtName);

            m_txtDescription = new Entry {
                Placeholder = "Description"
            };
            InsideLayout.Children.Add (m_txtDescription);

            m_txtNumberOfEnds = new Entry {
                Placeholder = "Number of Ends",
                Keyboard = Keyboard.Numeric
            };
            InsideLayout.Children.Add (m_txtNumberOfEnds);

            m_txtArrowsPerEnd = new Entry {
                Placeholder = "Arrows per End",
                Keyboard = Keyboard.Numeric
            };
            InsideLayout.Children.Add (m_txtArrowsPerEnd);

            m_txtDistance = new Entry {
                Placeholder = "Distance",
                Keyboard = Keyboard.Numeric
            };
            InsideLayout.Children.Add (m_txtDistance);

            m_layoutTarget = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center
            };
            InsideLayout.Children.Add (m_layoutTarget);

            m_lblTargetFace = new Label {
                Text = "Select Target Face"
            };
            m_layoutTarget.Children.Add (m_lblTargetFace);

            m_btnPickTargetFace = new Button {
                Text = "Pick",
                WidthRequest = 80,
                HeightRequest = 40,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End
            };
            m_btnPickTargetFace.Clicked += PickTargetFace;
            m_layoutTarget.Children.Add (m_btnPickTargetFace);

            m_pickUnits = new Picker ();
            m_pickUnits.Items.Add ("Yards");
            m_pickUnits.Items.Add ("Meters");
            InsideLayout.Children.Add (m_pickUnits);
            m_pickUnits.SelectedIndex = 0;
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

            if (m_RoundType.TargetFaceId != Guid.Empty) {
                m_TargetFace = TargetFaceData.FindTarget (m_RoundType.TargetFaceId);
                m_lblTargetFace.Text = m_TargetFace.Name;
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
            m_lblTargetFace.Text = _targetFace.Name;
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

            Navigation.PopAsync (true);
        }
    }
}

