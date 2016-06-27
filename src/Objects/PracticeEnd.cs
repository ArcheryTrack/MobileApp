using System;
using ATMobile.Interfaces;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ATMobile.Objects
{
    public class PracticeEnd : AbstractObject, IHasParent
    {
        public PracticeEnd ()
        {
            Results = new List<ShotArrow> ();
        }

        public Guid ParentId { get; set; }

        public int EndNumber { get; set; }

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

