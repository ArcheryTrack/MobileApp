using System;
using ATMobile.Delegates;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class ScoreControl : ContentView, IDisposable
    {
        private TargetFace m_TargetFace;

        private Grid m_EntryGrid;

        private TextButton m_btnX;
        private TextButton m_btn10;
        private TextButton m_btn9;
        private TextButton m_btn8;
        private TextButton m_btn7;
        private TextButton m_btn6;
        private TextButton m_btn5;
        private TextButton m_btn4;
        private TextButton m_btn3;
        private TextButton m_btn2;
        private TextButton m_btn1;
        private TextButton m_btnM;

        public ScoreClickedDelegate ScoreClicked;

        public ScoreControl ()
        {
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

            m_btnX = new TextButton ("X");
            m_btnX.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btnX, 0, 0);

            m_btn10 = new TextButton ("10");
            m_btn10.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn10, 1, 0);

            m_btn9 = new TextButton ("9");
            m_btn9.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn9, 2, 0);

            m_btn8 = new TextButton ("8");
            m_btn8.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn8, 0, 1);

            m_btn7 = new TextButton ("7");
            m_btn7.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn7, 1, 1);

            m_btn6 = new TextButton ("6");
            m_btn6.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn6, 2, 1);

            m_btn5 = new TextButton ("5");
            m_btn5.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn5, 0, 2);

            m_btn4 = new TextButton ("4");
            m_btn4.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn4, 1, 2);

            m_btn3 = new TextButton ("3");
            m_btn3.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn3, 2, 2);

            m_btn2 = new TextButton ("2");
            m_btn2.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn2, 0, 3);

            m_btn1 = new TextButton ("1");
            m_btn1.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btn1, 1, 3);

            m_btnM = new TextButton ("M");
            m_btnM.OnClicked += Clicked;
            m_EntryGrid.Children.Add (m_btnM, 2, 3);

            Content = m_EntryGrid;
        }

        private void Clicked (string value)
        {
            var clicked = ScoreClicked;

            if (clicked != null) {
                clicked (value);
            }
        }

        public void SetTargetFace (TargetFace _targetFace)
        {
            m_TargetFace = _targetFace;
            EnableControls ();
            DisableControls ();
        }

        private void EnableControls ()
        {
            m_btnM.IsEnabled = true;
            m_btnX.IsEnabled = true;
            m_btn1.IsEnabled = true;
            m_btn2.IsEnabled = true;
            m_btn3.IsEnabled = true;
            m_btn3.IsEnabled = true;
            m_btn4.IsEnabled = true;
            m_btn5.IsEnabled = true;
            m_btn6.IsEnabled = true;
            m_btn7.IsEnabled = true;
            m_btn8.IsEnabled = true;
            m_btn9.IsEnabled = true;
            m_btn10.IsEnabled = true;
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

        public void Dispose ()
        {
            m_btnX.OnClicked -= Clicked;
            m_btn10.OnClicked -= Clicked;
            m_btn9.OnClicked -= Clicked;
            m_btn8.OnClicked -= Clicked;
            m_btn7.OnClicked -= Clicked;
            m_btn6.OnClicked -= Clicked;
            m_btn5.OnClicked -= Clicked;
            m_btn4.OnClicked -= Clicked;
            m_btn3.OnClicked -= Clicked;
            m_btn2.OnClicked -= Clicked;
            m_btn1.OnClicked -= Clicked;
            m_btnM.OnClicked -= Clicked;
        }
    }
}

