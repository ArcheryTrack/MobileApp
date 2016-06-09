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
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.Transparent;

            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetBinding(TextCell.TextProperty, "FullName");

            ItemTemplate = cell;
        }

        public void RefreshList() 
        {
            ATManager manager = ATManager.GetInstance();
            m_Archers = manager.GetArchers();

            ItemsSource = m_Archers;
        }
    }
}

