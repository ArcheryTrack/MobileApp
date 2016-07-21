using System;
using ATMobile.Controls;
using ATMobile.Objects;

namespace ATMobile.Forms
{
    public class RoundsForm : AbstractForm
    {
        private Tournament m_Tournament;

        TournamentRoundsListView m_listRounds;

        public RoundsForm () : base ("Rounds")
        {
            m_listRounds = new TournamentRoundsListView ();
            OutsideLayout.Children.Add (m_listRounds);
        }

        public void SetupForm (Tournament _tournament)
        {
            m_Tournament = _tournament;

            m_listRounds.RefreshList (_tournament.Id);
        }
    }
}

