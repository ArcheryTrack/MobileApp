using System;
using System.Collections.Generic;
using ATMobile.Enums;
using ATMobile.Objects;

namespace ATMobile.Data
{
    public static class DistanceUnitData
    {
        public static List<DistanceUnit> GetDistanceUnits ()
        {
            List<DistanceUnit> units = new List<DistanceUnit> ();

            DistanceUnit meters = new DistanceUnit {
                Name = "Meters",
                UnitOfMeasure = DistanceUnits.Meters
            };
            units.Add (meters);

            DistanceUnit yards = new DistanceUnit {
                Name = "Yards",
                UnitOfMeasure = DistanceUnits.Yards
            };
            units.Add (yards);

            return units;
        }

        public static DistanceUnit FindDistanceUnit (DistanceUnits _unitOfMeasure)
        {
            var data = GetDistanceUnits ();

            foreach (var item in data) {
                if (item.UnitOfMeasure == _unitOfMeasure) {
                    return item;
                }
            }

            return null;
        }
    }
}

