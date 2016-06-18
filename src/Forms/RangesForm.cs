using System;
using ATMobile.Controls;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class RangesForm : ContentPage
    {
        private StackLayout m_OutsideLayout;
        private Button m_Add;
        private RangeListView m_RangeList;

        public RangesForm ()
        {
            Title = "Ranges";

            //Icon = "settings.png";
            BackgroundColor = Color.FromHex ("EEEEEE");

            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            m_Add = new Button {
                Text = "Add Range"
            };
            m_Add.Clicked += OnAdd;
            m_OutsideLayout.Children.Add (m_Add);

            m_RangeList = new RangeListView ();
            m_RangeList.ItemSelected += OnSelected;
            m_OutsideLayout.Children.Add (m_RangeList);

            Content = m_OutsideLayout;
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            Range range = (Range)e.SelectedItem;

            RangeForm addRange = new RangeForm ();
            addRange.SetupForm (range);

            Navigation.PushAsync (addRange);
        }

        void OnAdd (object sender, EventArgs e)
        {
            RangeForm addRange = new RangeForm ();
            Navigation.PushAsync (addRange);
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            m_RangeList.RefreshList ();
        }
    }
}

