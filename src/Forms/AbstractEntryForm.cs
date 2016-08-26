﻿using System;
using System.Text;
using ATMobile.Constants;
using ATMobile.Controls;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public abstract class AbstractEntryForm : AbstractForm
    {
        private Header m_Header;
        protected ATButtonBar ButtonsLayout;
        protected StackLayout InsideLayout;

        protected Button CancelButton;
        protected Button SaveButton;

        public abstract void Save ();
        public abstract void ValidateForm (StringBuilder _sb);

        protected AbstractEntryForm (string _title) : base (_title, 0, 0)
        {
            m_Header = new Header (_title) {
                Margin = new Thickness (0, 0, 0, 0)
            };
            OutsideLayout.Children.Add (m_Header);

            ButtonsLayout = new ATButtonBar ();
            OutsideLayout.Children.Add (ButtonsLayout);

            CancelButton = ButtonsLayout.Add ("Cancel", LayoutOptions.StartAndExpand);
            CancelButton.Clicked += OnCancel;

            SaveButton = ButtonsLayout.Add ("Save", LayoutOptions.EndAndExpand);
            SaveButton.Clicked += OnSave;

            InsideLayout = new StackLayout {
                Spacing = 5,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = 5,
                Margin = new Thickness (20, 0, 20, 20)
            };
            OutsideLayout.Children.Add (InsideLayout);
        }

        private void OnCancel (object sender, EventArgs e)
        {
            PublishActionMessage ("Cancel Pressed");

            Navigation.PopModalAsync (true);
        }

        private void OnSave (object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder ();
            ValidateForm (sb);

            if (sb.Length > 0) {
                DisplayAlert ("Warning", sb.ToString (), "OK");
            } else {
                PublishActionMessage ("Save Pressed");
                Save ();
                Navigation.PopModalAsync (true);
            }
        }
    }
}

