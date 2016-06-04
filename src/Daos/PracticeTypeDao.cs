using System;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class PracticeTypeDao : AbstractDao<PracticeType>
    {
        public PracticeTypeDao(LiteDatabase _db)
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
                return "PracticeTypes";
            }
        }
    }
}

