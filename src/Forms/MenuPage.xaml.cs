﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ATMobile.Controls;

namespace ATMobile.Forms
{
	public partial class MenuPage : ContentPage
	{
		public ListView Menu { get; set; }

		public MenuPage ()
		{
			InitializeComponent ();

			//Icon = "settings.png";
			Title = "ArcheryTrack"; // The Title property must be set.
			BackgroundColor = Color.FromHex ("333333");

			Menu = new MenuListView();

			var menuLabel = new ContentView {
				Padding = new Thickness (10, 36, 0, 5),
				Content = new Label {
					TextColor = Color.FromHex ("AAAAAA"),
					Text = "MENU", 
				}
			};

			var layout = new StackLayout { 
				Spacing = 0, 
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			layout.Children.Add (menuLabel);
			layout.Children.Add (Menu);

			Content = layout;
		}
	}
}
