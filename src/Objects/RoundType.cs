using System;
using ATMobile.Interfaces;

namespace ATMobile.Objects
{
    public class RoundType : AbstractObject, IHasParent
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid ParentGuid { get; set; }

        public int NumberOfEnds { get; set; }

        public Distance Distance { get; set; }

        public int ArrowsPerEnd { get; set; }

        public bool CountX { get; set; }

    }
}

