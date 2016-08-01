using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ATMobile.Controls;
using ATMobile.Constants;

namespace ATMobile.Forms
{
    public class MenuPage : ContentPage
    {
        public MenuListView Menu { get; set; }

        public MenuPage ()
        {
            //Icon = "settings.png";
            Title = "Menu"; // The Title property must be set.
            BackgroundColor = Color.FromHex (UIConstants.MenuTitleColor);

            Menu = new MenuListView ();

            var menuLabel = new ContentView {
                Padding = new Thickness (10, 36, 0, 5),
                Content = new Label {
                    TextColor = Color.FromHex (UIConstants.MenuTitleTextColor),
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

