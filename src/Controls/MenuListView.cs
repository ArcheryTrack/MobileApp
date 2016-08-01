using System;
using Xamarin.Forms;
using System.Collections.Generic;
using ATMobile.Data;
using ATMobile.Constants;

namespace ATMobile.Controls
{
    public class MenuListView : AbstractListView
    {
        public MenuListView ()
        {
            BackgroundColor = Color.FromHex (UIConstants.MenuListColor);

            List<Objects.MenuOption> data = new MenuListData ();

            ItemsSource = data;

            var cell = new DataTemplate (typeof (ImageCell));
            cell.SetBinding (TextCell.TextProperty, "Title");
            cell.SetBinding (ImageCell.ImageSourceProperty, "IconSource");

            ItemTemplate = cell;
        }
    }
}

