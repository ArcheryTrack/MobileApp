using System;
using ATMobile.Constants;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public abstract class AbstractListForm : AbstractForm
    {
        protected Button AddButton;
        protected Frame ListFrame;

        public abstract void Add ();

        public AbstractListForm (
            string _title,
            string _addButtonText = "Add") : base (_title)
        {
            AddButton = new Button {
                Text = _addButtonText,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                MinimumWidthRequest = 200
            };
            AddButton.Clicked += OnAdd;
            OutsideLayout.Children.Add (AddButton);

            ListFrame = new Frame {
                HasShadow = false,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            if (Device.Idiom == TargetIdiom.Phone) {
                ListFrame.Margin = new Thickness (10, 5, 10, 10);
            } else {
                ListFrame.Margin = new Thickness (20, 15, 20, 20);
            }


            OutsideLayout.Children.Add (ListFrame);
        }

        void OnAdd (object sender, EventArgs e)
        {
            PublishActionMessage ("Add Pressed");

            Add ();
        }
    }
}

