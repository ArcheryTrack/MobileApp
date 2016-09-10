using System;
using ATMobile.Cells;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using ATMobile.PickerForms;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class PracticeEndsForm : AbstractListForm, IDisposable
    {
        private Archer m_Archer;
        private Practice m_Practice;
        private ATLabel m_lblArcher;
        private ATLabel m_lblSummary;
        private PracticeEndsListView m_PracticeEnds;

        public PracticeEndsForm () : base ("Practice")
        {
            m_lblArcher = new ATLabel {
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold
            };
            OutsideLayout.Children.Insert (0, m_lblArcher);

            m_lblSummary = new ATLabel {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Arrows: 0, Score: 0"
            };
            OutsideLayout.Children.Insert (1, m_lblSummary);

            if (Device.Idiom == TargetIdiom.Phone) {
                m_lblArcher.Margin = new Thickness (0, 10, 0, 0);
            } else {
                m_lblArcher.Margin = new Thickness (0, 20, 0, 0);
            }

            m_PracticeEnds = new PracticeEndsListView ();
            m_PracticeEnds.ItemSelected += EditPracticeEnd;
            ListFrame.Content = m_PracticeEnds;

            PracticeEndCell.PracticeEndDeleteClicked += DeletePracticeEnd;
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

            m_lblSummary.Text = string.Format ("Arrows: {0}, Score: {1}", arrows, score);
        }

        public override void Add ()
        {
            PracticeEndForm practiceEnd = new PracticeEndForm ();
            practiceEnd.SetupForm (m_Archer, m_Practice, null, m_PracticeEnds.Ends.Count);

            Navigation.PushModalAsync (practiceEnd);
        }

        void EditPracticeEnd (object sender, SelectedItemChangedEventArgs e)
        {
            PracticeEnd end = (PracticeEnd)e.SelectedItem;

            PracticeEndForm practiceEnd = new PracticeEndForm ();
            practiceEnd.SetupForm (m_Archer, m_Practice, end, m_PracticeEnds.Ends.Count);

            PublishActionMessage ("Practice End Selected");

            Navigation.PushModalAsync (practiceEnd);
        }

        public async void DeletePracticeEnd (PracticeEnd _practiceEnd)
        {
            PublishActionMessage ("Practice End Delete Selected");

            bool result = await DisplayAlert ("ArcheryTrack", "Are you sure you want to delete this practice end.  All associated records will also be deleted", "Delete", "Cancel");

            if (result) {
                ATManager manager = ATManager.GetInstance ();

                manager.DeletePracticeEnd (_practiceEnd.Id);
                manager.RenumberPracticeEnds (_practiceEnd.ParentId);

                RefreshList ();
            }
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

        public void Dispose ()
        {
            PracticeEndCell.PracticeEndDeleteClicked -= DeletePracticeEnd;
        }
    }
}

