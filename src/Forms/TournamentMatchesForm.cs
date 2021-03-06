﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ATMobile.Cells;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentMatchesForm : AbstractListForm
    {
        private Tournament m_Tournament;
        private Round m_Round;
        private List<Match> m_Matches;
        private Archer m_Archer;

        private ATLabel m_lblArcher;
        private MatchListView m_lstMatches;

        public TournamentMatchesForm () : base ("Elimination Round")
        {
            AddButton.Text = "Add Match";

            m_lblArcher = new ATLabel {
                Text = "",
                HorizontalTextAlignment = TextAlignment.Center
            };
            OutsideLayout.Children.Insert (0, m_lblArcher);

            m_lstMatches = new MatchListView ();
            m_lstMatches.ItemSelected += MatchSelected;
            ListFrame.Content = m_lstMatches;

            if (Device.Idiom == TargetIdiom.Phone) {
                m_lblArcher.Margin = new Thickness (0, 15, 0, 0);
            } else {
                m_lblArcher.Margin = new Thickness (0, 20, 0, 0);
            }
        }

        public override void Add ()
        {
            TournamentMatchForm matchForm = new TournamentMatchForm ();
            matchForm.SetupForm (m_Tournament, m_Round, null);
            Navigation.PushModalAsync (matchForm);
        }

        public void SetupForm (Tournament _tournament, Round _round)
        {
            ATManager manager = ATManager.GetInstance ();

            m_Tournament = _tournament;
            m_Round = _round;

            if (m_Round.ArcherId != null) {
                m_Archer = manager.GetArcher (m_Round.ArcherId.Value);

                if (m_Archer != null) {
                    m_lblArcher.Text = m_Archer.FullName;
                }
            }

            RefreshList ();
        }

        void RefreshList ()
        {
            m_Matches = ATManager.GetInstance ().GetMatches (m_Round.Id);
            m_lstMatches.ItemsSource = m_Matches;
        }

        void MatchSelected (object sender, SelectedItemChangedEventArgs e)
        {
            Match match = (Match)e.SelectedItem;
            TournamentMatchForm matchForm = new TournamentMatchForm ();
            matchForm.SetupForm (m_Tournament, m_Round, match);
            Navigation.PushModalAsync (matchForm);
        }

        public async Task DeleteMatch (Match _match)
        {
            PublishActionMessage ("Match Delete Selected");

            bool result = await DisplayAlert ("ArcheryTrack", "Are you sure you want to delete this match.  All associated records will also be deleted", "Delete", "Cancel");

            if (result) {
                ATManager.GetInstance ().DeleteMatch (_match.Id);
                RefreshList ();
            }
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            MatchCell.MatchDeleteClicked += DeleteMatch;
            m_Matches = ATManager.GetInstance ().GetMatches (m_Round.Id);
            m_lstMatches.ItemsSource = m_Matches;
        }

        protected override void OnDisappearing ()
        {
            base.OnDisappearing ();
            MatchCell.MatchDeleteClicked -= DeleteMatch;
        }
    }
}

