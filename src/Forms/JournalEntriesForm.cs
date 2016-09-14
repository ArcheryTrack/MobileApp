using System.Threading.Tasks;
using ATMobile.Cells;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class JournalEntriesForm : AbstractListForm
    {
        private Archer m_CurrentArcher;
        private ArcherBar m_ArcherBar;
        private JournalEntryListView m_lstJournalEntries;

        public JournalEntriesForm ()
            : base ("Journal")
        {
            m_lstJournalEntries = new JournalEntryListView ();
            m_lstJournalEntries.ItemSelected += OnSelected;
            ListFrame.Content = m_lstJournalEntries;

            m_ArcherBar = new ArcherBar ();
            m_ArcherBar.ArcherPicked += ArcherPicked;
            m_CurrentArcher = m_ArcherBar.CurrentArcher;
            OutsideLayout.Children.Insert (0, m_ArcherBar);
        }

        public override void Add ()
        {
            JournalEntryForm form = new JournalEntryForm ();
            form.SetupForm (null, m_CurrentArcher);
            Navigation.PushModalAsync (form, true);
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            JournalEntry entry = (JournalEntry)e.SelectedItem;

            JournalEntryForm form = new JournalEntryForm ();
            form.SetupForm (entry, m_CurrentArcher);

            PublishActionMessage ("JournalEntry Selected");

            Navigation.PushModalAsync (form, true);
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();
            JournalEntryCell.JournalEntryDeleteClicked += DeleteClicked;

            RefreshList ();
        }

        protected override void OnDisappearing ()
        {
            base.OnDisappearing ();

            JournalEntryCell.JournalEntryDeleteClicked -= DeleteClicked;
        }

        private void ArcherPicked (Archer _archer)
        {
            m_CurrentArcher = _archer;

            RefreshList ();
        }

        private void RefreshList ()
        {
            if (m_CurrentArcher != null) {
                m_lstJournalEntries.RefreshList (m_CurrentArcher.Id);
                AddButton.IsEnabled = true;
            } else {
                AddButton.IsEnabled = false;
                m_lstJournalEntries.ItemsSource = null;
            }
        }

        async Task DeleteClicked (JournalEntry _journalEntry)
        {
            PublishActionMessage ("Range Delete Selected");

            bool result = await DisplayAlert ("ArcheryTrack", "Are you sure you want to delete this Journal Entry.", "Delete", "Cancel");

            if (result) {
                ATManager manager = ATManager.GetInstance ();

                manager.DeleteJournalEntry (_journalEntry.Id);
                RefreshList ();
            }
        }
    }
}

