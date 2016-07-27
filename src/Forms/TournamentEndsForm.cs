using System;
using System.Collections.Generic;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentEndsForm : AbstractForm
    {
        private StackLayout m_Inside;
        private Label m_lblTournament;
        private Label m_lblRound;

        private StackLayout m_ArcherLayout;
        private Button m_btnPrevious;
        private Button m_btnNext;
        private Label m_lblArcher;

        private Label m_lblSummary;
        private TournamentEndsListView m_TournamentEnds;

        private Round m_Round;
        private Tournament m_Tournament;

        private int m_CurrentArcherIndex;
        private Archer m_CurrentArcher;

        public TournamentEndsForm () : base ("Tournaments")
        {
            m_Inside = new StackLayout {
                Spacing = 5,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };
            OutsideLayout.Children.Add (m_Inside);

            m_lblTournament = new Label {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            m_Inside.Children.Add (m_lblTournament);

            m_lblRound = new Label {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            m_Inside.Children.Add (m_lblRound);

            m_ArcherLayout = new StackLayout {
                Spacing = 5,
                Padding = 5,
                Orientation = StackOrientation.Horizontal
            };
            m_Inside.Children.Add (m_ArcherLayout);

            /* Setup the Archer */
            m_btnPrevious = new Button {
                Text = "<",
                HorizontalOptions = LayoutOptions.Start
            };
            m_btnPrevious.Clicked += PreviousClicked;
            m_ArcherLayout.Children.Add (m_btnPrevious);

            m_lblArcher = new Label {
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
            m_lblSummary = new Label {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Arrows: 0, Score: 0"
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

        }

        private void LoadEnds (Guid _archerId)
        {
            List<TournamentEnd> ends = ATManager.GetInstance ().GetTournamentEnds (m_Round.Id, _archerId);

            if (ends.Count == 0) {
                ends = BuildEnds (m_Tournament, m_Round, _archerId);
            }

            m_TournamentEnds.ItemsSource = ends;
        }

        private void BuildAllEnds (Tournament _tournament, Round _round)
        {
            var manager = ATManager.GetInstance ();

            foreach (var archerId in _tournament.Archers) {
                List<TournamentEnd> ends = manager.GetTournamentEnds (_tournament.Id, archerId);

                if (ends.Count == 0) {
                    BuildEnds (_tournament, _round, archerId);
                }
            }
        }

        private List<TournamentEnd> BuildEnds (Tournament _tournament, Round _round, Guid _archerId)
        {
            var manager = ATManager.GetInstance ();

            List<TournamentEnd> ends = new List<TournamentEnd> ();

            for (int i = 1; i <= _round.ExpectedEnds; i++) {
                TournamentEnd end = new TournamentEnd ();
                end.ArcherId = _archerId;
                end.EndNumber = i;
                end.CreatedDateTime = DateTime.Now;
                end.ParentId = _round.Id;

                ends.Add (end);
                manager.Persist (end);
            }

            return ends;
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            TournamentEnd end = (TournamentEnd)e.SelectedItem;

            TournamentEndForm form = new TournamentEndForm ();
            form.SetupForm (m_CurrentArcher, m_Tournament, m_Round, end);
            Navigation.PushAsync (form, true);
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            SetArcher ();
            SetScore ();
        }
    }
}

