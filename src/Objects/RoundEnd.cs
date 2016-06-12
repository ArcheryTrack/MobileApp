using System;
using ATMobile.Interfaces;
using System.Collections.Generic;

namespace ATMobile.Objects
{
    public class RoundEnd : AbstractObject, IHasParent
    {
        public RoundEnd ()
        {
            Arrows = new List<ShotArrow> ();
        }

        public Guid ParentId { get; set; }

        public List<ShotArrow> Arrows { get; set; }
    }
}

