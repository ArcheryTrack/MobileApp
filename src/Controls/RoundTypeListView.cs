using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class RoundTypeListView : AbstractListView
    {
        private ObservableCollection<RoundType> m_RoundTypes;

        public RoundTypeListView ()
        {
            ItemTemplate = new DataTemplate (typeof (RoundTypeCell));
            RowHeight = 60;
        }

        public void RefreshList (Guid _tournamentTypeId)
        {
            ATManager manager = ATManager.GetInstance ();
            var roundTypes = manager.GetRoundTypes (_tournamentTypeId);

            m_RoundTypes = new ObservableCollection<RoundType> (roundTypes);
            ItemsSource = m_RoundTypes;
        }

        public void ClearList ()
        {
            ItemsSource = null;
        }
    }
}

