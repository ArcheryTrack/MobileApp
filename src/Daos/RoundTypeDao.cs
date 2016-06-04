using System;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class RoundTypeDao : AbstractDao<Round>
    {
        public RoundTypeDao(LiteDatabase _db) 
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
                return "RoundTypes";
            }
        }
    }
}

