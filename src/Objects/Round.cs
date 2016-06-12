using System;
using ATMobile.Interfaces;

namespace ATMobile.Objects
{
    public class Round : AbstractObject, IHasParent
    {
        public Round ()
        {
        }

        public Guid ParentId { get; set; }
    }
}

