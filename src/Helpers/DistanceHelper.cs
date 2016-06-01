using System;
using ATMobile.Objects;
using ATMobile.Enums;

namespace ATMobile.Helpers
{
	public static class DistanceHelper
	{
		public static Distance ToYards(this Distance _distance)
		{
			if (_distance.Units == DistanceUnits.Yards) 
			{
				//Already yards
				return _distance;
			}

			Distance output = new Distance ();
			output.Units = DistanceUnits.Yards;
			output.Measurement = _distance.Measurement * 0.9144;
			return output;
		}

		public static Distance ToMeters(this Distance _distance)
		{
			if (_distance.Units == DistanceUnits.Meters) 
			{
				//Already meters
				return _distance;
			}

			Distance output = new Distance ();
			output.Units = DistanceUnits.Meters;
			output.Measurement = _distance.Measurement * 1.09361;
			return output;
		}

	}
}

