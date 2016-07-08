using System;
using LiteDB;
using ATMobile.Objects;
using System.Collections.Generic;
using System.Linq;
using ATMobile.Interfaces;

namespace ATMobile.Daos
{
    public abstract class AbstractDao<T> : IDisposable where T : AbstractObject, new()
    {
        private LiteDatabase m_Database;
        protected LiteCollection<T> m_Collection;

        public abstract string CollectionName { get; }

        public abstract void BuildIndexes ();

        protected AbstractDao (LiteDatabase _database)
        {
            m_Database = _database;

            BuildCollection ();
        }

        private void BuildCollection ()
        {
            m_Collection = m_Database.GetCollection<T> (CollectionName);
            BuildIndexes ();
        }

        private void Insert (T _item)
        {
            m_Collection.Insert (_item);
        }

        private void Update (T _item)
        {
            m_Collection.Update (_item);
        }

        public void Persist (T _item)
        {
            if (_item.Id.Equals (Guid.Empty)) {
                throw new ArgumentException ("Empty Guid not allowed");
            }

            T existing = Get (_item.Id);

            if (existing == null) {
                Insert (_item);
            } else {
                Update (_item);
            }
        }

        public T Get (Guid _id)
        {
            Query query = Query.EQ ("_id", _id);
            return m_Collection.FindOne (query);
        }

        public List<T> GetAll ()
        {
            IEnumerable<T> items = m_Collection.FindAll ();
            return items.ToList ();
        }

        public void Delete (Guid _id)
        {
            Query query = Query.EQ ("_id", _id);
            m_Collection.Delete (query);
        }

        public void Dispose ()
        {
            m_Collection = null;
            m_Database = null;
        }

        protected List<T> GetChildren (Guid _parentGuid)
        {
            Query query = Query.EQ ("ParentId", _parentGuid);
            return m_Collection.Find (query).ToList ();
        }
    }
}

