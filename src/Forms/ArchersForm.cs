using Xamarin.Forms;
using ATMobile.Controls;
using ATMobile.Objects;

namespace ATMobile.Forms
{
    public class ArchersForm : AbstractListForm
    {
        private ArcherListView m_ArcherList;

        public ArchersForm () : base ("Archers")
        {
            m_ArcherList = new ArcherListView ();
            m_ArcherList.ItemSelected += OnSelected;
            ListFrame.Content = m_ArcherList;
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            Archer archer = (Archer)e.SelectedItem;

            ArcherForm addArcher = new ArcherForm ();
            addArcher.SetArcher (archer);

            Navigation.PushModalAsync (addArcher);
        }

        public override void Add ()
        {
            ArcherForm addArcher = new ArcherForm ();
            Navigation.PushModalAsync (addArcher);
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            m_ArcherList.RefreshList ();
        }
    }
}

