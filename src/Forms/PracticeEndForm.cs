using System;
using ATMobile.Controls;
using ATMobile.Helpers;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class PracticeEndForm : ContentPage
    {
        private Archer m_Archer;
        private Practice m_Practice;
        private PracticeEnd m_PracticeEnd;
        private int m_EndCount;
        private TargetFace m_TargetFace;

        private StackLayout m_OutsideLayout;
        private StackLayout m_InsideLayout;
        private Button m_btnSave;
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

        public PracticeEndForm ()
        {
            Title = "End";

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

            m_ArrowsListView = new PracticeArrowListView ();
            m_ArrowsListView.HeightRequest = 300;
            m_InsideLayout.Children.Add (m_ArrowsListView);

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

            m_InsideLayout.Children.Add (m_EntryGrid);

            Content = m_OutsideLayout;
        }

        private void Clicked (string value)
        {
            ShotArrow arrow = new ShotArrow ();

            arrow.ArrowNumber = m_PracticeEnd.Results.Count + 1;

            arrow.Score = value;
            if (value == "X") {
                if (m_TargetFace != null) {
                    arrow.ScoreValue = m_TargetFace.MaximumPoints;
                } else {
                    arrow.ScoreValue = -1;
                }
            } else if (value == "M") {
                arrow.ScoreValue = 0;
            } else {
                arrow.ScoreValue = Convert.ToInt32 (value);
            }

            m_PracticeEnd.Results.Add (arrow);

            m_ArrowsListView.ClearList ();
            m_ArrowsListView.Arrows = m_PracticeEnd.Results;
            m_ArrowsListView.RefreshList ();
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

            m_ArrowsListView.Arrows = m_PracticeEnd.Results;
        }

        private void OnSave (object sender, EventArgs e)
        {
            ATManager.GetInstance ().Persist (m_PracticeEnd);

            Navigation.PopAsync ();
        }
    }
}

