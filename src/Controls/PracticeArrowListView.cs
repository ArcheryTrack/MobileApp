using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Delegates;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class PracticeArrowListView : AbstractListView
    {
        public DeletePracticeArrowClickedDelegate DeletePracticeArrowClicked;
        public List<ShotArrow> Arrows { get; set; }

        public PracticeArrowListView ()
        {
            ItemTemplate = new DataTemplate (typeof (PracticeArrowCell));
            PracticeArrowCell.DeletePracticeArrowClicked += ArrowDeleted;
            RowHeight = 30;
        }

        public void RefreshList ()
        {
            ItemsSource = Arrows;
        }

        public void ClearList ()
        {
            ItemsSource = new List<ShotArrow> ();
        }

        public void ArrowDeleted (int arrowNumber)
        {
            var deleteClicked = DeletePracticeArrowClicked;
            if (deleteClicked != null) {
                deleteClicked (arrowNumber);
            }
        }
    }
}

