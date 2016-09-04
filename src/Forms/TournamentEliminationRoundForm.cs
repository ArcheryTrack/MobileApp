using System;
using System.Text;
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

        public TournamentEliminationRoundForm () : base ("Edit Elimination Round")
        {
            m_datDate = new ATDateEntry ("Date", "Date");
            InsideLayout.Children.Add (m_datDate);

            m_txtSeed = new ATTextEntry {
                Keyboard = Keyboard.Numeric,
                Title = "Seed",
                Placeholder = "Enter starting seed"
            };
            InsideLayout.Children.Add (m_txtSeed);
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

            ATManager.GetInstance ().Persist (m_Round);
        }

        public override void ValidateForm (StringBuilder _sb)
        {

        }

        public void SetupForm (Tournament _tournament, Round _round)
        {
            m_Tournament = _tournament;
            m_Round = _round;

            m_datDate.SelectedDate = m_Round.Date;

            if (m_Round.Seed.HasValue) {
                m_txtSeed.Text = Convert.ToString (m_Round.Seed);
            }
        }
    }
}

