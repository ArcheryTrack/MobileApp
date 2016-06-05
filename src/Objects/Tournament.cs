using System;

namespace ATMobile.Objects
{
    public class Tournament : AbstractObject
    {
        public Tournament()
        {
        }

        public Guid TournamentType { get; set; }
    }
}

