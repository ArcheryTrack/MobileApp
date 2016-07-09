using ATMobile.Controls;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class PracticeEndsForm : AbstractListForm
    {
        private Archer m_Archer;
        private Practice m_Practice;
        private Label m_lblArcher;
        private PracticeEndsListView m_PracticeEnds;

        public PracticeEndsForm () : base ("Practice")
        {
            m_lblArcher = new Label ();
            OutsideLayout.Children.Insert (0, m_lblArcher);

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
        }
    }
}

