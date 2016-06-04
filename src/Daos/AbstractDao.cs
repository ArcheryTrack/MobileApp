using System;
using LiteDB;
using ATMobile.Objects;
using System.Collections.Generic;
using System.Linq;

namespace ATMobile.Daos
{
    public abstract class AbstractDao<T> : IDisposable where T:AbstractObject, new()
    {
        private LiteDatabase m_Database;
        protected LiteCollection<T> m_Collection;

        public abstract string CollectionName { get; }

        public abstract void BuildIndexes();

        protected AbstractDao(LiteDatabase _database)
        {
            m_Database = _database;

            BuildCollection();
        }

        private void BuildCollection()
        {
            m_Collection = m_Database.GetCollection<T>(CollectionName);
            m_Collection.EnsureIndex("Guid", true);
            BuildIndexes();
        }

        private void Insert(T _item)
        {
            m_Collection.Insert(_item);
        }

        private void Update(T _item)
        {
            m_Collection.Update(_item);
        }

        public void Persist(T _item)
        {
            T existing = Get(_item.Guid);

            if (existing == null)
            {
                Insert(_item);
            } else
            {
                Update(_item);
            }
        }

        public T Get(Guid _guid)
        {
            Query query = Query.EQ("Guid", _guid.ToString());
            return m_Collection.FindOne(query);
        }

        public List<T> GetAll()
        {
            IEnumerable<T> items = m_Collection.FindAll();
            return items.ToList();
        }

        public void Delete(Guid _guid)
        {
            Query query = Query.EQ("Guid", _guid.ToString());
            m_Collection.Delete(query);
        }

        public void Dispose()
        {
            m_Collection = null;
            m_Database = null;
        }
    }
}

