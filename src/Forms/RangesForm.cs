using ATMobile.Controls;
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

            Navigation.PushAsync (addRange);
        }

        public override void Add ()
        {
            RangeForm addRange = new RangeForm ();
            Navigation.PushModalAsync (addRange);
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            m_RangeList.RefreshList ();
        }
    }
}

