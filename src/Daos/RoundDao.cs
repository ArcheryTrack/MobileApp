using System;
using System.Collections.Generic;
using System.Linq;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class RoundDao : AbstractDao<Round>
    {
        public RoundDao (LiteDatabase _db)
            : base (_db)
        {
        }

        public override void BuildIndexes ()
        {
        }

        public override string CollectionName {
            get {
                return "Rounds";
            }
        }

        public List<Round> GetRounds (Guid _tournamentId)
        {
            List<Round> rounds = GetChildren (_tournamentId);

            return rounds.OrderBy ((Round arg) => arg.RoundNumber).ToList ();
        }
    }
}

