using System;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class PracticeEndDao : AbstractDao<PracticeEnd>
    {
        public PracticeEndDao(LiteDatabase _db) 
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
                return "PracticeEnds";
            }
        }
    }
}

