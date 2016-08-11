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
    public class GenericPicker<T, U> : ContentPage where U : AbstractBaseCell
    {
        private StackLayout m_OutsideLayout;
        private Label m_lblTitle;
        public GenericListView<U> List;
        private Button m_btnCancel;

        public event GenericPickedDelegate<T> ItemPicked;

        public GenericPicker (
            string _title)
        {
            m_OutsideLayout = new StackLayout {
                Spacing = 0,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 0
            };

            AbsoluteLayout header = new AbsoluteLayout {
                BackgroundColor = Color.FromHex (UIConstants.NavBarColor),
                Margin = new Thickness (0, 0, 0, 0),
                MinimumHeightRequest = 65,
                HeightRequest = 65
            };
            m_OutsideLayout.Children.Add (header);

            m_lblTitle = new Label {
                Text = _title,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.FromHex (UIConstants.NavBarTextColor),
                FontAttributes = FontAttributes.Bold
            };

            AbsoluteLayout.SetLayoutFlags (m_lblTitle,
                AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds (m_lblTitle,
                new Rectangle (0.5,
                    0.7, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            header.Children.Add (m_lblTitle);

            BoxView line = new BoxView {
                HeightRequest = 1,
                Color = Color.FromHex (UIConstants.LineColor),
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            AbsoluteLayout.SetLayoutFlags (
                line,
                AbsoluteLayoutFlags.WidthProportional);

            AbsoluteLayout.SetLayoutBounds (line,
                new Rectangle (0,
                               64.5,
                               1,
                               AbsoluteLayout.AutoSize));

            header.Children.Add (line);

            Title = _title;
            Padding = new Thickness (0, 20, 0, 0);

            //Icon = "settings.png";
            BackgroundColor = Color.FromHex (UIConstants.DetailFormBackgroundColor);

            Frame frame = new Frame {
                Margin = new Thickness (10, 10, 10, 5),
                HasShadow = false
            };
            m_OutsideLayout.Children.Add (frame);

            List = new GenericListView<U> ();
            List.ItemSelected += OnSelected;
            frame.Content = List;

            m_btnCancel = new Button () {
                Text = "Cancel",
                Margin = new Thickness (10, 5, 10, 20)
            };
            m_btnCancel.Clicked += OnCancel;
            m_OutsideLayout.Children.Add (m_btnCancel);

            Content = m_OutsideLayout;
        }

        async void OnCancel (object sender, EventArgs e)
        {

            await Navigation.PopModalAsync ();
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

