using System;

namespace ATMobile.Objects
{
	public class Archer : AbstractObject
	{
		public Archer()
		{
		}

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public DateTime StartedArchery { get; set; }
	}
}

