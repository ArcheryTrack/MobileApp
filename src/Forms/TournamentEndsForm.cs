using System;
using ATMobile.Controls;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentEndsForm : AbstractListForm
    {
        private Label m_lblTournament;
        private Label m_lblRound;
        private Label m_lblArcher;
        private Label m_lblSummary;
        private Round m_Round;
        private TournamentEndsListView m_TournamentEnds;
        private Tournament m_Tournament;

        public TournamentEndsForm () : base ("Tournaments")
        {
            m_lblTournament = new Label {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            OutsideLayout.Children.Insert(0, m_lblTournament);

            m_lblRound = new Label {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            OutsideLayout.Children.Insert (1, m_lblRound);

            m_lblArcher = new Label {
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold
            };
            OutsideLayout.Children.Insert (2, m_lblArcher);

            m_lblSummary = new Label {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Arrows: 0, Score: 0"
            };
            OutsideLayout.Children.Insert (3, m_lblSummary);

            m_TournamentEnds = new TournamentEndsListView ();
            m_TournamentEnds.ItemSelected += OnSelected;
            ListFrame.Content = m_TournamentEnds;
        }

        public void SetupForm (Tournament _tournament, Round _round)
        {
            m_Tournament = _tournament;
            m_Round = _round;

            m_lblRound.Text = _round.RoundText;
            m_lblTournament.Text = _tournament.Name;
        }

        public override void Add ()
        {

        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}

