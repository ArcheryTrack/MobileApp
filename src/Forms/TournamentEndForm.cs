using System;
using System.Collections.ObjectModel;
using System.Linq;
using ATMobile.Controls;
using ATMobile.Helpers;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentEndForm : AbstractEntryForm
    {
        private Archer m_Archer;
        private Tournament m_Practice;
        private TournamentEnd m_PracticeEnd;
        private int m_EndCount;
        private TargetFace m_TargetFace;

        private Label m_lblPoints;

        private ShotArrowListView m_ArrowsListView;
        private ScoreControl m_ScoreControl;

        public TournamentEndForm () : base ("End")
        {

            m_lblPoints = new Label {
                Text = "Points: "
            };
            InsideLayout.Children.Add (m_lblPoints);

            StackLayout layout = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 5,
                Spacing = 15
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

            arrow.Score = value;
            if (value == "X") {
                if (m_TargetFace != null) {
                    arrow.ScoreValue = m_TargetFace.MaximumPoints;
                    arrow.SortValue = arrow.ScoreValue + 1;
                } else {
                    arrow.ScoreValue = -1;
                    arrow.SortValue = arrow.ScoreValue + 1;
                }
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
                m_TargetFace = TargetHelper.FindTarget (m_Practice.TargetFaceId.Value);
            } else {
                m_TargetFace = null;
            }

            if (m_PracticeEnd == null) {
                m_PracticeEnd = new PracticeEnd ();
                m_PracticeEnd.Id = Guid.NewGuid ();
                m_PracticeEnd.ParentId = m_Practice.Id;
                m_PracticeEnd.EndNumber = m_EndCount + 1;
            }

            FillList ();

            m_ScoreControl.SetTargetFace (m_TargetFace);

            SetPoints ();
        }

        public override void Save ()
        {
            m_PracticeEnd.Results = m_ArrowsListView.Arrows.ToList ();

            ATManager.GetInstance ().Persist (m_PracticeEnd);

            Navigation.PopAsync ();
        }

        protected override void OnDisappearing ()
        {
            base.OnDisappearing ();

            m_ScoreControl.ScoreClicked -= Clicked;
            m_ArrowsListView.Dispose ();
        }
    }
}

