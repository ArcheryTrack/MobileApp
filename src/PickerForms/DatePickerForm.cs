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
        private Label m_lblTitle;
        private CalendarControl m_CalendarControl;
        private Button m_btnCancel;

        public OnDateSelectedDelegate OnDateSelected;

        public DatePickerForm (string _title, bool showDecade = false)
        {
            m_OutsideLayout = new StackLayout {
                Spacing = 0,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 0
            };

            AbsoluteLayout header = new AbsoluteLayout {
                BackgroundColor = Color.FromHex (UIConstants.NavBarColor),
                Margin = new Thickness (0, 0, 0, 0),
                MinimumHeightRequest = 65,
                HeightRequest = 65
            };
            m_OutsideLayout.Children.Add (header);

            m_lblTitle = new Label {
                Text = _title,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.FromHex (UIConstants.NavBarTextColor),
                FontAttributes = FontAttributes.Bold
            };

            AbsoluteLayout.SetLayoutFlags (m_lblTitle,
                AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds (m_lblTitle,
                new Rectangle (0.5,
                    0.7, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            header.Children.Add (m_lblTitle);

            BoxView line = new BoxView {
                HeightRequest = 1,
                Color = Color.FromHex (UIConstants.LineColor),
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            AbsoluteLayout.SetLayoutFlags (
                line,
                AbsoluteLayoutFlags.WidthProportional);

            AbsoluteLayout.SetLayoutBounds (line,
                new Rectangle (0,
                               64.5,
                               1,
                               AbsoluteLayout.AutoSize));

            header.Children.Add (line);

            Title = _title;
            Padding = new Thickness (0, 20, 0, 0);

            //Icon = "settings.png";
            BackgroundColor = Color.FromHex (UIConstants.DetailFormBackgroundColor);

            m_CalendarControl = new CalendarControl (showDecade) {
                SelectedDate = DateTime.Now,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness (10, 5, 10, 5)
            };
            m_CalendarControl.OnDateSelected += DateSelected;
            m_OutsideLayout.Children.Add (m_CalendarControl);

            m_btnCancel = new Button () {
                Text = "Cancel",
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness (10, 5, 10, 20)
            };
            m_btnCancel.Clicked += OnCancel;
            m_OutsideLayout.Children.Add (m_btnCancel);

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

