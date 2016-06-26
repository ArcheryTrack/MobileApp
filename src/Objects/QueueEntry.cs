using System;
using LiteDB;

namespace ATMobile.Objects
{
    public class QueueEntry
    {
        [BsonId]
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        public string Action { get; set; }

        public string ObjectType { get; set; }

        public string Json { get; set; }
    }
}

