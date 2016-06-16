using System;
using ATMobile.Enums;
using ATMobile.Helpers;

namespace ATMobile.Objects
{
    public class Distance : IComparable
    {
        public DistanceUnits Units { get; set; }

        public double Measurement { get; set; }

        public override string ToString ()
        {
            return string.Format ("{0} {1}", Measurement, Units);
        }

        public int CompareTo (object obj)
        {
            var a = obj as Distance;

            if (a != null) {
                var otherMeters = a.ToMeters ();
                var thisMeters = this.ToMeters ();

                return thisMeters.Measurement.CompareTo (otherMeters.Measurement);
            } else {
                throw new ArgumentException ("Object is not an instance of Distance.");
            }
        }
    }
}

