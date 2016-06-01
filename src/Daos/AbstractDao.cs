using System;
using LiteDB;
using ATMobile.Objects;

namespace ATMobile.Daos
{
	public abstract class AbstractDao<T> : IDisposable where T:AbstractObject
	{
		private readonly LiteDatabase m_Database;
		protected LiteCollection<T> m_Collection;

		public abstract string CollectionName { get; }
		public abstract void BuildIndexes();

		protected AbstractDao (LiteDatabase _database)
		{
			m_Database = _database;

			BuildCollection ();
		}

		private void BuildCollection()
		{
			m_Collection = m_Database.GetCollection<T> (CollectionName);
			m_Collection.EnsureIndex ("Guid", true);
			BuildIndexes();
		}

		public void Insert(T item)
		{
			m_Collection.Insert (item);
		}

	}
}

