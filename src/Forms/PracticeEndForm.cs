using System;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class PracticeEndForm : ContentPage
    {
        public Archer m_Archer;
        public Practice m_Practice;
        public PracticeEnd m_PracticeEnd;
        public int m_EndCount;

        private StackLayout m_OutsideLayout;
        private StackLayout m_InsideLayout;
        private Button m_btnSave;
        private PracticeArrowListView m_ArrowsListView;
        private Grid m_EntryGrid;

        private Button m_btnX;
        private Button m_btn10;
        private Button m_btn9;
        private Button m_btn8;
        private Button m_btn7;
        private Button m_btn6;
        private Button m_btn5;
        private Button m_btn4;
        private Button m_btn3;
        private Button m_btn2;
        private Button m_btn1;
        private Button m_btnM;

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

            m_btnX = new Button { Text = "X", BorderWidth = 1, BorderRadius = 5 };
            m_EntryGrid.Children.Add (m_btnX, 0, 0);

            m_btn10 = new Button { Text = "10", BorderWidth = 1, BorderRadius = 5 };
            m_EntryGrid.Children.Add (m_btn10, 1, 0);

            m_btn9 = new Button { Text = "9", BorderWidth = 1, BorderRadius = 5 };
            m_EntryGrid.Children.Add (m_btn9, 2, 0);

            m_btn8 = new Button { Text = "8", BorderWidth = 1, BorderRadius = 5 };
            m_EntryGrid.Children.Add (m_btn8, 0, 1);

            m_btn7 = new Button { Text = "7", BorderWidth = 1, BorderRadius = 5 };
            m_EntryGrid.Children.Add (m_btn7, 1, 1);

            m_btn6 = new Button { Text = "6", BorderWidth = 1, BorderRadius = 5 };
            m_EntryGrid.Children.Add (m_btn6, 2, 1);

            m_btn5 = new Button { Text = "5", BorderWidth = 1, BorderRadius = 5 };
            m_EntryGrid.Children.Add (m_btn5, 0, 2);

            m_btn4 = new Button { Text = "4", BorderWidth = 1, BorderRadius = 5 };
            m_EntryGrid.Children.Add (m_btn4, 1, 2);

            m_btn3 = new Button { Text = "3", BorderWidth = 1, BorderRadius = 5 };
            m_EntryGrid.Children.Add (m_btn3, 2, 2);

            m_btn2 = new Button { Text = "2", BorderWidth = 1, BorderRadius = 5 };
            m_EntryGrid.Children.Add (m_btn2, 0, 3);

            m_btn1 = new Button { Text = "1", BorderWidth = 1, BorderRadius = 5 };
            m_EntryGrid.Children.Add (m_btn1, 1, 3);

            m_btnM = new Button { Text = "M", BorderWidth = 1, BorderRadius = 5 };
            m_EntryGrid.Children.Add (m_btnM, 2, 3);

            m_InsideLayout.Children.Add (m_EntryGrid);

            Content = m_OutsideLayout;
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

