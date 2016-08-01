using System;
using ATMobile.Constants;
using ATMobile.Controls;
using ATMobile.Managers;
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
                Content = new Label {
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

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            var menuItem = e.SelectedItem as MenuOption;
            Page displayPage = (Page)Activator.CreateInstance (menuItem.TargetType);
            Navigation.PushAsync (displayPage, true);
        }
    }
}

