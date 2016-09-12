using System;
using System.Collections.Generic;
using System.Linq;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class RoundTypeDao : AbstractDao<RoundType>
    {
        public RoundTypeDao (LiteDatabase _db)
            : base (_db)
        {
        }

        public override void BuildIndexes ()
        {
        }

        public override string CollectionName {
            get {
                return "RoundTypes";
            }
        }

        public List<RoundType> GetRoundTypes (Guid _tournamentTypeId)
        {
            List<RoundType> roundTypes = GetChildren (_tournamentTypeId);

            return roundTypes.OrderBy ((RoundType arg) => arg.RoundNumber).ToList ();
        }
    }
}

