using System;
using System.Collections.Generic;
using System.Linq;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class MatchDao : AbstractDao<Match>
    {
        public MatchDao (LiteDatabase _db)
            : base (_db)
        {
        }

        public override void BuildIndexes ()
        {
        }

        public override string CollectionName {
            get {
                return "Matches";
            }
        }

        public List<Match> GetMatches (Guid _roundId)
        {
            var practices = GetChildren (_roundId);

            return practices.OrderBy ((Match arg) => arg.MatchNumber).ToList ();
        }

        public List<Match> GetMatches (Guid _roundId, Guid _archerId)
        {
            Query query = Query.And (Query.EQ ("ParentId", _roundId), Query.EQ ("ArcherId", _archerId));
            var matches = m_Collection.Find (query).ToList ();

            return matches.OrderBy ((Match arg) => arg.MatchNumber).ToList ();
        }
    }
}

