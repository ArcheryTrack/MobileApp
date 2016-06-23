using System;
using System.Collections.Generic;
using System.Linq;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class PracticeEndDao : AbstractDao<PracticeEnd>
    {
        public PracticeEndDao (LiteDatabase _db)
            : base (_db)
        {
        }

        public override void BuildIndexes ()
        {
        }

        public override string CollectionName {
            get {
                return "PracticeEnds";
            }
        }

        public List<PracticeEnd> GetPracticeEnds (Guid _practiceGuid)
        {
            var practices = GetChildren (_practiceGuid);

            return practices.OrderBy ((PracticeEnd arg) => arg.EndNumber).ToList ();
        }
    }
}

