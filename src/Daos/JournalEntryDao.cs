using System;
using System.Linq;
using System.Collections.Generic;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class JournalEntryDao : AbstractDao<JournalEntry>
    {
        public JournalEntryDao (LiteDatabase _db)
            : base (_db)
        {
        }

        public override string CollectionName {
            get {
                return "JournalEntries";
            }
        }

        public override void BuildIndexes ()
        {

        }

        /// <summary>
        /// Returns all journal entries
        /// </summary>
        /// <returns>The journal entries.</returns>
        /// <param name="_archerId">Archer identifier.</param>
        public List<JournalEntry> GetJournalEntries (Guid _archerId)
        {
            return m_Collection.Find ((JournalEntry entry) => (entry.ParentId == _archerId))
                               .OrderBy ((JournalEntry arg) => arg.DateTime)
                               .ToList ();
        }

        /// <summary>
        /// Returns the requested number of journale entries starting at requested location
        /// </summary>
        /// <returns>The journal entries.</returns>
        /// <param name="_archerId">Archer identifier.</param>
        /// <param name="start">Start.</param>
        /// <param name="count">Count.</param>
        public List<JournalEntry> GetJournalEntries (Guid _archerId, int start, int count)
        {
            return m_Collection.Find ((JournalEntry entry) => (entry.ParentId == _archerId))
                               .OrderBy ((JournalEntry arg) => arg.DateTime)
                               .Skip (start)
                               .Take (count)
                               .ToList ();
        }
    }
}

