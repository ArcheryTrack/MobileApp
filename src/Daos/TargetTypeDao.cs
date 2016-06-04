using System;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class TargetTypeDao : AbstractDao<TargetType>
    {
        public TargetTypeDao(LiteDatabase _db) 
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
                return "TargetTypes";
            }
        }
    }
}

