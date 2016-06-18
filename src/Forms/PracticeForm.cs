using System;
using System.Collections.Generic;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public partial class PracticeForm : ContentPage
    {
        private Archer m_Archer;
        private Practice m_Practice;

        private StackLayout m_OutsideLayout;
        private StackLayout m_InsideLayout;
        private Label m_lblDate;
        private Label m_lblTime;
        private DatePicker m_datDate;
        private TimePicker m_timTime;
        private Button m_btnSave;


        public PracticeForm ()
        {
            Title = "Practice";

            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            m_btnSave = new Button {
                Text = "Save"
            };
            m_btnSave.Clicked += OnSave;
            m_OutsideLayout.Children.Add (m_btnSave);

            m_InsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };
            m_OutsideLayout.Children.Add (m_InsideLayout);

            DateTime now = DateTime.Now;

            m_lblDate = new Label ();
            m_lblDate.Text = "Date";
            m_InsideLayout.Children.Add (m_lblDate);

            m_datDate = new DatePicker ();
            m_datDate.Date = now.Date;
            m_InsideLayout.Children.Add (m_datDate);

            m_lblTime = new Label ();
            m_lblTime.Text = "Time";
            m_InsideLayout.Children.Add (m_lblTime);

            m_timTime = new TimePicker ();
            m_timTime.Time = now.TimeOfDay;
            m_InsideLayout.Children.Add (m_timTime);

            Content = m_OutsideLayout;
        }

        public void SetupForm (Archer _archer, Practice _practice)
        {
            m_Archer = _archer;
            m_Practice = _practice;

            if (m_Practice != null) {
                m_datDate.Date = m_Practice.DateTime.Date;
                m_timTime.Time = m_Practice.DateTime.TimeOfDay;
            }
        }

        private void OnSave (object sender, EventArgs e)
        {
            if (m_Practice == null) {
                m_Practice = new Practice ();
                m_Practice.Id = Guid.NewGuid ();
            }

            DateTime date = m_datDate.Date;
            date.AddTicks (m_timTime.Time.Ticks);

            ATManager.GetInstance ().Persist (m_Practice);

            Navigation.PopAsync ();
        }
    }
}
