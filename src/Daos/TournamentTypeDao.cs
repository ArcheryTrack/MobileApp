using System;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class TournamentTypeDao : AbstractDao<TournamentType>
    {
        public TournamentTypeDao(LiteDatabase _db) 
            : base(_db)
        {
        }

        public override void BuildIndexes()
        {
        }

        public override string CollectionName
        {
            get
            {
                return "TournamentTypes";
            }
        }
    }
}

