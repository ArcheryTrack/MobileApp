using System;
using System.Collections.Generic;
using System.Text;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentMatchForm : AbstractEntryForm
    {
        private Tournament m_Tournament;
        private Round m_Round;
        private Match m_Match;
        private Archer m_Archer;

        private ATLabel m_lblArcher;
        private ATTextEntry m_txtOpponent;
        private ATIntegerEntry m_intOpponentScore;
        private ATIntegerEntry m_intArcherScore;
        private ATToggleEntry m_togWon;
        private ATEditor m_txtNote;

        public TournamentMatchForm () : base ("Match")
        {
            m_lblArcher = new ATLabel {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            InsideLayout.Children.Add (m_lblArcher);

            m_txtOpponent = new ATTextEntry {
                Title = "Opponent",
                Placeholder = "Enter opponent's name"
            };
            InsideLayout.Children.Add (m_txtOpponent);

            m_intArcherScore = new ATIntegerEntry {
                Title = "Archer Score"
            };
            InsideLayout.Children.Add (m_intArcherScore);

            m_intOpponentScore = new ATIntegerEntry {
                Title = "Opponent's Score"
            };
            InsideLayout.Children.Add (m_intOpponentScore);

            m_togWon = new ATToggleEntry {
                Title = "Won Match"
            };
            InsideLayout.Children.Add (m_togWon);

            m_txtNote = new ATEditor {
                Title = "Notes"
            };
            InsideLayout.Children.Add (m_txtNote);

        }

        public override void Save ()
        {
            m_Match.Note = m_txtNote.Text;
            m_Match.Opponent = m_txtOpponent.Text;
            m_Match.Score = m_intArcherScore.Value;
            m_Match.OpponentScore = m_intOpponentScore.Value;
            m_Match.Won = m_togWon.IsToggled;

            ATManager.GetInstance ().Persist (m_Match);
        }

        public override void ValidateForm (StringBuilder _sb)
        {

        }

        public void SetupForm (Tournament _tournament, Round _round, Match _match)
        {
            m_Tournament = _tournament;
            m_Round = _round;
            m_Match = _match;

            m_Archer = ATManager.GetInstance ().GetArcher (m_Round.ArcherId.Value);

            if (m_Match == null) {
                List<Match> matches = ATManager.GetInstance ().GetMatches (m_Round.Id);

                m_Match = new Match {
                    ParentId = m_Round.Id,
                    ArcherId = m_Round.ArcherId.Value,
                    MatchNumber = matches.Count + 1
                };
            }

            m_lblArcher.Text = string.Format ("Match {0} - {1}", m_Match.MatchNumber, m_Archer.FullName);
            m_txtOpponent.Text = m_Match.Opponent;
            m_intArcherScore.Value = m_Match.Score;
            m_intOpponentScore.Value = m_Match.OpponentScore;
            m_togWon.IsToggled = m_Match.Won;
            m_txtNote.Text = m_Match.Note;
        }


    }
}

