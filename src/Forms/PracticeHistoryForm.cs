using System;
using System.Collections.Generic;
using ATMobile.Objects;
using Xamarin.Forms;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Constants;
using ATMobile.Cells;

namespace ATMobile.Forms
{
    public class PracticeHistoryForm : AbstractListForm, IDisposable
    {
        private Picker m_ArcherPicker;
        private PracticeHistoryListView m_PracticeHistory;
        private List<Archer> m_Archers;
        private bool m_Loading;

        public PracticeHistoryForm () : base ("Practice History")
        {
            m_Loading = true;

            //Load Archers and setup picker
            m_Archers = ATManager.GetInstance ().GetArchers ();

            m_ArcherPicker = new Picker {
                Margin = new Thickness (20, 0, 20, 0)
            };
            foreach (var archer in m_Archers) {
                m_ArcherPicker.Items.Add (archer.FullName);
            }
            m_ArcherPicker.SelectedIndexChanged += OnArcherPicked;
            OutsideLayout.Children.Insert (1, m_ArcherPicker);

            m_PracticeHistory = new PracticeHistoryListView ();
            m_PracticeHistory.ItemSelected += OnSelected;
            ListFrame.Content = m_PracticeHistory;

            PracticeHistoryCell.PracticeEditClicked += OnEditClicked;

            GetCurrentArcher ();

            m_Loading = false;
        }

        void OnEditClicked (Practice _practice)
        {
            Archer selected = GetSelectedArcher ();

            if (selected != null) {
                PracticeForm editPractice = new PracticeForm ();
                editPractice.SetupForm (selected, _practice);

                PublishActionMessage ("Practice Edit");

                Navigation.PushModalAsync (editPractice);
            }
        }

        void GetCurrentArcher ()
        {
            Guid? currentArcher = ATManager.GetInstance ().SettingManager.GetCurrentArcher ();

            if (currentArcher != null) {
                for (int i = 0; i < m_Archers.Count; i++) {
                    Archer archer = m_Archers [i];

                    if (archer.Id == currentArcher.Value) {
                        m_ArcherPicker.SelectedIndex = i;
                        break;
                    }
                }
            } else {
                if (m_Archers.Count > 0) {
                    m_ArcherPicker.SelectedIndex = 0;
                }
            }
        }

        Archer GetSelectedArcher ()
        {
            if (m_ArcherPicker.SelectedIndex >= 0) {
                return m_Archers [m_ArcherPicker.SelectedIndex];
            }

            return null;
        }

        void OnArcherPicked (object sender, EventArgs e)
        {
            if (!m_Loading) {
                if (m_ArcherPicker.SelectedIndex != -1) {
                    Archer archer = m_Archers [m_ArcherPicker.SelectedIndex];
                    ATManager.GetInstance ().SettingManager.SetCurrentArcher (archer.Id);
                }
            }

            if (m_ArcherPicker.SelectedIndex >= 0) {
                AddButton.IsEnabled = true;
            } else {
                AddButton.IsEnabled = false;
            }

            RefreshList ();
        }

        public override void Add ()
        {
            Archer selected = GetSelectedArcher ();

            if (selected != null) {
                PracticeForm addPractice = new PracticeForm ();
                addPractice.SetupForm (selected, null);

                Navigation.PushModalAsync (addPractice);
            }
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            Practice practice = (Practice)e.SelectedItem;

            Archer selected = GetSelectedArcher ();

            if (selected != null) {
                PracticeEndsForm practiceEnds = new PracticeEndsForm ();
                practiceEnds.SetupForm (selected, practice);

                PublishActionMessage ("Practice Selected");

                Navigation.PushAsync (practiceEnds);
            }
        }

        protected override void OnAppearing ()
        {
            RefreshList ();
        }

        private void RefreshList ()
        {
            if (m_ArcherPicker.SelectedIndex == -1) {
                m_PracticeHistory.ClearList ();
            } else {
                Archer archer = m_Archers [m_ArcherPicker.SelectedIndex];
                m_PracticeHistory.RefreshList (archer.Id);
            }
        }

        public void Dispose ()
        {
            PracticeHistoryCell.PracticeEditClicked -= OnEditClicked;
        }
    }
}

