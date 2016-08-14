using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ATMobile.Cells;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class JournalEntryListView : AbstractLoadMoreListView<JournalEntry>
    {
        Guid? m_ArcherId;

        public JournalEntryListView ()
        {
            ItemTemplate = new DataTemplate (typeof (JournalEntryCell));
            //RowHeight = 30;
            HasUnevenRows = true;
        }

        public void RefreshList (Guid _archerId)
        {
            Rows.Clear ();

            m_ArcherId = _archerId;

            List<JournalEntry> entries = ATManager.GetInstance ().GetJournalEntries (
                m_ArcherId.Value,
                0,
                20);

            AppendRows (entries);
        }

        public override void LoadMoreData (int start)
        {
            List<JournalEntry> entries = ATManager.GetInstance ().GetJournalEntries (
                m_ArcherId.Value,
                start,
                20);

            AppendRows (entries);
        }
    }
}


