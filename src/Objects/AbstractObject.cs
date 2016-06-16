using System;
using LiteDB;

namespace ATMobile.Objects
{
    public abstract class AbstractObject
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}

