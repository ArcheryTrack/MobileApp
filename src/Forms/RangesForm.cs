using System;
using ATMobile.Cells;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class RangesForm : AbstractListForm
    {
        private RangeListView m_RangeList;

        public RangesForm () : base ("Ranges")
        {
            m_RangeList = new RangeListView {
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            m_RangeList.ItemSelected += OnSelected;
            ListFrame.Content = m_RangeList;
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            Range range = (Range)e.SelectedItem;

            RangeForm addRange = new RangeForm ();
            addRange.SetupForm (range);

            PublishActionMessage ("Range Selected");

            Navigation.PushModalAsync (addRange);
        }

        public override void Add ()
        {
            RangeForm addRange = new RangeForm ();
            Navigation.PushModalAsync (addRange);
        }

        async void DeleteClicked (Range _range)
        {
            PublishActionMessage ("Range Delete Selected");

            bool result = await DisplayAlert ("ArcheryTrack", "Are you sure you want to delete this Range.", "Delete", "Cancel");

            if (result) {
                ATManager manager = ATManager.GetInstance ();

                manager.DeleteRange (_range.Id);
                m_RangeList.RefreshList ();
            }
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            RangeCell.RangeDeleteClicked += DeleteClicked;
            m_RangeList.RefreshList ();
        }

        protected override void OnDisappearing ()
        {
            base.OnDisappearing ();
            RangeCell.RangeDeleteClicked -= DeleteClicked;
        }
    }
}

