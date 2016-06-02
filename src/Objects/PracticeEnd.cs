using System;
using ATMobile.Interfaces;
using System.Collections.Generic;

namespace ATMobile.Objects
{
    public class PracticeEnd : AbstractObject, IHasParent
    {
        public PracticeEnd()
        {
        }

        public Guid ParentGuid { get; set; }

        public List<ShotArrow> Results { get; set; }

    }
}

