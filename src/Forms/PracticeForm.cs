﻿using System;
using System.Collections.Generic;
using ATMobile.Data;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public partial class PracticeForm : ContentPage
    {
        private Archer m_Archer;
        private Practice m_Practice;
        private List<Range> m_Ranges;
        private List<TargetFace> m_TargetFaces;

        private StackLayout m_OutsideLayout;
        private StackLayout m_InsideLayout;

        private Button m_btnSave;
        private Label m_lblDate;
        private DatePicker m_datDate;
        private Label m_lblTime;
        private TimePicker m_timTime;
        private Label m_lblLocation;
        private Picker m_pickLocation;
        private Label m_lblTargetFace;
        private Picker m_pickTargetFace;


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

            m_lblLocation = new Label ();
            m_lblLocation.Text = "Location";

            m_pickLocation = new Picker ();
            m_InsideLayout.Children.Add (m_pickLocation);

            m_Ranges = ATManager.GetInstance ().GetRanges ();
            foreach (var item in m_Ranges) {
                m_pickLocation.Items.Add (item.Name);
            }

            m_lblTargetFace = new Label ();
            m_lblTargetFace.Text = "Target";

            m_pickTargetFace = new Picker ();
            m_InsideLayout.Children.Add (m_pickTargetFace);

            m_TargetFaces = TargetFaceData.GetData ();
            foreach (var item in m_TargetFaces) {
                m_pickLocation.Items.Add (item.Name);
            }

            Content = m_OutsideLayout;
        }

        public void SetupForm (Archer _archer, Practice _practice)
        {
            m_Archer = _archer;
            m_Practice = _practice;

            if (m_Practice != null) {
                m_datDate.Date = m_Practice.DateTime.Date;
                m_timTime.Time = m_Practice.DateTime.TimeOfDay;

                if (m_Practice.RangeId != null) {
                    for (int i = 0; i < m_Ranges.Count; i++) {
                        var range = m_Ranges [i];

                        if (range.Id.Equals (m_Practice.RangeId)) {
                            m_pickLocation.SelectedIndex = i;
                            break;
                        }
                    }
                }

                if (m_Practice.TargetFaceId != null) {
                    for (int i = 0; i < m_TargetFaces.Count; i++) {
                        var target = m_TargetFaces [i];

                        if (target.Id.Equals (m_Practice.TargetFaceId)) {
                            m_pickTargetFace.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        private void OnSave (object sender, EventArgs e)
        {
            if (m_Practice == null) {
                m_Practice = new Practice ();
                m_Practice.Id = Guid.NewGuid ();
                m_Practice.ParentId = m_Archer.Id;
            }

            DateTime date = m_datDate.Date;
            date.AddTicks (m_timTime.Time.Ticks);
            m_Practice.DateTime = date;

            if (m_pickLocation.SelectedIndex >= 0) {
                var range = m_Ranges [m_pickLocation.SelectedIndex];

                m_Practice.RangeId = range.Id;
                m_Practice.RangeName = range.Name;
            } else {
                m_Practice.RangeId = null;
                m_Practice.RangeName = null;
            }

            if (m_pickTargetFace.SelectedIndex >= 0) {
                var target = m_TargetFaces [m_pickTargetFace.SelectedIndex];

                m_Practice.TargetFaceId = target.Id;
            } else {
                m_Practice.TargetFaceId = null;
            }

            ATManager.GetInstance ().Persist (m_Practice);

            Navigation.PopAsync ();
        }
    }
}
