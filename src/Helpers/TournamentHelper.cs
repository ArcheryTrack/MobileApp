using System;
using System.Collections.Generic;
using ATMobile.Managers;
using ATMobile.Objects;

namespace ATMobile.Helpers
{
    public static class TournamentHelper
    {
        public static void BuildRounds (Tournament _tournament, Round _round)
        {
            var manager = ATManager.GetInstance ();

            foreach (var archerId in _tournament.Archers) {
                List<TournamentEnd> ends = manager.GetTournamentEnds (_tournament.Id, archerId);

                if (ends.Count == 0) {
                    BuildEnds (_tournament, _round, archerId);
                }
            }
        }

        public static List<TournamentEnd> BuildEnds (Tournament _tournament, Round _round, Guid _archerId)
        {
            var manager = ATManager.GetInstance ();

            List<TournamentEnd> ends = new List<TournamentEnd> ();

            for (int i = 1; i <= _round.ExpectedEnds; i++) {
                TournamentEnd end = new TournamentEnd ();
                end.ArcherId = _archerId;
                end.EndNumber = i;
                end.CreatedDateTime = DateTime.Now;
                end.ParentId = _round.Id;

                ends.Add (end);
                manager.Persist (end);
            }

            return ends;
        }


    }
}

