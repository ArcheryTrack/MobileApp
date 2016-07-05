using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class ArcherPickerListView : ListView
    {
        private List<Archer> m_Archers;

        public ArcherPickerListView ()
        {
            ItemTemplate = new DataTemplate (typeof (ArcherCell)); ;

            RefreshList ();
        }

        public void RefreshList ()
        {
            ATManager manager = ATManager.GetInstance ();
            m_Archers = manager.GetArchers ();

            ItemsSource = m_Archers;
        }

    }
}

