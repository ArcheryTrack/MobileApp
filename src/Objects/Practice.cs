using System;
using ATMobile.Interfaces;

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
                return DateTime.ToString ("d");
            }
        }

        public Guid ParentId { get; set; }
    }
}

