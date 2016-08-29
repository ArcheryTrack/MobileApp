using System;
using ATMobile.Constants;
using ATMobile.Controls;
using ATMobile.Delegates;
using Xamarin.Forms;

namespace ATMobile.PickerForms
{
    public class DatePickerForm : ContentPage
    {
        private StackLayout m_OutsideLayout;
        private CalendarControl m_CalendarControl;
        private Button m_btnCancel;

        public OnDateSelectedDelegate OnDateSelected;

        public DatePickerForm (string _title, bool showDecade = false)
        {
            Title = _title;
            Padding = new Thickness (0, 0, 0, 0);
            BackgroundColor = Color.FromHex (UIConstants.DetailFormBackgroundColor);

            m_OutsideLayout = new StackLayout {
                Spacing = 0,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 0
            };

            Header header = new Header (_title) {
                Margin = new Thickness (0, 0, 0, 0)
            };
            m_OutsideLayout.Children.Add (header);

            ATButtonBar buttonBar = new ATButtonBar ();
            m_OutsideLayout.Children.Add (buttonBar);

            m_btnCancel = buttonBar.Add ("Cancel", LayoutOptions.Start);
            m_btnCancel.Clicked += OnCancel;

            m_CalendarControl = new CalendarControl (showDecade) {
                SelectedDate = DateTime.Now,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness (10, 5, 10, 5)
            };
            m_CalendarControl.OnDateSelected += DateSelected;
            m_OutsideLayout.Children.Add (m_CalendarControl);

            Content = m_OutsideLayout;
        }

        public DateTime? SelectedDate {
            get {
                return m_CalendarControl.SelectedDate;
            }
            set {
                if (value == null) {
                    m_CalendarControl.SelectedDate = DateTime.Now;
                } else {
                    m_CalendarControl.SelectedDate = value.Value;
                }
            }
        }

        public DateTime MinimumDate {
            get {
                return m_CalendarControl.MinimumDate;
            }
            set {
                m_CalendarControl.MinimumDate = value;
            }
        }

        public DateTime MaximumDate {
            get {
                return m_CalendarControl.MaximumDate;
            }
            set {
                m_CalendarControl.MaximumDate = value;
            }
        }

        async void DateSelected (DateTime _selectedDate)
        {
            var selected = OnDateSelected;
            if (selected != null) {
                selected (_selectedDate);
            }

            await Navigation.PopModalAsync ();
        }

        async void OnCancel (object sender, EventArgs e)
        {
            await Navigation.PopModalAsync ();
        }
    }
}

