using System;
using System.Collections.Generic;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class JournalEntriesForm : AbstractListForm
    {
        private Archer m_CurrentArcher;
        private int m_CurrentArcherIndex;
        private List<Archer> m_Archers;
        private StackLayout m_ArcherLayout;
        private Button m_btnPrevious;
        private Button m_btnNext;
        private Label m_lblArcher;
        private JournalEntryListView m_lstJournalEntries;

        public JournalEntriesForm ()
            : base ("Journal")
        {
            m_lstJournalEntries = new JournalEntryListView ();
            m_lstJournalEntries.ItemSelected += OnSelected;
            ListFrame.Content = m_lstJournalEntries;

            /* Setup the Archer */
            m_ArcherLayout = new StackLayout {
                Spacing = 5,
                Padding = 5,
                Orientation = StackOrientation.Horizontal
            };
            OutsideLayout.Children.Insert (0, m_ArcherLayout);

            m_btnPrevious = new Button {
                Text = "<",
                HorizontalOptions = LayoutOptions.Start
            };
            m_btnPrevious.Clicked += PreviousClicked;
            m_ArcherLayout.Children.Add (m_btnPrevious);

            m_lblArcher = new Label {
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            m_ArcherLayout.Children.Add (m_lblArcher);

            m_btnNext = new Button {
                Text = ">",
                HorizontalOptions = LayoutOptions.End
            };
            m_btnNext.Clicked += NextClicked;
            m_ArcherLayout.Children.Add (m_btnNext);

            m_Archers = ATManager.GetInstance ().GetArchers ();
            m_CurrentArcherIndex = 0;
            SetArcher ();
        }

        public override void Add ()
        {

        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
        }

        void PreviousClicked (object sender, EventArgs e)
        {
            m_CurrentArcherIndex--;

            if (m_CurrentArcherIndex < 0) {
                m_CurrentArcherIndex = m_Archers.Count - 1;
            }

            SetArcher ();
        }

        void NextClicked (object sender, EventArgs e)
        {
            m_CurrentArcherIndex++;

            if (m_CurrentArcherIndex >= m_Archers.Count) {
                m_CurrentArcherIndex = 0;
            }

            SetArcher ();
        }

        private void SetArcher ()
        {
            m_CurrentArcher = m_Archers [m_CurrentArcherIndex];

            m_lblArcher.Text = m_CurrentArcher.FullName;

            m_lstJournalEntries.RefreshList (m_CurrentArcher.Id);
        }


    }
}

