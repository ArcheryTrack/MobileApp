using System;
using System.Collections.Generic;
using ATMobile.Objects;
using LiteDB;
using System.Linq;

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
            Query query = Query.EQ ("_id", _id);
            m_Collection.Delete (query);
        }

        public List<QueueEntry> QueueEntries ()
        {
            var result = m_Collection.FindAll ().OrderBy (r => r.DateTime).ToList ();

            return result;
        }

        public void BuildIndexes ()
        {

        }

    }
}

