using System;
using System.Collections.Generic;
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
            return GetChildren (_tournamentTypeId);
        }
    }
}

