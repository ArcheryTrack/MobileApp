using System;
using ATMobile.Constants;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class AbstractForm : ContentPage
    {
        protected StackLayout OutsideLayout;

        protected AbstractForm (string title)
        {
            Title = title;
            BackgroundColor = Color.FromHex (UIConstants.DetailFormBackgroundColor);

            OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            Content = OutsideLayout;
        }

    }
}

