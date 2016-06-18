using System;
using Xamarin.Forms;
using System.Collections.Generic;
using ATMobile.Data;

namespace ATMobile.Controls
{
    public class MenuListView : AbstractListView
    {
        public MenuListView ()
        {
            List<MenuItem> data = new MenuListData ();

            ItemsSource = data;

            var cell = new DataTemplate (typeof (ImageCell));
            cell.SetBinding (TextCell.TextProperty, "Title");
            cell.SetBinding (ImageCell.ImageSourceProperty, "IconSource");

            ItemTemplate = cell;
        }
    }
}

