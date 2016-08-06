using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATMobile.Interfaces;

namespace ATMobile.Objects
{
    public class TournamentEnd : AbstractObject, IHasParent
    {
        public TournamentEnd ()
        {
            Results = new List<ShotArrow> ();
        }

        public Guid ParentId { get; set; }

        public Guid ArcherId { get; set; }

        public int EndNumber { get; set; }

        public string Note { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public List<ShotArrow> Results { get; set; }

        public List<ShotArrow> SortedResults {
            get {
                List<ShotArrow> sorted = Results.OrderByDescending (r => r.SortValue).ToList ();

                return sorted;
            }
        }

        public List<ShotArrow> SortedByArrowNumber {
            get {
                List<ShotArrow> sorted = Results.OrderBy (r => r.ArrowNumber).ToList ();

                return sorted;
            }
        }

        public string CellOutput {
            get {
                StringBuilder sb = new StringBuilder ();

                sb.Append ("End ");
                sb.Append (EndNumber);
                sb.Append (" : ");

                if (Results.Count == 0) {
                    sb.Append ("Not Shot");
                } else {
                    sb.Append (ResultsString);
                    sb.Append (" = ");
                    sb.Append (TotalScore);
                }

                return sb.ToString ();
            }
        }

        public string ResultsString {
            get {
                StringBuilder sb = new StringBuilder ();

                var sorted = SortedResults;

                foreach (var item in sorted) {
                    sb.Append (item.Score);
                    sb.Append (", ");
                }

                if (sb.Length > 0) {
                    sb.Remove (sb.Length - 2, 2);
                }


                return sb.ToString ();
            }
        }

        public int TotalScore {
            get {
                int total = 0;

                foreach (var item in Results) {
                    if (item.ScoreValue > 0) {
                        total += item.ScoreValue;
                    }
                }

                return total;
            }
        }
    }
}

