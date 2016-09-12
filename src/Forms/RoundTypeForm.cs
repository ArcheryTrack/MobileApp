using System;
using System.Text;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class RoundTypeForm : AbstractEntryForm
    {
        private RoundType m_RoundType;

        private Grid m_PickerGrid;

        private ATTextEntry m_txtName;
        private ATTextEntry m_txtDescription;
        private ATIntegerEntry m_txtNumberOfEnds;
        private ATIntegerEntry m_txtArrowsPerEnd;
        private ATToggleEntry m_togCountX;
        private ATDistanceEntry m_txtDistance;
        private ATTargetEntry m_targetEntry;

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

            m_txtNumberOfEnds = new ATIntegerEntry {
                Title = "Ends",
                Placeholder = "Ends per round"
            };
            InsideLayout.Children.Add (m_txtNumberOfEnds);

            m_txtArrowsPerEnd = new ATIntegerEntry {
                Title = "Arrows",
                Placeholder = "Arrows per end"
            };
            InsideLayout.Children.Add (m_txtArrowsPerEnd);

            m_togCountX = new ATToggleEntry {
                Title = "Count Xs?",
                IsToggled = false
            };
            InsideLayout.Children.Add (m_togCountX);

            m_txtDistance = new ATDistanceEntry ();
            InsideLayout.Children.Add (m_txtDistance);

            m_targetEntry = new ATTargetEntry ("Target", "Target Face");
            InsideLayout.Children.Add (m_targetEntry);
        }

        public void SetupForm (RoundType _roundType)
        {
            m_RoundType = _roundType;

            if (m_RoundType.Name == null) {
                m_txtName.Text = string.Format ("Round {0}", m_RoundType.RoundNumber);
            } else {
                m_txtName.Text = m_RoundType.Name;
            }

            m_txtDescription.Text = m_RoundType.Description;
            m_togCountX.IsToggled = m_RoundType.CountX;

            if (m_RoundType.NumberOfEnds >= 1) {
                m_txtNumberOfEnds.Value = m_RoundType.NumberOfEnds;
            }

            if (m_RoundType.ArrowsPerEnd >= 1) {
                m_txtArrowsPerEnd.Value = m_RoundType.ArrowsPerEnd;
            }

            m_txtDistance.Distance = m_RoundType.Distance;

            if (!Guid.Empty.Equals (m_RoundType.TargetFaceId)) {
                TargetFace t = ATManager.GetInstance ().GetTargetFace (m_RoundType.TargetFaceId);
                m_targetEntry.TargetFace = t;
            }
        }

        public override void ValidateForm (StringBuilder _sb)
        {
            if (string.IsNullOrEmpty (m_txtName.Text)) {
                _sb.AppendLine ("You must specify a name.");
            }

            if (m_txtNumberOfEnds.Value == 0) {
                _sb.AppendLine ("You must specify the number of ends");
            }

            if (m_txtArrowsPerEnd.Value == 0) {
                _sb.AppendLine ("You must specify the number of arrows per end");
            }

            if (m_targetEntry.TargetFace == null) {
                _sb.AppendLine ("You must specify a target face.");
            }
        }

        public override void Save ()
        {
            m_RoundType.Name = m_txtName.Text;
            m_RoundType.Description = m_txtDescription.Text;

            m_RoundType.NumberOfEnds = Convert.ToInt32 (m_txtNumberOfEnds.Value);
            m_RoundType.ArrowsPerEnd = Convert.ToInt32 (m_txtArrowsPerEnd.Value);

            if (m_targetEntry.TargetFace != null) {
                m_RoundType.TargetFaceId = m_targetEntry.TargetFace.Id;
            }

            m_RoundType.Distance = m_txtDistance.Distance;

            ATManager.GetInstance ().Persist (m_RoundType);
        }
    }
}

