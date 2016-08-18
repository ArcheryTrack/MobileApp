using System;
using ATMobile.Constants;
using ATMobile.Managers;
using ATMobile.Messages;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class AbstractForm : ContentPage
    {
        protected StackLayout OutsideLayout;

        protected AbstractForm (string title,
                               int padding = 5,
                               int spacing = 5)
        {
            Title = title;
            BackgroundColor = Color.FromHex (UIConstants.DetailFormBackgroundColor);

            OutsideLayout = new StackLayout {
                Spacing = spacing,
                VerticalOptions = LayoutOptions.Fill,
                Padding = padding
            };

            Content = OutsideLayout;
        }

        protected void PublishActionMessage (string _action)
        {
            ATManager manager = ATManager.GetInstance ();

            ActionMessage am = new ActionMessage {
                Form = this.GetType ().FullName,
                Action = _action
            };

            manager.MessagingManager.Publish (am);
        }

    }
}

