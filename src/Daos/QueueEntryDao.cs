using System;
using System.Collections.Generic;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class QueueEntryDao
    {
        private LiteDatabase m_Database;
        protected LiteCollection<QueueEntry> m_Collection;

        public QueueEntryDao (LiteDatabase _db)
        {
            m_Database = _db;
            m_Collection = m_Database.GetCollection<QueueEntry> ("Queue");
        }

        public void Persist (QueueEntry _queueEntry)
        {
            m_Collection.Insert (_queueEntry);
        }

        public void Delete (Guid _id)
        {

        }

        public List<QueueEntry> QueueEntries ()
        {
            return null;
        }
    }
}

