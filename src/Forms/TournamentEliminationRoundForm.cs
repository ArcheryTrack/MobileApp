using System;
using System.Text;
using ATMobile.Cells;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentEliminationRoundForm : AbstractEntryForm
    {
        private Tournament m_Tournament;
        private Round m_Round;
        private ATDateEntry m_datDate;
        private ATTextEntry m_txtSeed;
        private ATArcherEntry m_entArcher;
        private ATEditor m_txtNote;

        public TournamentEliminationRoundForm () : base ("Edit Elimination Round")
        {
            m_datDate = new ATDateEntry ("Date", "Date");
            InsideLayout.Children.Add (m_datDate);

            m_entArcher = new ATArcherEntry ("Archer", "Select Archer");
            InsideLayout.Children.Add (m_entArcher);

            m_txtSeed = new ATTextEntry {
                Keyboard = Keyboard.Numeric,
                Title = "Seed",
                Placeholder = "Enter starting seed"
            };
            InsideLayout.Children.Add (m_txtSeed);

            m_txtNote = new ATEditor (100, 200);
            InsideLayout.Children.Add (m_txtNote);
        }

        public override void Save ()
        {
            if (m_datDate.SelectedDate.HasValue) {
                m_Round.Date = m_datDate.SelectedDate.Value;
            }

            int seed;
            if (int.TryParse (m_txtSeed.Text, out seed)) {
                m_Round.Seed = seed;
            }

            m_Round.Note = m_txtNote.Text;
            m_Round.ArcherId = m_entArcher.Archer.Id;

            ATManager.GetInstance ().Persist (m_Round);
        }

        public override void ValidateForm (StringBuilder _sb)
        {
            if (m_entArcher.Archer == null) {
                _sb.AppendLine ("An archer is required.");
            }
        }

        public void SetupForm (Tournament _tournament, Round _round)
        {
            m_Tournament = _tournament;
            m_Round = _round;

            m_datDate.SelectedDate = m_Round.Date;

            if (m_Tournament.Archers.Count == 1) {
                Archer archer = ATManager.GetInstance ().GetArcher (m_Tournament.Archers [0]);
                m_entArcher.Archer = archer;
            }

            if (m_Round.Seed.HasValue) {
                m_txtSeed.Text = Convert.ToString (m_Round.Seed);
            }

            m_txtNote.Text = m_Round.Note;

            m_entArcher.PossibleArchers = m_Tournament.Archers;
        }
    }
}

