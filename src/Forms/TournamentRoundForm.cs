using System;
using System.Text;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;

namespace ATMobile.Forms
{
    public class TournamentRoundForm : AbstractEntryForm
    {
        private Tournament m_Tournament;
        private Round m_Round;
        private ATDateEntry m_datDate;
        private ATEditor m_txtNote;

        public TournamentRoundForm () : base ("Edit Round")
        {
            m_datDate = new ATDateEntry ("Date", "Date");
            InsideLayout.Children.Add (m_datDate);

            m_txtNote = new ATEditor (100, 300);
            InsideLayout.Children.Add (m_txtNote);
        }

        public override void ValidateForm (StringBuilder _sb)
        {

        }

        public override void Save ()
        {
            m_Round.Date = m_datDate.SelectedDate.Value;
            m_Round.Note = m_txtNote.Text;

            ATManager.GetInstance ().Persist (m_Round);
        }

        public void SetupForm (Tournament _tournament, Round _round)
        {
            m_Tournament = _tournament;
            m_Round = _round;

            m_datDate.SelectedDate = m_Round.Date;
            m_txtNote.Text = m_Round.Note;
        }
    }
}

