﻿using System;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class AboutForm : AbstractForm
    {
        private const string HTML = @"
            <html>
                <head>                    
                </head>
                <body>
                    <h1>ArcheryTrack</h1>
                    <p>A simple data base approach to tracking practice and tournaments.</p>
                    <br>
                    <p>OpenSource Projects In Use</p>
                    <ul>
                        <li>LiteDb</li>
                        <li>OxyPlot</li>
                    </ul>
                </body>    
            </html> ";

        public AboutForm () : base ("About")
        {
            WebView webView = new WebView {
                Source = new HtmlWebViewSource {
                    Html = HTML,
                },
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            OutsideLayout.Children.Add (webView);
        }
    }
}
