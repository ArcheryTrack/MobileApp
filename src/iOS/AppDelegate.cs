using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.IO;
using ATMobile.Interfaces;
using ATMobileLogging;
using ATMobile.Plugins.Equipment;

namespace ATMobile.iOS
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init ();

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

            return plugins;
        }
    }
}

