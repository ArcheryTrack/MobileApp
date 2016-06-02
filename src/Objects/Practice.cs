using System;

namespace ATMobile.Objects
{
	public class Practice : AbstractObject
	{
		public Practice()
		{
		}

		public DateTime DateTime { get; set; }
		public int ArrowsPerEnd { get; set; }
		public string Notes { get; set; }
		public int AdditionalArrowsShot { get; set; }

        public bool TrackArrow {get;set;}

	}
}

