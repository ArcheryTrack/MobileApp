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
using ATMobile.iOS.Managers;

namespace ATMobile.iOS
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private App m_App;
        private IPlatformManager m_PlatformManager;

        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init ();
            KeyboardOverlapRenderer.Init ();

            string rootAppFolder = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
            string dataFolder = Path.Combine (rootAppFolder, "Library", "ATMobile");

            if (!Directory.Exists (dataFolder)) {
                Directory.CreateDirectory (dataFolder);
            }

            m_PlatformManager = new PlatformManager ();

            m_App = new App (dataFolder, GetPlugins ());
            LoadApplication (m_App);

            return base.FinishedLaunching (app, options);
        }

        private List<IPlugin> GetPlugins ()
        {
            List<IPlugin> plugins = new List<IPlugin> ();

            plugins.Add (new EquipmentPlugin ());

            //TODO - Change where these constants are set
            plugins.Add (new LoggingPlugin (m_PlatformManager, "iOS", "127.0.0.1", "xBG4Y4Y8LMD6mWvwRMhNaVKXBZuRGTFEuxpgcEE9wwvXC6yB"));
            plugins.Add (new PubSubPlugin ());

            return plugins;
        }
    }
}

