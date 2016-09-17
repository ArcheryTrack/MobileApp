using Xamarin.Forms;
using System.Collections.Generic;
using ATMobile.Objects;
using ATMobile.Managers;
using ATMobile.Cells;

namespace ATMobile.Controls
{
    public class ArcherListView : AbstractListView
    {
        private List<Archer> m_Archers;

        public ArcherListView ()
        {
            ItemTemplate = new DataTemplate (typeof (ArcherCell)); ;
        }

        public void RefreshList ()
        {
            ATManager manager = ATManager.GetInstance ();
            m_Archers = manager.GetArchers ();

            ItemsSource = m_Archers;
        }
    }
}

