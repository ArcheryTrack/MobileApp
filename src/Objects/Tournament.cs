using System;
using System.Collections.Generic;

namespace ATMobile.Objects
{
    public class Tournament : AbstractObject
    {
        public Tournament ()
        {
            Archers = new List<Guid> ();
        }

        public string Name { get; set; }

        public Guid RangeId { get; set; }

        public Guid TournamentType { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public List<Guid> Archers { get; set; }
    }
}

