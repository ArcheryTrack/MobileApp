using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ATMobile.Controls;

namespace ATMobile.Forms
{
    public partial class ArchersForm : ContentPage
    {
        public ListView ArcherList { get; set; }

        public ArchersForm()
        {
            InitializeComponent();

            Title = "Archers";

            //Icon = "settings.png";
            BackgroundColor = Color.FromHex("EEEEEE");

            ArcherList = new ArcherListView();

            var layout = new StackLayout { 
                Spacing = 0, 
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            layout.Children.Add(ArcherList);

            Content = layout;
        }

        void OnAdd(object sender, EventArgs e)
        {
            Page addArcher = new ArcherForm();
            Navigation.PushAsync(addArcher);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Load list
        }
    }
}

