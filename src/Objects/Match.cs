using System;
using ATMobile.Interfaces;

namespace ATMobile.Objects
{
    public class Match : AbstractObject, IHasParent
    {
        public Guid ArcherId { get; set; }

        public int MatchNumber { get; set; }

        public int Score { get; set; }

        public string Competitor { get; set; }

        public int CompetitorScore { get; set; }

        public string Note { get; set; }

        public bool Won { get; set; }

        /// <summary>
        /// Gets or sets the Round Id that this match belongs to
        /// </summary>
        /// <value>The parent identifier.</value>
        public Guid ParentId { get; set; }

        public string DisplayText {
            get {

                if (Competitor != null) {
                    if (Won) {
                        return string.Format ("{0} against {1} (Won)", MatchNumber, Competitor);
                    }

                    return string.Format ("{0} against {1}", MatchNumber, Competitor);
                }

                return string.Format ("{0}", MatchNumber);
            }
        }
    }
}

