using System;
using System.Collections.Generic;
using System.Linq;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class TournamentDao : AbstractDao<Tournament>
    {
        public TournamentDao (LiteDatabase _db)
            : base (_db)
        {
        }

        public override void BuildIndexes ()
        {
        }

        public override string CollectionName {
            get {
                return "Tournaments";
            }
        }

        public List<Tournament> GetTournaments (Guid _archerGuid)
        {
            //TODO rewrite
            //var tournaments = GetChildren (_archerGuid);

            //return tournaments.OrderByDescending ((Tournament arg) => arg.StartDateTime).ToList ();

            return null;
        }
    }
}

