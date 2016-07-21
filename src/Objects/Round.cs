using System;
using ATMobile.Interfaces;

namespace ATMobile.Objects
{
    public class Round : AbstractObject, IHasParent
    {
        public int RoundNumber { get; set; }

        public int ExpectedEnds { get; set; }

        public int ExpectedArrowsPerEnd { get; set; }

        public Guid ParentId { get; set; }

        public Distance Distance { get; set; }

        public string RoundText 
        {
            get {
                return string.Format ("Round Number - {0}", RoundNumber);
            }
        }
    }
}

