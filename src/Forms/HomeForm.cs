using System;
using ATMobile.Constants;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class HomeForm : MasterDetailPage
    {
        private App m_App;

        public HomeForm (App _app)
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

        void NavigateTo (MenuOption menu)
        {
            Page displayPage = null;

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
    }
}

