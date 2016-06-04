using System;
using ATMobile.Enums;

namespace ATMobile.Objects
{
    public class Distance
    {
        public Distance()
        {
        }

        public DistanceUnits Units { get; set; }

        public double Measurement { get; set; }
    }
}

