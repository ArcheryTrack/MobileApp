using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Constants;
using ATMobile.Controls;
using ATMobile.Delegates;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.PickerForms
{
    public abstract class AbstractGenericPicker<T, U> : ContentPage where U : AbstractBaseCell
    {
        private Header m_Header;
        private StackLayout m_OutsideLayout;
        public GenericListView<U> List;
        private Button m_btnCancel;
        private Button m_btnAdd;

        public event GenericPickedDelegate<T> ItemPicked;
        public abstract void AddPressed ();

        protected AbstractGenericPicker (
            string _title,
            string _subject)
        {
            BackgroundColor = Color.FromHex (UIConstants.DetailFormBackgroundColor);
            Title = _title;
            Padding = new Thickness (0, 0, 0, 0);

            m_OutsideLayout = new StackLayout {
                Spacing = 0,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 0
            };

            m_Header = new Header (_title) {
                Margin = new Thickness (0, 0, 0, 0)
            };
            m_OutsideLayout.Children.Add (m_Header);

            ATButtonBar buttons = new ATButtonBar ();
            m_OutsideLayout.Children.Add (buttons);

            m_btnCancel = buttons.Add ("Cancel", LayoutOptions.StartAndExpand);
            m_btnCancel.Clicked += OnCancel;

            m_btnAdd = buttons.Add (string.Format ("Add {0}", _subject), LayoutOptions.EndAndExpand, 200);
            m_btnAdd.Clicked += OnAdd;

            Frame frame = new Frame {
                Margin = new Thickness (10, 10, 10, 5),
                HasShadow = false
            };
            m_OutsideLayout.Children.Add (frame);

            List = new GenericListView<U> ();
            List.ItemSelected += OnSelected;
            frame.Content = List;

            Content = m_OutsideLayout;
        }

        async void OnCancel (object sender, EventArgs e)
        {
            await Navigation.PopModalAsync ();
        }

        void OnAdd (object sender, EventArgs e)
        {
            AddPressed ();
        }

        async void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            T item = (T)e.SelectedItem;

            var picked = ItemPicked;
            if (picked != null) {
                picked (item);
            }

            await Navigation.PopModalAsync ();
        }
    }
}

