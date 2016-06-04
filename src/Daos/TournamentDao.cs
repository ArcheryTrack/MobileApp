using System;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class TournamentDao : AbstractDao<Tournament>
    {
        public TournamentDao(LiteDatabase _db) 
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
                return "Tournaments";
            }
        }
    }
}

