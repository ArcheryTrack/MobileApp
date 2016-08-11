using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class DefaultForm : AbstractForm
    {
        private RecentListView m_listRecent;
        private Label m_lblTitle;

        public DefaultForm () : base ("ArcheryTrack")
        {
            m_lblTitle = new Label {
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
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            var manager = ATManager.GetInstance ();
            RecentItem recentItem = (RecentItem)e.SelectedItem;

            if (recentItem.ItemType == "Tournament") {
                Tournament tournament = manager.GetTournament (recentItem.Id);

                TournamentRoundsForm form = new TournamentRoundsForm ();
                form.SetupForm (tournament);
                Navigation.PushAsync (form, true);

            } else {
                Practice practice = manager.GetPractice (recentItem.Id);
                Archer archer = manager.GetArcher (practice.ParentId);

                PracticeEndsForm form = new PracticeEndsForm ();
                form.SetupForm (archer, practice);
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

