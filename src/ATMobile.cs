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

        }

        protected override void OnSleep ()
        {
            IEnumerable<IPlugin> plugins = ATManager.GetInstance ().PluginManager.Plugins;

            foreach (var plugin in plugins) {
                plugin.OnSleep ();
            }
        }

        protected override void OnResume ()
        {
            IEnumerable<IPlugin> plugins = ATManager.GetInstance ().PluginManager.Plugins;

            foreach (var plugin in plugins) {
                plugin.OnResume ();
            }
        }
    }
}

