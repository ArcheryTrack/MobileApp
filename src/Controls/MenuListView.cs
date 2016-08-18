using System;
using Xamarin.Forms;
using System.Collections.Generic;
using ATMobile.Data;
using ATMobile.Constants;
using ATMobile.Objects;
using ATMobile.Managers;

namespace ATMobile.Controls
{
    public class MenuListView : AbstractListView
    {
        public MenuListView ()
        {
            BackgroundColor = Color.FromHex (UIConstants.MenuListColor);

            var cell = new DataTemplate (typeof (ImageCell));
            cell.SetBinding (TextCell.TextProperty, "Title");
            cell.SetBinding (ImageCell.ImageSourceProperty, "IconSource");

            ItemTemplate = cell;

            List<MenuOption> data = ATManager.GetInstance ().MenuManager.GetMainMenu ();
            ItemsSource = data;

        }
    }
}

