using System;
using System.Collections.Generic;
using ATMobile.Constants;
using ATMobile.Data;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class SettingsListView : AbstractListView
    {
        public SettingsListView ()
        {
            BackgroundColor = Color.FromHex (UIConstants.MenuListColor);
            List<MenuOption> data = new SettingsListData ();

            ItemsSource = data;

            var cell = new DataTemplate (typeof (ImageCell));
            cell.SetBinding (TextCell.TextProperty, "Title");
            cell.SetBinding (ImageCell.ImageSourceProperty, "IconSource");

            ItemTemplate = cell;
        }
    }
}

