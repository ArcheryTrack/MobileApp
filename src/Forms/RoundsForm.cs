using System;
using ATMobile.Objects;

namespace ATMobile.Forms
{
    public class RoundsForm : AbstractForm
    {
        private Tournament m_Tournament;

        public RoundsForm () : base ("Rounds")
        {
        }

        public void SetupForm (Tournament _tournament)
        {
            m_Tournament = _tournament;

        }
    }
}

