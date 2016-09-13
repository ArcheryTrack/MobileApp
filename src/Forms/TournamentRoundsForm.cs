using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Controls;
using ATMobile.Enums;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentRoundsForm : AbstractForm
    {
        private Tournament m_Tournament;

        private ATButtonBar m_ButtonBar;
        private TournamentRoundsListView m_listRounds;
        private ATLabel m_lblTournamentTitle;
        private Button m_btnAddRound;

        public TournamentRoundsForm () : base ("Rounds")
        {
            m_lblTournamentTitle = new ATLabel {
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            OutsideLayout.Children.Add (m_lblTournamentTitle);

            m_ButtonBar = new ATButtonBar ();
            m_btnAddRound = m_ButtonBar.Add ("Add Round", LayoutOptions.CenterAndExpand);
            m_btnAddRound.Clicked += AddRound_Clicked;
            OutsideLayout.Children.Add (m_ButtonBar);

            Frame frame = new Frame {
                HasShadow = false,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            m_listRounds = new TournamentRoundsListView ();
            m_listRounds.ItemSelected += OnSelected;
            frame.Content = m_listRounds;

            if (Device.Idiom == TargetIdiom.Phone) {
                m_lblTournamentTitle.Margin = new Thickness (20, 15, 20, 0);
                frame.Margin = new Thickness (20, 10, 20, 20);
            } else {
                m_lblTournamentTitle.Margin = new Thickness (20, 15, 20, 0);
                frame.Margin = new Thickness (20, 15, 20, 20);
            }

            OutsideLayout.Children.Add (frame);
        }

        public void EditRound (Round _round)
        {
            if (_round.CompetitionType == CompetitionType.Ranking) {
                TournamentRoundForm editRound = new TournamentRoundForm ();
                editRound.SetupForm (m_Tournament, _round);
                PublishActionMessage ("Ranking Round Edit Selected");
                Navigation.PushModalAsync (editRound);
            } else {
                TournamentEliminationRoundForm form = new TournamentEliminationRoundForm ();
                form.SetupForm (m_Tournament, _round);
                PublishActionMessage ("Elimination Round Edit Selected");
                Navigation.PushModalAsync (form);
            }
        }

        public async void DeleteRound (Round _round)
        {
            if (_round.CompetitionType == CompetitionType.Ranking) {
                PublishActionMessage ("Ranking Round Delete Selected");
            } else {
                PublishActionMessage ("Elimination Round Delete Selected");
            }

            bool result = await DisplayAlert ("ArcheryTrack", "Are you sure you want to delete this round.  All associated records will also be deleted", "Delete", "Cancel");

            if (result) {
                ATManager.GetInstance ().DeleteRound (_round.Id);
                m_listRounds.RefreshList (m_Tournament.Id);
            }
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            Round round = (Round)e.SelectedItem;

            if (round.CompetitionType == CompetitionType.Ranking) {
                TournamentEndsForm tournamentEnds = new TournamentEndsForm ();
                tournamentEnds.SetupForm (m_Tournament, round);
                PublishActionMessage ("Ranking Round Selected");
                Navigation.PushAsync (tournamentEnds);
            } else {
                TournamentMatchesForm tournamentMatches = new TournamentMatchesForm ();
                tournamentMatches.SetupForm (m_Tournament, round);
                PublishActionMessage ("Elimination Round Selected");
                Navigation.PushAsync (tournamentMatches);
            }
        }

        public void SetupForm (Tournament _tournament)
        {
            m_Tournament = _tournament;

            if (_tournament.Name != null) {
                m_lblTournamentTitle.Text = _tournament.Name;
            } else {
                m_lblTournamentTitle.Text = "[Name not specified]";
            }
        }

        async void AddRound_Clicked (object sender, EventArgs e)
        {
            PublishActionMessage ("Round Added");

            var action = await DisplayActionSheet ("What type of Round", "Cancel", null, "Ranking Round", "Elimination Round");

            Round round = new Round {
                ParentId = m_Tournament.Id,
                RoundNumber = GetNextRoundNumber ()
            };

            DateTime now = DateTime.Now;
            if (now > m_Tournament.StartDateTime) {
                round.Date = now.Date;
            } else {
                round.Date = m_Tournament.StartDateTime;
            }

            if (action == "Ranking Round") {
                round.CompetitionType = CompetitionType.Ranking;

                var form = new TournamentRoundForm ();
                form.SetupForm (m_Tournament, round);
                await Navigation.PushModalAsync (form);

            } else if (action == "Elimination Round") {
                round.CompetitionType = CompetitionType.Elimination;

                var form = new TournamentEliminationRoundForm ();
                form.SetupForm (m_Tournament, round);
                await Navigation.PushModalAsync (form);
            }
        }

        public int GetNextRoundNumber ()
        {
            List<Round> rounds = ATManager.GetInstance ().GetRounds (m_Tournament.Id);

            int max = 0;

            foreach (var round in rounds) {
                if (round.RoundNumber > max) {
                    max = round.RoundNumber;
                }
            }

            return max + 1;
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            TournamentRoundCell.RoundEditClicked += EditRound;
            TournamentRoundCell.RoundDeleteClicked += DeleteRound;
            m_listRounds.RefreshList (m_Tournament.Id);
        }

        protected override void OnDisappearing ()
        {
            base.OnDisappearing ();
            TournamentRoundCell.RoundEditClicked -= EditRound;
            TournamentRoundCell.RoundDeleteClicked -= DeleteRound;
        }
    }
}

