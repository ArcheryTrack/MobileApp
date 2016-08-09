using System;
using ATMobile.Constants;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public abstract class AbstractEntryForm : AbstractForm
    {
        private Label m_lblTitle;
        protected StackLayout ButtonsLayout;
        protected StackLayout InsideLayout;

        protected Button CancelButton;
        protected Button SaveButton;

        public abstract void Save ();

        protected AbstractEntryForm (string _title) : base (_title, 0, 0)
        {
            AbsoluteLayout header = new AbsoluteLayout {
                BackgroundColor = Color.FromHex (UIConstants.NavBarColor),
                Margin = new Thickness (0, 0, 0, 0),
                MinimumHeightRequest = 65,
                HeightRequest = 65
            };
            OutsideLayout.Children.Add (header);

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

            ButtonsLayout = new StackLayout {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Padding = 0,
                Spacing = 0
            };
            OutsideLayout.Children.Add (ButtonsLayout);

            CancelButton = new Button {
                Text = "Cancel",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 100
            };
            CancelButton.Clicked += OnCancel;
            ButtonsLayout.Children.Add (CancelButton);

            SaveButton = new Button {
                Text = "Save",
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 100
            };
            SaveButton.Clicked += OnSave;
            ButtonsLayout.Children.Add (SaveButton);

            InsideLayout = new StackLayout {
                Spacing = 5,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5,
                Margin = new Thickness (20, 0, 20, 20)
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

