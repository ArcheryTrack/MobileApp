using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class TournamentEndsListView : AbstractListView
    {
        public List<TournamentEnd> Ends;

        public TournamentEndsListView ()
        {
            ItemTemplate = new DataTemplate (typeof (TournamentEndCell));
            //RowHeight = 30;
            HasUnevenRows = true;
        }
    }
}

