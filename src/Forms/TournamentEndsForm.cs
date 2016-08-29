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

        private StackLayout m_ArcherLayout;
        private Button m_btnPrevious;
        private Button m_btnNext;
        private ATLabel m_lblArcher;

        private ATLabel m_lblSummary;
        private TournamentEndsListView m_TournamentEnds;

        private Round m_Round;
        private Tournament m_Tournament;
        private List<TournamentEnd> m_Ends;

        private int m_CurrentArcherIndex;
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
            m_ArcherLayout = new StackLayout {
                Orientation = StackOrientation.Horizontal
            };
            m_Inside.Children.Add (m_ArcherLayout);

            m_btnPrevious = new Button {
                Text = "<",
                HorizontalOptions = LayoutOptions.Start
            };
            m_btnPrevious.Clicked += PreviousClicked;
            m_ArcherLayout.Children.Add (m_btnPrevious);

            m_lblArcher = new ATLabel {
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            m_ArcherLayout.Children.Add (m_lblArcher);

            m_btnNext = new Button {
                Text = ">",
                HorizontalOptions = LayoutOptions.End
            };
            m_btnNext.Clicked += NextClicked;
            m_ArcherLayout.Children.Add (m_btnNext);

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

        public void SetupForm (Tournament _tournament, Round _round)
        {
            m_Tournament = _tournament;
            m_Round = _round;

            m_lblRound.Text = _round.RoundText;
            m_lblTournament.Text = _tournament.Name;

            if (m_Tournament.Archers.Count > 0) {
                m_CurrentArcherIndex = 0;
            } else {
                m_lblArcher.Text = "[No Archers Picked]";
                m_btnNext.IsEnabled = false;
                m_btnNext.IsEnabled = false;
            }

            if (m_Tournament.Archers.Count > 1) {
                m_btnNext.IsEnabled = true;
                m_btnPrevious.IsEnabled = true;
            } else {
                m_btnNext.IsEnabled = false;
                m_btnPrevious.IsEnabled = false;
            }
        }

        void PreviousClicked (object sender, EventArgs e)
        {
            m_CurrentArcherIndex--;

            if (m_CurrentArcherIndex < 0) {
                m_CurrentArcherIndex = m_Tournament.Archers.Count - 1;
            }

            SetArcher ();
        }

        void NextClicked (object sender, EventArgs e)
        {
            m_CurrentArcherIndex++;

            if (m_CurrentArcherIndex >= m_Tournament.Archers.Count) {
                m_CurrentArcherIndex = 0;
            }

            SetArcher ();
        }

        private void SetArcher ()
        {
            Guid archerId = m_Tournament.Archers [m_CurrentArcherIndex];
            m_CurrentArcher = ATManager.GetInstance ().GetArcher (archerId);

            m_lblArcher.Text = m_CurrentArcher.FullName;

            LoadEnds (archerId);
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

            SetArcher ();
            SetScore ();
        }
    }
}

