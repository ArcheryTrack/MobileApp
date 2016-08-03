using System;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class AboutForm : AbstractForm
    {
        public AboutForm () : base ("About")
        {
            WebView webView = new WebView {
                Source = new UrlWebViewSource {
                    Url = "http://www.archerytrack.com/",
                },
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            OutsideLayout.Children.Add (webView);
        }
    }
}

