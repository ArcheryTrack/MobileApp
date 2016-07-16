using System;
using ATMobile.Controls;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentEndsForm : AbstractListForm
    {
        private Label m_lblArcher;
        private Label m_lblSummary;
        private TournamentEndsListView m_TournamentEnds;
        private Tournament m_Tournament;

        public TournamentEndsForm () : base ("Tournaments")
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

            m_TournamentEnds = new TournamentEndsListView ();
            m_TournamentEnds.ItemSelected += OnSelected;
            ListFrame.Content = m_TournamentEnds;
        }

        public void SetupForm (Tournament _tournament)
        {
            m_Tournament = _tournament;
        }

        public override void Add ()
        {

        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}

