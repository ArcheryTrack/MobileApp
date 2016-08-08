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

        public List<ChartEntry> GetChartEntriesForEnds (Guid _endParentId, Guid _archerId)
        {
            Query query =
                Query.And (
                    Query.And (
                        Query.EQ ("ParentId", _endParentId),
                        Query.EQ ("ChartEntryType", (int)ChartEntryTypes.End)),
                    Query.EQ ("ArcherId", _archerId));

            List<ChartEntry> entries = m_Collection.FindAll ().ToList ();

            return m_Collection.Find ((ChartEntry x) =>
                                      x.ParentId == _endParentId
                                      && x.ArcherId == _archerId
                                      && x.ChartEntryType == ChartEntryTypes.End).ToList ();
        }

        public List<ChartEntry> GetChartEntriesForRounds (Guid _tournamentId, Guid _archerId)
        {
            Query query =
                Query.And (
                    Query.And (
                        Query.EQ ("ParentId", _tournamentId),
                        Query.EQ ("ChartEntryType", (int)ChartEntryTypes.Round)),
                    Query.EQ ("ArcherId", _archerId));
            return m_Collection.Find (query).OrderBy (r => r.Sequence).ToList ();
        }

        public List<ChartEntry> GetChartEntriesForPractices (DateTime _startDate, DateTime _endDate, Guid _archerId)
        {
            Query query =
                Query.And (
                    Query.And (
                        Query.Between ("Date", _endDate, _endDate),
                        Query.EQ ("ChartEntryType", (int)ChartEntryTypes.Practice)),
                    Query.EQ ("ArcherId", _archerId));
            return m_Collection.Find (query).OrderBy (r => r.Date).ToList ();
        }

        public List<ChartEntry> GetChartEntriesForTournaments (DateTime _startDate, DateTime _endDate, Guid _archerId)
        {
            Query query =
                Query.And (
                    Query.And (
                        Query.Between ("Date", _endDate, _endDate),
                        Query.EQ ("ChartEntryType", (int)ChartEntryTypes.Tournament)),
                    Query.EQ ("ArcherId", _archerId));
            return m_Collection.Find (query).OrderBy (r => r.Date).ToList ();
        }
    }
}

