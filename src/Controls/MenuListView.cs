using System;
using Xamarin.Forms;
using System.Collections.Generic;
using ATMobile.Constants;
using ATMobile.Objects;
using ATMobile.Managers;
using ATMobile.Cells;

namespace ATMobile.Controls
{
    public class MenuListView : AbstractListView
    {
        public MenuListView ()
        {
            BackgroundColor = Color.FromHex (UIConstants.MenuListColor);

            ItemTemplate = new DataTemplate (typeof (MenuCell));

            List<MenuOption> data = ATManager.GetInstance ().MenuManager.GetMainMenu ();
            ItemsSource = data;

        }
    }
}

