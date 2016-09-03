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
                               int? padding = null,
                               int? spacing = null)
        {
            Title = title;
            BackgroundColor = Color.FromHex (UIConstants.DetailFormBackgroundColor);

            if (Device.Idiom == TargetIdiom.Phone) {
                if (padding == null) {
                    padding = 0;
                }

                if (spacing == null) {
                    spacing = 1;
                }
            } else {
                if (padding == null) {
                    padding = 5;
                }

                if (spacing == null) {
                    spacing = 5;
                }
            }

            OutsideLayout = new StackLayout {
                Spacing = spacing.Value,
                VerticalOptions = LayoutOptions.Fill,
                Padding = padding.Value
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

