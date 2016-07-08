using System.Collections.Generic;
using System.Text;
using ATMobile.Managers;

namespace ATMobile.Objects
{
    public class TournamentType : AbstractObject
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details {
            get {
                List<RoundType> roundTypes = ATManager.GetInstance ().GetRoundTypes (Id);

                int totalArrows = 0;

                foreach (var item in roundTypes) {
                    totalArrows += item.NumberOfEnds * item.ArrowsPerEnd;
                }

                return string.Format ("{0} rounds totalling {1} arrows", roundTypes.Count, totalArrows);
            }
        }
    }
}
