using System;
using ATMobile.Delegates;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class CalendarControl : ContentView, IDisposable
    {
        private DateTime m_MinDate;
        private DateTime m_MaxDate;
        private DateTime m_SelectedDate;

        //Overall container
        private StackLayout m_Outside;

        //Header Container
        private StackLayout m_Header;
        private Button m_btnPreviousMonth;
        private Button m_btnNextMonth;
        private Button m_btnPreviousYear;
        private Button m_btnNextYear;
        private Label m_lblTitle;
        private IntButton [] m_btnDates;

        //Calendar Grid
        private Grid m_gridCal;

        //Footer Container
        private StackLayout m_Footer;

        public OnDateSelectedDelegate OnDateSelected;

        public CalendarControl ()
        {
            m_MinDate = new DateTime (1900, 1, 1);
            m_MaxDate = new DateTime (2100, 12, 31);
            m_SelectedDate = DateTime.Now.Date;

            //Setup the containing control.
            m_Outside = new StackLayout {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            //Setup the Header control (May want to use a grid)
            m_Header = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill
            };
            m_Outside.Children.Add (m_Header);

            m_btnPreviousYear = new Button {
                Text = "<<",
                HorizontalOptions = LayoutOptions.Start
            };
            m_btnPreviousYear.Clicked += PreviousYear;
            m_Header.Children.Add (m_btnPreviousYear);

            m_btnPreviousMonth = new Button {
                Text = "<",
                HorizontalOptions = LayoutOptions.Start
            };
            m_btnPreviousMonth.Clicked += PreviousMonth;
            m_Header.Children.Add (m_btnPreviousMonth);

            m_lblTitle = new Label {
                Text = "",
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            m_Header.Children.Add (m_lblTitle);

            m_btnNextMonth = new Button {
                Text = ">",
                HorizontalOptions = LayoutOptions.End
            };
            m_btnNextMonth.Clicked += NextMonth;
            m_Header.Children.Add (m_btnNextMonth);

            m_btnNextYear = new Button {
                Text = ">>",
                HorizontalOptions = LayoutOptions.End
            };
            m_btnNextYear.Clicked += NextYear;
            m_Header.Children.Add (m_btnNextYear);


            //Setup the day grid
            m_gridCal = new Grid {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            m_gridCal.Children.Add (new Label { Text = "S", HorizontalTextAlignment = TextAlignment.Center }, 0, 0);
            m_gridCal.Children.Add (new Label { Text = "M", HorizontalTextAlignment = TextAlignment.Center }, 1, 0);
            m_gridCal.Children.Add (new Label { Text = "T", HorizontalTextAlignment = TextAlignment.Center }, 2, 0);
            m_gridCal.Children.Add (new Label { Text = "W", HorizontalTextAlignment = TextAlignment.Center }, 3, 0);
            m_gridCal.Children.Add (new Label { Text = "T", HorizontalTextAlignment = TextAlignment.Center }, 4, 0);
            m_gridCal.Children.Add (new Label { Text = "F", HorizontalTextAlignment = TextAlignment.Center }, 5, 0);
            m_gridCal.Children.Add (new Label { Text = "S", HorizontalTextAlignment = TextAlignment.Center }, 6, 0);

            m_btnDates = new IntButton [42];
            for (int i = 0; i < 42; i++) {
                m_btnDates [i] = new IntButton (0);
                m_btnDates [i].OnClicked += DatePressed;

                int y = (i / 7) + 1; //Add one for header row
                int x = i % 7;

                m_gridCal.Children.Add (m_btnDates [i], x, y);
            }

            m_Outside.Children.Add (m_gridCal);

            m_Footer = new StackLayout () {
                Orientation = StackOrientation.Horizontal
            };
            m_Outside.Children.Add (m_Footer);

            this.Content = m_Outside;

            RefreshCalendar ();
        }

        private void DatePressed (int dayOfMonth)
        {
            m_SelectedDate = new DateTime (m_SelectedDate.Year, m_SelectedDate.Month, dayOfMonth);

            var selected = OnDateSelected;
            if (selected != null) {
                selected (m_SelectedDate);
            }
        }

        private void PreviousMonth (object sender, EventArgs e)
        {
            m_SelectedDate = m_SelectedDate.AddMonths (-1);
            RefreshCalendar ();
        }

        private void NextMonth (object sender, EventArgs e)
        {
            m_SelectedDate = m_SelectedDate.AddMonths (1);
            RefreshCalendar ();
        }

        private void PreviousYear (object sender, EventArgs e)
        {
            m_SelectedDate = m_SelectedDate.AddYears (-1);
            RefreshCalendar ();
        }

        private void NextYear (object sender, EventArgs e)
        {
            m_SelectedDate = m_SelectedDate.AddYears (1);
            RefreshCalendar ();
        }

        public DateTime MinimumDate {
            get { return m_MinDate; }
            set { m_MinDate = value; }
        }

        public DateTime MaximumDate {
            get { return m_MaxDate; }
            set { m_MaxDate = value; }
        }

        public DateTime SelectedDate {
            get { return m_SelectedDate; }
            set {
                m_SelectedDate = value;
                RefreshCalendar ();
            }
        }

        private void RefreshCalendar ()
        {
            m_lblTitle.Text = string.Format ("{0}/{1}", m_SelectedDate.Date.Month, m_SelectedDate.Date.Year);

            DateTime firstOfMonth = new DateTime (m_SelectedDate.Year, m_SelectedDate.Month, 1);
            DateTime lastOfMonth = firstOfMonth.AddMonths (1).AddDays (-1);
            DayOfWeek dayOfWeekFirstOfMonth = firstOfMonth.DayOfWeek;

            foreach (var button in m_btnDates) {
                button.SetValue (0);
                button.IsEnabled = false;
            }

            int start = (int)dayOfWeekFirstOfMonth;
            int end = start + lastOfMonth.Day;
            int dayCount = 1;
            for (int i = start; i < end; i++) {
                m_btnDates [i].SetValue (dayCount);
                m_btnDates [i].IsEnabled = true;
                dayCount++;
            }
        }

        public void Dispose ()
        {
            foreach (var button in m_btnDates) {
                button.OnClicked -= DatePressed;
            }
        }
    }
}

