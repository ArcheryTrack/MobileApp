using System;
using Xamarin.Forms;
using System.Collections.Generic;
using ATMobile.Objects;
using ATMobile.Managers;

namespace ATMobile.Controls
{
    public class ArcherListView : ListView
    {
        private List<Archer> m_Archers;

        public ArcherListView()
        {
            ATManager manager = ATManager.GetInstance();
            m_Archers = manager.GetArchers();

            ItemsSource = m_Archers;
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.Transparent;

            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetBinding(TextCell.TextProperty, "FirstName");
            cell.SetBinding(TextCell.TextProperty, "LastName");

            ItemTemplate = cell;


        }
    }
}

