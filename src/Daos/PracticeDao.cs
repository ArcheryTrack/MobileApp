using System;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class PracticeDao : AbstractDao<Practice>
    {
        public PracticeDao(LiteDatabase _db) 
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
                return "Practices";
            }
        }
    }
}

