using System;
using System.Threading.Tasks;
using ATMobile.Constants;
using ATMobile.Interfaces;
using ATMobile.Managers;
using ATMobile.Messages;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class MainForm : MasterDetailPage, IMainForm
    {
        public bool FiredAppStarted = false;
        private App m_App;

        public MainForm (App _app)
        {
            m_App = _app;

            Title = "ArcheryTrack";

            PopulateInitialData ();

            var menuPage = new MenuPage ();
            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo (e.SelectedItem as Objects.MenuOption);
            Master = menuPage;

            Detail = new ATNavigationPage (new DefaultForm ());
        }

        private void PopulateInitialData ()
        {
            ATManager manager = ATManager.GetInstance ();

            InitialDataManager dataManager = new InitialDataManager (manager);
            dataManager.PopulateData ();
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

        private void NavigateTo (MenuOption menu)
        {
            Page displayPage = null;

            SendMessage (string.Format ("{0} menu clicked", menu.Title));

            if (menu is PluginMenuOption) {
                PluginMenuOption pmo = (PluginMenuOption)menu;

                displayPage = pmo.Plugin.GetPage (pmo);

            } else {
                displayPage = (Page)Activator.CreateInstance (menu.TargetType);
            }

            if (displayPage != null) {
                Detail = new ATNavigationPage (displayPage) {
                    BarTextColor = Color.FromHex (UIConstants.NavBarTextColor),
                    BarBackgroundColor = Color.FromHex (UIConstants.NavBarColor)
                };

                IsPresented = false;
            }
        }

        public Task<bool> ShowAlert (string _title, string _message, string _accept, string _cancel)
        {
            return DisplayAlert (_title, _message, _accept, _cancel);
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            if (!FiredAppStarted) {
                FiredAppStarted = true;
                ATManager.GetInstance ().MessagingManager.Publish (this);
            }
        }

        public void PushModal (ContentPage _form)
        {
            Navigation.PushModalAsync (_form);
        }
    }
}

