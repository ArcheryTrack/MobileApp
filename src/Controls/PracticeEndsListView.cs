using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class PracticeEndsListView : AbstractListView
    {
        public List<PracticeEnd> Ends;

        public PracticeEndsListView ()
        {
            ItemTemplate = new DataTemplate (typeof (PracticeEndCell));
            RowHeight = 30;
        }

        public void RefreshList (Guid _practiceId)
        {
            ATManager manager = ATManager.GetInstance ();
            Ends = manager.GetPracticeEnds (_practiceId);
            ItemsSource = Ends;
        }

        public void ClearList ()
        {
            ItemsSource = null;
        }
    }
}

