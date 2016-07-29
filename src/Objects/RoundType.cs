using System;
using ATMobile.Interfaces;

namespace ATMobile.Objects
{
    public class RoundType : AbstractObject, IHasParent
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid ParentId { get; set; }

        public int NumberOfEnds { get; set; }

        public Distance Distance { get; set; }

        public int ArrowsPerEnd { get; set; }

        public bool CountX { get; set; }

        public Guid TargetFaceId { get; set; }

        public string Details {
            get {
                return string.Format ("{0} ends of {1} arrows", NumberOfEnds, ArrowsPerEnd);
            }
        }
    }
}

