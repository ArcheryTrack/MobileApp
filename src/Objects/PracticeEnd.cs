using System;
using ATMobile.Interfaces;
using System.Collections.Generic;
using System.Text;

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

        public string ResultsString {
            get {
                StringBuilder sb = new StringBuilder ();

                foreach (var item in Results) {
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

