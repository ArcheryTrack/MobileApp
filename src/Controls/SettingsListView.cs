using System;
using System.Collections.Generic;
using ATMobile.Data;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class SettingsListView : AbstractListView
    {
        public SettingsListView ()
        {
            List<MenuItem> data = new SettingsListData ();

            ItemsSource = data;

            var cell = new DataTemplate (typeof (ImageCell));
            cell.SetBinding (TextCell.TextProperty, "Title");
            cell.SetBinding (ImageCell.ImageSourceProperty, "IconSource");

            ItemTemplate = cell;
        }
    }
}

