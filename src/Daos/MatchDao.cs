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
    }
}

