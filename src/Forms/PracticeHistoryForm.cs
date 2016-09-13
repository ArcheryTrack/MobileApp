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
    public class PracticeHistoryForm : AbstractListForm
    {
        private ArcherBar m_ArcherBar;
        private PracticeHistoryListView m_PracticeHistory;
        private bool m_Loading;
        private Archer m_CurrentArcher;

        public PracticeHistoryForm () : base ("Practice History", "Add Practice")
        {
            m_Loading = true;

            m_ArcherBar = new ArcherBar ();
            m_ArcherBar.ArcherPicked += ArcherPicked;
            m_CurrentArcher = m_ArcherBar.CurrentArcher;
            OutsideLayout.Children.Insert (1, m_ArcherBar);

            m_PracticeHistory = new PracticeHistoryListView ();
            m_PracticeHistory.ItemSelected += OnSelected;
            ListFrame.Content = m_PracticeHistory;

            m_Loading = false;
        }

        void EditPractice (Practice _practice)
        {
            if (m_CurrentArcher != null) {
                PracticeForm editPractice = new PracticeForm ();
                editPractice.SetupForm (m_CurrentArcher, _practice);

                PublishActionMessage ("Practice Edit");

                Navigation.PushModalAsync (editPractice);
            }
        }

        public async void DeletePractice (Practice _practice)
        {
            PublishActionMessage ("Practice Delete Selected");

            bool result = await DisplayAlert ("ArcheryTrack", "Are you sure you want to delete this practice.  All associated records will also be deleted", "Delete", "Cancel");

            if (result) {
                ATManager.GetInstance ().DeletePractice (_practice.Id);
                RefreshList ();
            }
        }


        void ArcherPicked (Archer archer)
        {
            if (!m_Loading) {
                m_CurrentArcher = archer;

                if (m_CurrentArcher != null) {
                    ATManager.GetInstance ().SettingManager.SetCurrentArcher (m_CurrentArcher.Id);
                }
            }

            RefreshList ();
        }

        public override void Add ()
        {
            Archer selected = m_CurrentArcher;

            if (selected != null) {
                PracticeForm addPractice = new PracticeForm ();
                addPractice.SetupForm (selected, null);

                Navigation.PushModalAsync (addPractice);
            }
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            Practice practice = (Practice)e.SelectedItem;

            if (m_CurrentArcher != null) {
                PracticeEndsForm practiceEnds = new PracticeEndsForm ();
                practiceEnds.SetupForm (m_CurrentArcher, practice);

                PublishActionMessage ("Practice Selected");

                Navigation.PushAsync (practiceEnds);
            }
        }

        protected override void OnAppearing ()
        {
            PracticeHistoryCell.PracticeEditClicked += EditPractice;
            PracticeHistoryCell.PracticeDeleteClicked += DeletePractice;

            RefreshList ();
        }

        protected override void OnDisappearing ()
        {
            base.OnDisappearing ();

            PracticeHistoryCell.PracticeEditClicked -= EditPractice;
            PracticeHistoryCell.PracticeDeleteClicked -= DeletePractice;
        }

        private void RefreshList ()
        {
            if (m_CurrentArcher != null) {
                m_PracticeHistory.RefreshList (m_CurrentArcher.Id);
                AddButton.IsEnabled = true;
            } else {
                AddButton.IsEnabled = false;
            }
        }
    }
}

