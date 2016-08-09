using System;
using ATMobile.Interfaces;

namespace ATMobile.Objects
{
    public class JournalEntry : AbstractObject, IHasParent
    {
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Archer
        /// </summary>
        /// <value>The parent identifier.</value>
        public Guid ParentId { get; set; }

        public string EntryText { get; set; }
    }
}

