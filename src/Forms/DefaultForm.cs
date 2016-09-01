using System.Collections.Generic;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class DefaultForm : AbstractForm
    {
        private RecentListView m_listRecent;
        private ATLabel m_lblTitle;

        public DefaultForm () : base ("ArcheryTrack")
        {
            m_lblTitle = new ATLabel {
                Text = "Recent Activities",
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness (20, 20, 20, 0)
            };
            OutsideLayout.Children.Add (m_lblTitle);

            Frame frame = new Frame {
                HasShadow = false,
                Margin = new Thickness (20, 15, 20, 20)
            };

            m_listRecent = new RecentListView ();
            m_listRecent.ItemSelected += OnSelected;
            frame.Content = m_listRecent;

            OutsideLayout.Children.Add (frame);

            BootstrapIfNeeded ();
        }

        private void BootstrapIfNeeded ()
        {
            ATManager manager = ATManager.GetInstance ();
            List<Archer> archers = manager.GetArchers ();

            if (archers.Count == 0) {
                ArcherForm archerForm = new ArcherForm (initialArcher: true);
                Navigation.PushModalAsync (archerForm);
            }
        }

        private void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            var manager = ATManager.GetInstance ();
            RecentItem recentItem = (RecentItem)e.SelectedItem;

            if (recentItem.ItemType == "Tournament") {
                Tournament tournament = manager.GetTournament (recentItem.Id);

                TournamentRoundsForm form = new TournamentRoundsForm ();
                form.SetupForm (tournament);

                PublishActionMessage ("Recent Tournament");

                Navigation.PushAsync (form, true);

            } else {
                Practice practice = manager.GetPractice (recentItem.Id);
                Archer archer = manager.GetArcher (practice.ParentId);

                PracticeEndsForm form = new PracticeEndsForm ();
                form.SetupForm (archer, practice);

                PublishActionMessage ("Recent Practice");

                Navigation.PushAsync (form, true);
            }
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            m_listRecent.RefreshList ();
        }
    }
}

