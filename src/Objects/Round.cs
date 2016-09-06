using System;
using ATMobile.Enums;
using ATMobile.Interfaces;
using ATMobile.Managers;

namespace ATMobile.Objects
{
    public class Round : AbstractObject, IHasParent
    {
        public Guid ParentId { get; set; }

        public DateTime Date { get; set; }

        public int RoundNumber { get; set; }

        public CompetitionType CompetitionType { get; set; }

        public int NumberOfEnds { get; set; }

        public int ArrowsPerEnd { get; set; }

        public Distance Distance { get; set; }

        public bool CountX { get; set; }

        public Guid TargetFaceId { get; set; }

        public string Note { get; set; }

        public Guid? RoundTypeId { get; set; }

        /// <summary>
        /// Gets or sets the archer identifier.
        /// Only used for Match Rounds
        /// </summary>
        /// <value>The archer identifier.</value>
        public Guid? ArcherId { get; set; }

        /// <summary>
        /// Gets or sets the Archer's seed.
        /// Only used for match rounds.
        /// </summary>
        /// <value>The seed.</value>
        public int? Seed { get; set; }


        public string RoundText {
            get {
                if (CompetitionType == CompetitionType.Ranking) {
                    return string.Format ("Round Number - {0}", RoundNumber);
                } else {
                    if (ArcherId != null) {
                        Archer archer = ATManager.GetInstance ().GetArcher (ArcherId.Value);

                        if (archer != null) {
                            return string.Format ("Elimination Round for {0}", archer.FullName);
                        }
                    }

                    return string.Format ("Elimination Round", RoundNumber);
                }
            }
        }
    }
}

