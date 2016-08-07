using System;
using ATMobile.Constants;
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

            var menuPage = new MenuPage ();
            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo (e.SelectedItem as Objects.MenuOption);
            Master = menuPage;

            Detail = new ATNavigationPage (new DefaultForm ());
        }

        void NavigateTo (Objects.MenuOption menu)
        {
            Page displayPage = (Page)Activator.CreateInstance (menu.TargetType);

            Detail = new ATNavigationPage (displayPage) {
                BarTextColor = Color.FromHex (UIConstants.NavBarTextColor),
                BarBackgroundColor = Color.FromHex (UIConstants.NavBarColor)
            };

            IsPresented = false;
        }
    }
}

