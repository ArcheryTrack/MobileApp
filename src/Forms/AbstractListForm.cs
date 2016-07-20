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

        public AbstractListForm (string _title) : base (_title)
        {
            AddButton = new Button {
                Text = "Add"
            };
            AddButton.Clicked += OnAdd;
            OutsideLayout.Children.Add (AddButton);

            ListFrame = new Frame {
                HasShadow = false,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            OutsideLayout.Children.Add (ListFrame);
        }

        void OnAdd (object sender, EventArgs e)
        {
            Add ();
        }
    }
}

