using System;

namespace ATMobile.Objects
{
	public class SightSetting : AbstractObject
	{
		public DateTime DateTime { get; set; }
		public Distance Distance { get; set; }
		public double Setting { get; set; }
	}
}

