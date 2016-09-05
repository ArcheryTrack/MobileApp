using System;
using System.Collections.Generic;
using ATMobile.Objects;

namespace ATMobile.Forms
{
    public class TournamentMatchesForm : AbstractListForm
    {
        private Tournament m_Tournament;
        private Round m_Round;
        private List<Match> m_Matches;

        public TournamentMatchesForm () : base ("Matches")
        {
        }

        public override void Add ()
        {

        }

        public void SetupForm (Tournament _tournament, Round _round)
        {
            m_Tournament = _tournament;
            m_Round = _round;



        }
    }
}

