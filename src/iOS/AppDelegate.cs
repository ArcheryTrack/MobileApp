using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using System.IO;
using ATMobile.Interfaces;
using ATMobile.Plugins.Logging;
using ATMobile.Plugins.Equipment;
using ATMobile.Plugins.PubSub;
using KeyboardOverlap.Forms.Plugin.iOSUnified;

namespace ATMobile.iOS
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init ();
            KeyboardOverlapRenderer.Init ();

            string rootAppFolder = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
            string dataFolder = Path.Combine (rootAppFolder, "Library", "ATMobile");

            if (!Directory.Exists (dataFolder)) {
                Directory.CreateDirectory (dataFolder);
            }

            LoadApplication (new App (dataFolder, GetPlugins ()));

            return base.FinishedLaunching (app, options);
        }

        private List<IPlugin> GetPlugins ()
        {
            List<IPlugin> plugins = new List<IPlugin> ();

            plugins.Add (new EquipmentPlugin ());
            plugins.Add (new LoggingPlugin ());
            plugins.Add (new PubSubPlugin ());

            return plugins;
        }
    }
}

