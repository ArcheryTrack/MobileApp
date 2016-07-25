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
        private Label m_lblArcher;
        private Label m_lblSummary;
        private Round m_Round;
        private TournamentEndsListView m_TournamentEnds;
        private Tournament m_Tournament;

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

            m_lblArcher = new Label {
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold
            };
            m_Inside.Children.Add (m_lblArcher);

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
                Guid archerId = m_Tournament.Archers [0];

                m_CurrentArcher = ATManager.GetInstance ().GetArcher (archerId);

                SetArcher ();
            } else {
                m_lblArcher.Text = "[No Archers Picked]";
            }

            SetScore ();
        }

        private void SetArcher ()
        {
            m_lblArcher.Text = m_CurrentArcher.FullName;
        }

        private void SetScore ()
        {

        }

        private void BuildEnds (Tournament _tournament, Round _round)
        {
            var manager = ATManager.GetInstance ();

            foreach (var archerId in _tournament.Archers) {
                List<TournamentEnd> ends = manager.GetTournamentEnds (_tournament.Id, archerId);

                if (ends.Count == 0) {
                    for (int i = 1; i <= _round.ExpectedEnds; i++) {
                        TournamentEnd end = new TournamentEnd ();
                        end.ArcherId = archerId;
                        end.EndNumber = i;
                        end.CreatedDateTime = DateTime.Now;
                        end.ParentId = _round.Id;

                        manager.Persist (end);
                    }
                }
            }
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {

        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            SetArcher ();
            SetScore ();
        }
    }
}

