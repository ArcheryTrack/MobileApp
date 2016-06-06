using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ATMobile.Forms
{
    public partial class ArchersForm : ContentPage
    {
        public ArchersForm()
        {
            InitializeComponent();

            Title = "Archers";
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

