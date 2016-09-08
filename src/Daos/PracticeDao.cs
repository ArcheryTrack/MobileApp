using System;
using System.Collections.Generic;
using System.Linq;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class PracticeDao : AbstractDao<Practice>
    {
        public PracticeDao (LiteDatabase _db)
            : base (_db)
        {
        }

        public override void BuildIndexes ()
        {

        }

        public override string CollectionName {
            get {
                return "Practices";
            }
        }

        public List<Practice> GetPractices (DateTime startDate, DateTime endDate)
        {
            Query query = Query.Between ("DateTime", startDate, endDate);
            return m_Collection.Find (query).OrderByDescending ((Practice arg) => arg.DateTime).ToList ();
        }

        public List<Practice> GetRecentPractices (DateTime startDate, DateTime endDate)
        {
            Query query = Query.Between ("ModifiedOn", startDate, endDate);
            return m_Collection.Find (query).OrderByDescending ((Practice arg) => arg.DateTime).ToList ();
        }

        public List<Practice> GetPractices (Guid _archerGuid)
        {
            var practices = GetChildren (_archerGuid);

            return practices.OrderByDescending ((Practice arg) => arg.DateTime).ToList ();
        }
    }
}

