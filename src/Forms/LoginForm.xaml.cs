using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ATMobile
{
	public partial class LoginForm : ContentPage
	{
		private App m_App;

		public LoginForm (App _app)
		{
			InitializeComponent ();

			m_App = _app;
		}

		void OnLogin(object sender, EventArgs e)
		{
			m_App.ShowMainPage ();
		}
	}
}

