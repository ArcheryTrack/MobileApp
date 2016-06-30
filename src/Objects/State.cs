using System;
using ATMobile.Interfaces;

namespace ATMobile.Objects
{
    public class State : AbstractNamedObject, IHasParent
    {
        public Guid ParentId { get; set; }
    }
}

