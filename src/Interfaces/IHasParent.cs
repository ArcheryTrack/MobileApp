using System;

namespace ATMobile.Interfaces
{
	public interface IHasParent
	{
		Guid ParentGuid { get; set; }
	}
}

