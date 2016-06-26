using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ATMobile.Forms
{
    public partial class LoginForm : ContentPage
    {
        private StackLayout m_OutsideLayout;
        private StackLayout m_InsideLayout;
        private Entry m_txtUsername;
        private Entry m_txtPassword;
        private Button m_btnLogin;
        private Label m_lblTitle;

        public LoginForm ()
        {
            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            m_lblTitle = new Label ();
            m_lblTitle.Text = "ArcheryTrack";
            m_lblTitle.FontFamily = "sans-serif";
            m_lblTitle.FontSize = 40.0;
            m_lblTitle.FontAttributes = FontAttributes.Bold;
            m_OutsideLayout.Children.Add (m_lblTitle);

            m_InsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };
            m_OutsideLayout.Children.Add (m_InsideLayout);

            m_txtUsername = new Entry ();
            m_txtUsername.Placeholder = "Username";
            m_txtUsername.Keyboard = Keyboard.Email;
            m_InsideLayout.Children.Add (m_txtUsername);

            m_txtPassword = new Entry ();
            m_txtPassword.Placeholder = "Password";
            m_txtPassword.IsPassword = true;
            m_InsideLayout.Children.Add (m_txtPassword);

            m_btnLogin = new Button {
                Text = "Login"
            };
            m_btnLogin.Clicked += OnLogin;
            m_InsideLayout.Children.Add (m_btnLogin);

            Content = m_OutsideLayout;
        }

        void OnLogin (object sender, EventArgs e)
        {
            Navigation.PopAsync (true);
        }
    }
}

