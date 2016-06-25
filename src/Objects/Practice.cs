using System;
using System.Collections.Generic;
using ATMobile.Interfaces;
using ATMobile.Managers;

namespace ATMobile.Objects
{
    public class Practice : AbstractObject, IHasParent
    {
        public DateTime DateTime { get; set; }

        public Guid? RangeId { get; set; }

        public string RangeName { get; set; }

        public Guid? TargetFaceId { get; set; }

        public int ArrowsPerEnd { get; set; }

        public string Notes { get; set; }

        public int AdditionalArrowsShot { get; set; }

        public bool TrackArrow { get; set; }

        public string DateTimeString {
            get {
                return DateTime.ToString ("g");
            }
        }

        public int TotalArrowsShot {
            get {
                int total = AdditionalArrowsShot;

                List<PracticeEnd> ends = ATManager.GetInstance ().GetPracticeEnds (Id);

                foreach (var item in ends) {
                    total += item.Results.Count;
                }

                return total;
            }
        }

        public Guid ParentId { get; set; }
    }
}

