using System;
using ATMobile.Constants;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public abstract class AbstractListForm : ContentPage
    {
        protected StackLayout OutsideLayout;
        protected Button AddButton;
        protected Frame ListFrame;

        public abstract void Add ();

        public AbstractListForm (string _title)
        {
            Title = _title;

            //Icon = "settings.png";
            BackgroundColor = Color.FromHex (UIConstants.FormBackgroundColor);

            OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            AddButton = new Button {
                Text = "Add"
            };
            AddButton.Clicked += OnAdd;
            OutsideLayout.Children.Add (AddButton);

            ListFrame = new Frame {
                HasShadow = false,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            OutsideLayout.Children.Add (ListFrame);

            Content = OutsideLayout;
        }

        void OnAdd (object sender, EventArgs e)
        {
            Add ();
        }
    }
}

