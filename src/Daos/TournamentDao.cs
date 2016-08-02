using System;
using System.Collections.Generic;
using System.Linq;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class TournamentDao : AbstractDao<Tournament>
    {
        public TournamentDao (LiteDatabase _db)
            : base (_db)
        {
        }

        public override void BuildIndexes ()
        {
        }

        public override string CollectionName {
            get {
                return "Tournaments";
            }
        }

        public List<Tournament> GetTournaments (DateTime startDate, DateTime endDate)
        {
            Query query = Query.Between ("EndDateTime", startDate, endDate);
            return m_Collection.Find (query).OrderByDescending ((Tournament arg) => arg.EndDateTime).ToList ();
        }

        public List<Tournament> GetTournamentsByType (Guid _tournamentTypeId)
        {
            Query query = Query.EQ ("TournamentTypeId", _tournamentTypeId);
            return m_Collection.Find (query).OrderByDescending ((Tournament arg) => arg.StartDateTime).ToList ();
        }

        public int GetTournamentsByTypeCount (Guid _tournamentTypeId)
        {
            Query query = Query.EQ ("TournamentTypeId", _tournamentTypeId);
            int count = m_Collection.Count (query);
            return count;
        }
    }
}

