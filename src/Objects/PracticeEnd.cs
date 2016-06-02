using System;
using ATMobile.Interfaces;

namespace ATMobile.Objects
{
	public class PracticeEnd : AbstractObject, IHasParent
	{
		public PracticeEnd()
		{
		}


		public Guid ParentGuid { get; set; }
	}
}

