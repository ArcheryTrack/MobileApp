using System;
using ATMobile.Constants;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public abstract class AbstractEntryForm : AbstractForm
    {
        protected StackLayout InsideLayout;
        protected Button SaveButton;

        public abstract void Save ();

        protected AbstractEntryForm (string _title) : base (_title)
        {
            SaveButton = new Button {
                Text = "Save"
            };
            SaveButton.Clicked += OnSave;
            OutsideLayout.Children.Add (SaveButton);

            InsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };
            OutsideLayout.Children.Add (InsideLayout);
        }

        private void OnSave (object sender, EventArgs e)
        {
            Save ();
        }
    }
}

