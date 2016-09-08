using System;
using LiteDB;

namespace ATMobile.Objects
{
    public abstract class AbstractObject
    {
        public AbstractObject ()
        {
            Id = Guid.NewGuid ();

            DateTime now = DateTime.Now;
            CreatedOn = now;
            ModifiedOn = now;
        }

        [BsonId]
        public Guid Id { get; set; }

        /// <summary>
        /// Set automatically upon saving
        /// </summary>
        /// <value>The created on.</value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Set automatically upon saving
        /// </summary>
        /// <value>The modified on.</value>
        public DateTime ModifiedOn { get; set; }
    }
}

