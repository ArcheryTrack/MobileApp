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
    public class PracticeEndForm : AbstractEntryForm
    {
        private Archer m_Archer;
        private Practice m_Practice;
        private PracticeEnd m_PracticeEnd;
        private int m_EndCount;
        private TargetFace m_TargetFace;

        private Label m_lblPoints;

        private PracticeArrowListView m_ArrowsListView;
        private Grid m_EntryGrid;

        private ArrowButton m_btnX;
        private ArrowButton m_btn10;
        private ArrowButton m_btn9;
        private ArrowButton m_btn8;
        private ArrowButton m_btn7;
        private ArrowButton m_btn6;
        private ArrowButton m_btn5;
        private ArrowButton m_btn4;
        private ArrowButton m_btn3;
        private ArrowButton m_btn2;
        private ArrowButton m_btn1;
        private ArrowButton m_btnM;

        public PracticeEndForm () : base ("End")
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
            m_ArrowsListView = new PracticeArrowListView ();
            frame.Content = m_ArrowsListView;
            layout.Children.Add (frame);

            m_EntryGrid = new Grid ();
            m_EntryGrid.VerticalOptions = LayoutOptions.FillAndExpand;
            m_EntryGrid.HorizontalOptions = LayoutOptions.Center;
            m_EntryGrid.RowDefinitions.Add (new RowDefinition { Height = GridLength.Auto });
            m_EntryGrid.RowDefinitions.Add (new RowDefinition { Height = GridLength.Auto });
            m_EntryGrid.RowDefinitions.Add (new RowDefinition { Height = GridLength.Auto });
            m_EntryGrid.RowDefinitions.Add (new RowDefinition { Height = GridLength.Auto });

            m_EntryGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = GridLength.Auto });
            m_EntryGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = GridLength.Auto });
            m_EntryGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = GridLength.Auto });

            m_btnX = new ArrowButton ("X");
            m_btnX.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btnX, 0, 0);

            m_btn10 = new ArrowButton ("10");
            m_btn10.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn10, 1, 0);

            m_btn9 = new ArrowButton ("9");
            m_btn9.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn9, 2, 0);

            m_btn8 = new ArrowButton ("8");
            m_btn8.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn8, 0, 1);

            m_btn7 = new ArrowButton ("7");
            m_btn7.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn7, 1, 1);

            m_btn6 = new ArrowButton ("6");
            m_btn6.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn6, 2, 1);

            m_btn5 = new ArrowButton ("5");
            m_btn5.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn5, 0, 2);

            m_btn4 = new ArrowButton ("4");
            m_btn4.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn4, 1, 2);

            m_btn3 = new ArrowButton ("3");
            m_btn3.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn3, 2, 2);

            m_btn2 = new ArrowButton ("2");
            m_btn2.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn2, 0, 3);

            m_btn1 = new ArrowButton ("1");
            m_btn1.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn1, 1, 3);

            m_btnM = new ArrowButton ("M");
            m_btnM.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btnM, 2, 3);

            Frame frame2 = new Frame {
                HasShadow = false,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            frame2.Content = m_EntryGrid;
            layout.Children.Add (frame2);
        }

        private void DisableControls ()
        {
            if (m_TargetFace != null) {

                if (m_TargetFace.MinimumPoints > 10
                    || m_TargetFace.MaximumPoints < 10) {
                    m_btn10.IsEnabled = false;
                }

                if (m_TargetFace.MinimumPoints > 9
                    || m_TargetFace.MaximumPoints < 9) {
                    m_btn9.IsEnabled = false;
                }

                if (m_TargetFace.MinimumPoints > 8
                    || m_TargetFace.MaximumPoints < 8) {
                    m_btn8.IsEnabled = false;
                }

                if (m_TargetFace.MinimumPoints > 7
                    || m_TargetFace.MaximumPoints < 7) {
                    m_btn7.IsEnabled = false;
                }

                if (m_TargetFace.MinimumPoints > 6
                    || m_TargetFace.MaximumPoints < 6) {
                    m_btn6.IsEnabled = false;
                }

                if (m_TargetFace.MinimumPoints > 5
                    || m_TargetFace.MaximumPoints < 5) {
                    m_btn5.IsEnabled = false;
                }

                if (m_TargetFace.MinimumPoints > 4
                    || m_TargetFace.MaximumPoints < 4) {
                    m_btn4.IsEnabled = false;
                }

                if (m_TargetFace.MinimumPoints > 3
                    || m_TargetFace.MaximumPoints < 3) {
                    m_btn3.IsEnabled = false;
                }

                if (m_TargetFace.MinimumPoints > 2
                    || m_TargetFace.MaximumPoints < 2) {
                    m_btn2.IsEnabled = false;
                }

                if (m_TargetFace.MinimumPoints > 1
                    || m_TargetFace.MaximumPoints < 1) {
                    m_btn1.IsEnabled = false;
                }
            }
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

            DisableControls ();

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

            m_ArrowsListView.Dispose ();
        }
    }
}

