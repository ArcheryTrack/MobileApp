using System;
using Xamarin.Forms;
using ATMobile.Forms;
using System.Collections.Generic;
using ATMobile.Interfaces;
using ATMobile.Managers;

namespace ATMobile
{
    public class App : Application
    {
        public static string DataFolder;

        public App (string _dataFolder, List<IPlugin> _plugins = null)
        {
            DataFolder = _dataFolder;

            if (_plugins != null) {
                ATManager.GetInstance ().PluginManager.AppendPlugins (_plugins);
            }

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

