using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ATMobile.Controls;
using ATMobile.Helpers;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class PracticeEndForm : AbstractEntryForm
    {
        private Archer m_Archer;
        private Practice m_Practice;
        private PracticeEnd m_PracticeEnd;
        private int m_EndCount;
        private TargetFace m_TargetFace;
        private ATLabel m_lblNote;
        private Editor m_txtNote;
        private ATLabel m_lblPoints;

        private ShotArrowListView m_ArrowsListView;
        private ScoreControl m_ScoreControl;


        public PracticeEndForm () : base ("End")
        {
            m_lblPoints = new ATLabel {
                Text = "Points: "
            };
            InsideLayout.Children.Add (m_lblPoints);

            StackLayout layout = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 5,
                Spacing = 0
            };
            InsideLayout.Children.Add (layout);

            Frame frame = new Frame {
                HasShadow = false,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            m_ArrowsListView = new ShotArrowListView ();
            frame.Content = m_ArrowsListView;
            layout.Children.Add (frame);

            Frame frame2 = new Frame {
                HasShadow = false,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            m_ScoreControl = new ScoreControl ();
            m_ScoreControl.ScoreClicked += Clicked;
            frame2.Content = m_ScoreControl;
            layout.Children.Add (frame2);

            m_lblNote = new ATLabel {
                Text = "Note",
                Margin = new Thickness (0, 10, 0, 5)
            };
            InsideLayout.Children.Add (m_lblNote);

            m_txtNote = new Editor {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            InsideLayout.Children.Add (m_txtNote);

        }

        private int FindNextArrowNumber ()
        {
            var sorted = m_ArrowsListView.Arrows;

            int count = sorted.Count;

            for (int i = 0; i < count; i++) {
                ShotArrow arrow = sorted [i];

                if (arrow.ArrowNumber != i + 1) {
                    return i + 1;
                }
            }

            return count + 1;
        }

        private void Clicked (string value)
        {
            ShotArrow arrow = new ShotArrow ();

            arrow.ArrowNumber = FindNextArrowNumber ();
            arrow.PossiblePoints = m_TargetFace.MaximumPoints;
            arrow.Score = value;

            if (value == "X") {
                arrow.ScoreValue = m_TargetFace.MaximumPoints;
                arrow.SortValue = arrow.ScoreValue + 1;
            } else if (value == "M") {
                arrow.ScoreValue = 0;
                arrow.SortValue = 0;
            } else {
                arrow.ScoreValue = Convert.ToInt32 (value);
                arrow.SortValue = arrow.ScoreValue;
            }

            m_ArrowsListView.Arrows.Insert (arrow.ArrowNumber - 1, arrow);

            SetPoints ();
        }

        private void SetPoints ()
        {
            int points = 0;
            foreach (var item in m_ArrowsListView.Arrows) {
                points += item.ScoreValue;
            }
            m_lblPoints.Text = string.Format ("Points: {0}", points);
        }

        public void FillList ()
        {
            ObservableCollection<ShotArrow> arrows = m_ArrowsListView.Arrows;
            arrows.Clear ();

            foreach (var item in m_PracticeEnd.SortedByArrowNumber) {
                arrows.Add (item);
            }

            m_ArrowsListView.Arrows = arrows;
        }

        public void SetupForm (
            Archer _archer,
            Practice _practice,
            PracticeEnd _end,
            int _endCount)
        {
            m_Archer = _archer;
            m_Practice = _practice;
            m_PracticeEnd = _end;
            m_EndCount = _endCount;

            if (m_Practice.TargetFaceId != null) {
                m_TargetFace = ATManager.GetInstance ().GetTargetFace (m_Practice.TargetFaceId.Value);
            } else {
                m_TargetFace = null;
            }

            if (m_PracticeEnd == null) {
                m_PracticeEnd = new PracticeEnd ();
                m_PracticeEnd.Id = Guid.NewGuid ();
                m_PracticeEnd.ParentId = m_Practice.Id;
                m_PracticeEnd.EndNumber = m_EndCount + 1;
                m_PracticeEnd.ArcherId = _archer.Id;
            }

            FillList ();

            m_ScoreControl.SetTargetFace (m_TargetFace);
            m_txtNote.Text = m_PracticeEnd.Note;

            SetPoints ();
        }

        public override void ValidateForm (StringBuilder _sb)
        {

        }

        public override void Save ()
        {
            m_PracticeEnd.Results = m_ArrowsListView.Arrows.ToList ();
            m_PracticeEnd.Note = m_txtNote.Text;

            ATManager.GetInstance ().Persist (m_PracticeEnd);
        }

        protected override void OnDisappearing ()
        {
            base.OnDisappearing ();

            m_ScoreControl.ScoreClicked -= Clicked;
            m_ArrowsListView.Dispose ();
        }
    }
}

