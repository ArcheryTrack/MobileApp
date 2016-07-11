using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class TournamentTypeListView : AbstractListView
    {
        private List<TournamentType> m_TournamentTypes;

        public TournamentTypeListView ()
        {
            ItemTemplate = new DataTemplate (typeof (TournamentTypeCell));
            //RowHeight = 60;
            HasUnevenRows = true;
        }

        public void RefreshList ()
        {
            ATManager manager = ATManager.GetInstance ();
            m_TournamentTypes = manager.GetTournamentTypes ();
            ItemsSource = m_TournamentTypes;
        }

        public void ClearList ()
        {
            ItemsSource = null;
        }
    }
}

