using System;
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

        public List<JournalEntry> GetJournalEntries (Guid _archerId)
        {
            return null;
        }
    }
}

