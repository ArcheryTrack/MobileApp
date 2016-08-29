using System;
using ATMobile.Constants;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Messages;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class SettingsForm : ContentPage
    {
        public SettingsListView m_Menu { get; set; }

        public SettingsForm ()
        {
            //Icon = "settings.png";
            Title = "Settings"; // The Title property must be set.
            BackgroundColor = Color.FromHex (UIConstants.MenuSettingsTitleColor);

            m_Menu = new SettingsListView ();
            m_Menu.ItemSelected += OnSelected;

            var menuLabel = new ContentView {
                Padding = new Thickness (10, 36, 0, 5),
                Content = new ATLabel {
                    TextColor = Color.FromHex (UIConstants.MenuSettingsTitleTextColor),
                    Text = "Setting",
                }
            };

            var layout = new StackLayout {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            layout.Children.Add (menuLabel);
            layout.Children.Add (m_Menu);

            Content = layout;
        }

        private void SendMessage (string _action)
        {
            ATManager manager = ATManager.GetInstance ();

            ActionMessage am = new ActionMessage {
                Form = this.GetType ().FullName,
                Action = _action
            };

            manager.MessagingManager.Publish (am);
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            var menuItem = e.SelectedItem as MenuOption;
            Page displayPage = (Page)Activator.CreateInstance (menuItem.TargetType);

            SendMessage (string.Format ("{0} setting menu clicked", menuItem.Title));

            Navigation.PushAsync (displayPage, true);
        }
    }
}

