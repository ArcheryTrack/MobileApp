using System;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class ArcherDao : AbstractDao<Archer>
    {
        public ArcherDao(LiteDatabase _db) 
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
                return "Archers";
            }
        }
    }
}

