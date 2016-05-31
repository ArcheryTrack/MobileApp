using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ATMobile
{
	public partial class HomeForm : TabbedPage
	{
		public HomeForm ()
		{
			InitializeComponent ();

			Title = "ArcheryTrack";

			Children.Add (new SightSetup ());
		}
	}
}

