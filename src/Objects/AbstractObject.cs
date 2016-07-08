using System;
using LiteDB;

namespace ATMobile.Objects
{
    public abstract class AbstractObject
    {
        public AbstractObject ()
        {
            Id = Guid.NewGuid ();
        }

        [BsonId]
        public Guid Id { get; set; }
    }
}

