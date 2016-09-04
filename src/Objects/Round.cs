using System;
using ATMobile.Enums;
using ATMobile.Interfaces;

namespace ATMobile.Objects
{
    public class Round : AbstractObject, IHasParent
    {
        public Guid ParentId { get; set; }

        public DateTime Date { get; set; }

        public int RoundNumber { get; set; }

        public CompetitionType CompetitionType { get; set; }

        public int? Seed { get; set; }

        public int NumberOfEnds { get; set; }

        public int ArrowsPerEnd { get; set; }

        public Distance Distance { get; set; }

        public bool CountX { get; set; }

        public Guid TargetFaceId { get; set; }

        public string Note { get; set; }

        public Guid? RoundTypeId { get; set; }

        public string RoundText {
            get {
                return string.Format ("Round Number - {0}", RoundNumber);
            }
        }
    }
}

