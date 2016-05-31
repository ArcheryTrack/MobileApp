using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ATMobile
{
	public partial class LoginForm : ContentPage
	{
		public LoginForm ()
		{
			InitializeComponent ();
		}

		async void OnLogin(object sender, EventArgs e)
		{
			Navigation.PushAsync (new HomeForm ());
		}
	}
}

