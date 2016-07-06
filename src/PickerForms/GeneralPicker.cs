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
    public class GeneralPicker<T, U> : ContentPage where U : AbstractBaseCell
    {
        private StackLayout m_OutsideLayout;
        private Label m_lblTitle;
        private GenericListView<U> m_ArcheryList;
        private Button m_btnCancel;

        public event GenericPickedDelegate<T> ItemPicked;

        public GeneralPicker (
            string _title,
            string _listCellField,
            List<T> items)
        {
            Title = _title;

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

            m_ArcheryList = new GenericListView<U> ();
            m_ArcheryList.ItemSelected += OnSelected;
            m_OutsideLayout.Children.Add (m_ArcheryList);

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

