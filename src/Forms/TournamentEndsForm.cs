using System;
using System.Collections.Generic;
using ATMobile.Controls;
using ATMobile.Helpers;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentEndsForm : AbstractForm
    {
        private StackLayout m_Inside;
        private ATLabel m_lblTournament;
        private ATLabel m_lblRound;

        private ArcherBar m_ArcherBar;

        private ATLabel m_lblSummary;
        private TournamentEndsListView m_TournamentEnds;

        private Round m_Round;
        private Tournament m_Tournament;
        private List<TournamentEnd> m_Ends;

        private Archer m_CurrentArcher;

        public TournamentEndsForm () : base ("Ends")
        {
            m_Inside = new StackLayout {
                Spacing = 5,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5,
                Margin = new Thickness (20, 10, 20, 20)
            };
            OutsideLayout.Children.Add (m_Inside);

            m_lblTournament = new ATLabel {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            m_Inside.Children.Add (m_lblTournament);

            m_lblRound = new ATLabel {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            m_Inside.Children.Add (m_lblRound);

            /* Setup the Archer */
            m_ArcherBar = new ArcherBar ();
            m_ArcherBar.ArcherPicked += ArcherPicked;
            m_Inside.Children.Add (m_ArcherBar);

            /* Setup the round summary */
            m_lblSummary = new ATLabel {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Arrows Shot: 0, Score: 0",
                Margin = new Thickness (0, 0, 0, 10)
            };
            m_Inside.Children.Add (m_lblSummary);

            Frame tournamentEndsFrame = new Frame {
                HasShadow = false,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            m_Inside.Children.Add (tournamentEndsFrame);

            m_TournamentEnds = new TournamentEndsListView ();
            m_TournamentEnds.ItemSelected += OnSelected;
            tournamentEndsFrame.Content = m_TournamentEnds;
        }

        private void ArcherPicked (Archer _archer)
        {
            m_CurrentArcher = _archer;

            LoadEnds (_archer.Id);
        }

        public void SetupForm (Tournament _tournament, Round _round)
        {
            m_Tournament = _tournament;
            m_Round = _round;

            m_lblRound.Text = _round.RoundText;
            m_lblTournament.Text = _tournament.Name;

            List<Archer> archers = ATManager.GetInstance ().GetArchers (_tournament.Archers);
            m_ArcherBar.Archers = archers;
            m_CurrentArcher = m_ArcherBar.CurrentArcher;
        }

        private void SetScore ()
        {
            int arrows = 0;
            int score = 0;

            foreach (var end in m_Ends) {

                score += end.TotalScore;
                arrows += end.ArrowsScored;
            }

            m_lblSummary.Text = string.Format ("Arrows Shot: {0}, Score: {1}", arrows, score);
        }

        private void LoadEnds (Guid _archerId)
        {
            m_Ends = ATManager.GetInstance ().GetTournamentEnds (m_Round.Id, _archerId);

            if (m_Ends.Count == 0) {
                m_Ends = TournamentHelper.BuildEnds (m_Tournament, m_Round, _archerId);
            }

            m_TournamentEnds.ItemsSource = m_Ends;
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            TournamentEnd end = (TournamentEnd)e.SelectedItem;

            TournamentEndForm form = new TournamentEndForm ();
            form.SetupForm (m_CurrentArcher, m_Tournament, m_Round, end);

            PublishActionMessage ("Tournament End Selected");

            Navigation.PushModalAsync (form, true);
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            LoadEnds (m_CurrentArcher.Id);
            SetScore ();
        }
    }
}

