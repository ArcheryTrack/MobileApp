using System;
using System.Collections.Generic;
using System.Linq;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class RangeDao : AbstractDao<Range>
    {
        public RangeDao (LiteDatabase _db)
            : base (_db)
        {
        }

        public override void BuildIndexes ()
        {
        }

        public override string CollectionName {
            get {
                return "Ranges";
            }
        }

        public List<Range> GetRanges ()
        {
            var ranges = GetAll ();

            return ranges.OrderBy ((Range arg) => arg.Name).ToList ();
        }
    }
}

