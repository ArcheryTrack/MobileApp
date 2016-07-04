using System;
using System.Collections.Generic;
using System.Text;

namespace ATMobile.Objects
{
    public class TournamentType : AbstractObject
    {
        public TournamentType ()
        {
            Rounds = new List<RoundType> ();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<RoundType> Rounds { get; set; }

        public int RoundCount {
            get {
                return Rounds.Count;
            }
        }

        public string Details {
            get {
                StringBuilder sb = new StringBuilder ();

                foreach (var item in Rounds) {
                    sb.AppendFormat ("{0} ends of {1} arrows,", item.NumberOfEnds, item.ArrowsPerEnd);
                }

                if (sb.Length > 0) {
                    sb.Remove (sb.Length - 1, 1);
                }

                return sb.ToString ();
            }
        }
    }
}
