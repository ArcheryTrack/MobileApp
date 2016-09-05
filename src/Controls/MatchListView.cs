using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class MatchListView : AbstractListView
    {
        public MatchListView ()
        {
            ItemTemplate = new DataTemplate (typeof (MatchCell));
            //RowHeight = 30;
            HasUnevenRows = true;
        }
    }
}

