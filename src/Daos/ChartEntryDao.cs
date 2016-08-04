using System;
using System.Collections.Generic;
using System.Linq;
using ATMobile.Enums;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class ChartEntryDao : AbstractDao<ChartEntry>
    {
        public ChartEntryDao (LiteDatabase _db)
            : base (_db)
        {
        }

        public override string CollectionName {
            get {
                return "ChartEntries";
            }
        }

        public override void BuildIndexes ()
        {

        }

        public List<ChartEntry> GetChartEntriesForEnds (Guid _practiceId)
        {
            Query query =
                Query.And (Query.EQ ("ParentId", _practiceId),
                           Query.EQ ("ChartEntryTypes", (int)ChartEntryTypes.End));
            return m_Collection.Find (query).OrderBy (r => r.Sequence).ToList ();
        }
    }
}

