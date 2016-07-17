using System;
using System.Collections.Generic;
using System.Linq;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class TournamentEndDao : AbstractDao<TournamentEnd>
    {
        public TournamentEndDao (LiteDatabase _db)
            : base (_db)
        {
        }

        public override void BuildIndexes ()
        {
        }

        public override string CollectionName {
            get {
                return "TournamentEnds";
            }
        }

        public List<TournamentEnd> GetPracticeEnds (Guid _tournamentId)
        {
            var tournamentEnds = GetChildren (_tournamentId);

            return tournamentEnds.OrderBy ((TournamentEnd arg) => arg.EndNumber).ToList ();
        }

        public List<TournamentEnd> GetPracticeEnds (Guid _tournamentId, Guid _archerId)
        {
            Query query = Query.And (Query.EQ ("ParentId", _tournamentId), Query.EQ ("ArcherId", _archerId));
            var tournamentEnds = m_Collection.Find (query).ToList ();

            return tournamentEnds.OrderBy ((TournamentEnd arg) => arg.EndNumber).ToList ();
        }
    }
}

