using System;
using ATMobile.Constants;
using ATMobile.Controls;
using Xamarin.Forms;

namespace ATMobile.PickerForms
{
    public class DatePickerForm : ContentPage
    {
        private StackLayout m_OutsideLayout;
        private Label m_lblTitle;
        private CalendarControl m_CalendarControl;
        private Button m_btnCancel;

        public DatePickerForm (string _title)
        {
            Title = _title;

            //Icon = "settings.png";
            BackgroundColor = Color.FromHex (UIConstants.FormBackgroundColor);

            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            m_lblTitle = new Label ();
            m_lblTitle.Text = _title;
            m_OutsideLayout.Children.Add (m_lblTitle);

            m_CalendarControl = new CalendarControl {
                SelectedDate = DateTime.Now,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            m_OutsideLayout.Children.Add (m_CalendarControl);

            m_btnCancel = new Button () {
                Text = "Cancel",
                VerticalOptions = LayoutOptions.End
            };
            m_btnCancel.Clicked += OnCancel;
            m_OutsideLayout.Children.Add (m_btnCancel);

            Content = m_OutsideLayout;
        }

        async void OnCancel (object sender, EventArgs e)
        {
            await Navigation.PopModalAsync ();
        }
    }
}

