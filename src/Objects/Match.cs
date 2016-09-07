using System;
using ATMobile.Interfaces;

namespace ATMobile.Objects
{
    public class Match : AbstractObject, IHasParent
    {
        public Guid ArcherId { get; set; }

        public int MatchNumber { get; set; }

        public int Score { get; set; }

        public string Opponent { get; set; }

        public int OpponentScore { get; set; }

        public string Note { get; set; }

        public bool Won { get; set; }

        /// <summary>
        /// Gets or sets the Round Id that this match belongs to
        /// </summary>
        /// <value>The parent identifier.</value>
        public Guid ParentId { get; set; }

        public string DisplayText {
            get {

                if (Opponent != null) {
                    if (Won) {
                        return string.Format ("Match {0} against {1} (Won)", MatchNumber, Opponent);
                    }

                    return string.Format ("Match {0} against {1}", MatchNumber, Opponent);
                }

                return string.Format ("Match {0}", MatchNumber);
            }
        }
    }
}

