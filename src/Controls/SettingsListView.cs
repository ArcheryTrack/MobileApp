using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Constants;
using ATMobile.Data;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class SettingsListView : AbstractListView
    {
        public SettingsListView ()
        {
            BackgroundColor = Color.FromHex (UIConstants.MenuListColor);

            ItemTemplate = new DataTemplate (typeof (MenuCell));

            List<MenuOption> data = ATManager.GetInstance ().MenuManager.GetSettingsMenu ();
            ItemsSource = data;
        }
    }
}

