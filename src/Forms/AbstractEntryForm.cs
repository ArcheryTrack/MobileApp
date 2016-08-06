using System;
using ATMobile.Constants;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public abstract class AbstractEntryForm : AbstractForm
    {
        protected StackLayout ButtonsLayout;
        protected StackLayout InsideLayout;

        protected Button CancelButton;
        protected Button SaveButton;

        public abstract void Save ();

        protected AbstractEntryForm (string _title) : base (_title)
        {
            ButtonsLayout = new StackLayout {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Padding = 5,
                Spacing = 10
            };
            OutsideLayout.Children.Add (ButtonsLayout);

            CancelButton = new Button {
                Text = "Cancel",
                HorizontalOptions = LayoutOptions.Start,
                WidthRequest = 100
            };
            CancelButton.Clicked += OnCancel;
            ButtonsLayout.Children.Add (CancelButton);

            SaveButton = new Button {
                Text = "Save",
                HorizontalOptions = LayoutOptions.End,
                WidthRequest = 100
            };
            SaveButton.Clicked += OnSave;
            ButtonsLayout.Children.Add (SaveButton);

            InsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };
            OutsideLayout.Children.Add (InsideLayout);
        }

        private void OnCancel (object sender, EventArgs e)
        {
            Navigation.PopModalAsync (true);
        }

        private void OnSave (object sender, EventArgs e)
        {
            Save ();

            Navigation.PopModalAsync (true);
        }
    }
}

