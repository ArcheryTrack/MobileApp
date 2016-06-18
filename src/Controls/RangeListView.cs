using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class RangeListView : AbstractListView
    {
        private List<Range> m_Ranges;

        public RangeListView ()
        {
            ItemTemplate = new DataTemplate (typeof (RangeCell));
        }

        public void RefreshList ()
        {
            ATManager manager = ATManager.GetInstance ();
            m_Ranges = manager.GetRanges ();

            ItemsSource = m_Ranges;
        }
    }
}


