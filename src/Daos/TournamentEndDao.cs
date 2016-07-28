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

        public List<TournamentEnd> GetTournamentEnds (Guid _tournamentId)
        {
            var tournamentEnds = GetChildren (_tournamentId);

            return tournamentEnds.OrderBy ((TournamentEnd arg) => arg.EndNumber).ToList ();
        }

        public List<TournamentEnd> GetTournamentEnds (Guid _roundId, Guid _archerId)
        {
            Query query = Query.And (Query.EQ ("ParentId", _roundId), Query.EQ ("ArcherId", _archerId));
            var tournamentEnds = m_Collection.Find (query).ToList ();

            return tournamentEnds.OrderBy ((TournamentEnd arg) => arg.EndNumber).ToList ();
        }

        public TournamentEnd GetTournamentEnd (Guid _roundId, Guid _archerId, int _endNumber)
        {
            Query query = Query.And (Query.EQ ("ParentId", _roundId), Query.EQ ("ArcherId", _archerId));
            var tournamentEnds = m_Collection.Find (query).ToList ();

            //TODO - Make part of query.
            foreach (var item in tournamentEnds) {
                if (item.EndNumber == _endNumber) {
                    return item;
                }
            }

            return null;
        }
    }
}

