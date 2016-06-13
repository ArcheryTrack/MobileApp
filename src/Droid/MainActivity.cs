using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;

namespace ATMobile.Droid
{
    [Activity (Label = "ATMobile.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            global::Xamarin.Forms.Forms.Init (this, bundle);

            string rootAppFolder = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
            string dataFolder = Path.Combine (rootAppFolder, "Library", "ATMobile");

            if (!Directory.Exists (dataFolder)) {
                Directory.CreateDirectory (dataFolder);
            }
            LoadApplication (new App ());
        }
    }
}

