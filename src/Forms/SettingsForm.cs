using System;
using ATMobile.Constants;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class SettingsForm : ContentPage
    {
        private StackLayout m_OutsideLayout;
        private Button m_btnLogin;
        private Button m_btnLogout;
        private Label m_lblUsername;

        public SettingsForm ()
        {
            Title = "Settings";

            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            var manager = ATManager.GetInstance ();
            string username = manager.GetSettingValue (SettingConstants.Username);
            string token = manager.GetSettingValue (SettingConstants.UserToken);

            if (username != null
                && token != null) {
                m_lblUsername = new Label ();
                m_lblUsername.Text = string.Format ("Logged in as {0}", username);
                m_OutsideLayout.Children.Add (m_lblUsername);

                m_btnLogout = new Button {
                    Text = "Logout"
                };
                m_btnLogout.Clicked += OnLogout;
                m_OutsideLayout.Children.Add (m_btnLogout);
            } else {
                m_btnLogin = new Button {
                    Text = "Login"
                };
                m_btnLogin.Clicked += OnLogin;
                m_OutsideLayout.Children.Add (m_btnLogin);
            }

            Content = m_OutsideLayout;
        }

        private void OnLogin (object sender, EventArgs e)
        {
            Navigation.PushAsync (new LoginForm ());
        }

        private void OnLogout (object sender, EventArgs e)
        {
            var manager = ATManager.GetInstance ();
            manager.SetSetting (SettingConstants.Username, null);
            manager.SetSetting (SettingConstants.UserToken, null);
        }
    }
}

