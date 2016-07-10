﻿using ATMobile.Controls;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class PracticeEndsForm : AbstractListForm
    {
        private Archer m_Archer;
        private Practice m_Practice;
        private Label m_lblArcher;
        private Label m_lblSummary;
        private PracticeEndsListView m_PracticeEnds;

        public PracticeEndsForm () : base ("Practice")
        {
            m_lblArcher = new Label {
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold
            };
            OutsideLayout.Children.Insert (0, m_lblArcher);

            m_lblSummary = new Label {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Arrows: 0, Score: 0"
            };
            OutsideLayout.Children.Insert (1, m_lblSummary);

            m_PracticeEnds = new PracticeEndsListView ();
            m_PracticeEnds.ItemSelected += OnSelected;
            ListFrame.Content = m_PracticeEnds;
        }

        public void SetupForm (Archer _archer, Practice _practice)
        {
            m_Archer = _archer;
            m_Practice = _practice;

            m_lblArcher.Text = m_Archer.FullName;

            if (m_Practice != null) {
                m_PracticeEnds.RefreshList (m_Practice.Id);
            }
        }

        private void SetSummary ()
        {
            int arrows = 0;
            int score = 0;

            foreach (var end in m_PracticeEnds.Ends) {
                foreach (var arrow in end.Results) {
                    arrows += 1;
                    score += arrow.ScoreValue;
                }
            }

            m_lblSummary.Text = string.Format ("Arrows:{0}, Score:{1}", arrows, score);
        }

        public override void Add ()
        {
            PracticeEndForm practiceEnd = new PracticeEndForm ();
            practiceEnd.SetupForm (m_Archer, m_Practice, null, m_PracticeEnds.Ends.Count);
            Navigation.PushAsync (practiceEnd);
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            PracticeEnd end = (PracticeEnd)e.SelectedItem;

            PracticeEndForm practiceEnd = new PracticeEndForm ();
            practiceEnd.SetupForm (m_Archer, m_Practice, end, m_PracticeEnds.Ends.Count);
            Navigation.PushAsync (practiceEnd);
        }

        protected override void OnAppearing ()
        {
            RefreshList ();
        }

        private void RefreshList ()
        {
            m_PracticeEnds.RefreshList (m_Practice.Id);
            SetSummary ();
        }
    }
}

