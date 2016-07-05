using System;
using ATMobile.Constants;
using ATMobile.Controls;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.PickerForms
{
    public class ArcherPicker : ContentPage
    {
        private StackLayout m_OutsideLayout;
        private Label m_lblTitle;
        private ArcherPickerListView m_ArcheryList;
        private Button m_btnCancel;

        public ArcherPicker ()
        {
            Title = "Select Archer";

            //Icon = "settings.png";
            BackgroundColor = Color.FromHex (UIConstants.FormBackgroundColor);

            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            m_lblTitle = new Label ();
            m_lblTitle.Text = "Select an Archer";
            m_OutsideLayout.Children.Add (m_lblTitle);

            m_ArcheryList = new ArcherPickerListView ();
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
            Archer archer = (Archer)e.SelectedItem;


            await Navigation.PopModalAsync ();
        }
    }
}

