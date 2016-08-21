using System;
using ATMobile.Constants;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class AboutForm : AbstractForm
    {
        public AboutForm () : base ("About")
        {
            WebView webView = new WebView {
                Source = new HtmlWebViewSource {
                    Html = AboutConstants.AboutHtml,
                },
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness (5, 5, 5, 5)
            };

            OutsideLayout.Children.Add (webView);
        }
    }
}

