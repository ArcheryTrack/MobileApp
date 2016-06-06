using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ATMobile.Forms
{
    public partial class ArcherForm : ContentPage
    {
        public ArcherForm()
        {
            InitializeComponent();

            Title = "Archer";
        }

        void OnSave(object sender, EventArgs e)
        {
            //TODO save

            Navigation.PopAsync();
        }
    }
}

