using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class PracticeArrowListView : AbstractListView
    {
        public List<ShotArrow> Arrows { get; set; }

        public PracticeArrowListView ()
        {
            ItemTemplate = new DataTemplate (typeof (PracticeArrowCell));
            RowHeight = 30;
        }

        public void RefreshList ()
        {
            ItemsSource = Arrows;

        }

        public void ClearList ()
        {
            ItemsSource = null;
        }
    }
}

