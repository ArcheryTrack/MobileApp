using System;
using System.Collections.Generic;
using ATMobile.Controls;
using ATMobile.Forms;

namespace ATMobile.Data
{
	public class MenuListData: List<MenuItem>
	{
		public MenuListData ()
		{
			this.Add(new MenuItem() {
				Title = "Sight Estimate",
				TargetType = typeof(SightEstimateForm)
			});
			this.Add (new MenuItem () { 
				Title = "Sight Setup", 
				TargetType = typeof(SightSetupForm)
			});

		}
	}
}

