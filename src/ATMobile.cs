using System;
using Xamarin.Forms;
using ATMobile.Forms;

namespace ATMobile
{
    public class App : Application
    {
        public static string DataFolder;

        public App (string _dataFolder)
        {
            DataFolder = _dataFolder;

            // The root page of your application
            MainPage = new HomeForm (this);
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }
    }
}

