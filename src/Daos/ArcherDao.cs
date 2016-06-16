using System;
using System.Linq;
using System.Collections.Generic;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class ArcherDao : AbstractDao<Archer>
    {
        public ArcherDao (LiteDatabase _db)
            : base (_db)
        {
        }

        public override void BuildIndexes ()
        {

        }

        public override string CollectionName {
            get {
                return "Archers";
            }
        }

        public List<Archer> GetArchers ()
        {
            List<Archer> archers = GetAll ();

            return archers.OrderBy ((Archer arg) => arg.FullName).ToList ();
        }
    }
}

