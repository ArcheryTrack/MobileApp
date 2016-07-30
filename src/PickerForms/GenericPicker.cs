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
            Title = _title;

            Padding = new Thickness (0, 20, 0, 0);

            //Icon = "settings.png";
            BackgroundColor = Color.FromHex (UIConstants.FormBackgroundColor);

            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            m_lblTitle = new Label ();
            m_lblTitle.Text = _title;
            m_OutsideLayout.Children.Add (m_lblTitle);

            List = new GenericListView<U> ();
            List.ItemSelected += OnSelected;
            m_OutsideLayout.Children.Add (List);

            m_btnCancel = new Button ();
            m_btnCancel.Text = "Cancel";
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

