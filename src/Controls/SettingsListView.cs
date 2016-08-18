using System;
using System.Collections.Generic;
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

            var cell = new DataTemplate (typeof (ImageCell));
            cell.SetBinding (TextCell.TextProperty, "Title");
            cell.SetBinding (ImageCell.ImageSourceProperty, "IconSource");

            ItemTemplate = cell;

            List<MenuOption> data = ATManager.GetInstance ().MenuManager.GetSettingsMenu ();
            ItemsSource = data;
        }
    }
}

