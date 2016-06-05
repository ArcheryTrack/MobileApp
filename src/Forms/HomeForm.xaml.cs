using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ATMobile.Forms;

namespace ATMobile.Forms
{
    public partial class HomeForm : MasterDetailPage
    {
        public HomeForm()
        {
            InitializeComponent();

            Title = "ArcheryTrack";

            var menuPage = new MenuPage();

            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as ATMobile.Controls.MenuItem);

            Master = menuPage;
            Detail = new NavigationPage(new DefaultForm());
        }

        void NavigateTo(ATMobile.Controls.MenuItem menu)
        {
            Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);

            Detail = new NavigationPage(displayPage);

            IsPresented = false;
        }
    }
}

