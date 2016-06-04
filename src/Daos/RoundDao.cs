using System;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class RoundDao : AbstractDao<Round>
    {
        public RoundDao(LiteDatabase _db) 
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
                return "Rounds";
            }
        }
    }
}

