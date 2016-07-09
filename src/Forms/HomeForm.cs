using System;
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

            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo (e.SelectedItem as ATMobile.Controls.MenuItem);

            Master = menuPage;
            Detail = new NavigationPage (new DefaultForm ());
        }

        void NavigateTo (ATMobile.Controls.MenuItem menu)
        {
            Page displayPage = (Page)Activator.CreateInstance (menu.TargetType);

            Detail = new NavigationPage (displayPage);

            IsPresented = false;
        }
    }
}

