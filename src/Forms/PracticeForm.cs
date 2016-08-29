using System;
using System.Collections.Generic;
using System.Text;
using ATMobile.Controls;
using ATMobile.Data;
using ATMobile.Helpers;
using ATMobile.Managers;
using ATMobile.Objects;
using ATMobile.PickerForms;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class PracticeForm : AbstractEntryForm
    {
        private Archer m_Archer;
        private Practice m_Practice;

        //Picked values
        private DateTime m_Date;
        private TargetFace m_TargetFace;
        private Range m_Location;

        private Grid m_Layout;

        private bool m_bolDatePicked;
        private ATLabel m_lblDate;
        private Button m_btnPickDate;

        private ATLabel m_lblTime;
        private TimePicker m_timTime;

        private ATLabel m_lblLocation;
        private Button m_btnPickLocation;

        private ATLabel m_lblTargetFace;
        private Button m_btnPickTargetFace;

        public PracticeForm () : base ("Practice")
        {
            //Setup grid to hold the controls
            m_Layout = new Grid {
                Padding = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute) //Date
                    },
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute) //Time
                    },
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute) //Location
                    },
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute) //Target
                    }
                },
                ColumnDefinitions = {
                    new ColumnDefinition {
                        Width = new GridLength(1, GridUnitType.Star)
                    },
                    new ColumnDefinition {
                        Width = new GridLength(80, GridUnitType.Absolute)
                    }
                }
            };
            InsideLayout.Children.Add (m_Layout);

            m_Date = DateTime.Now;

            //Setup the StartDate
            m_lblDate = new ATLabel {
                Text = "Date:",
                HeightRequest = 40,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            m_Layout.Children.Add (m_lblDate, 0, 0);

            m_btnPickDate = new Button {
                Text = "Pick",
                WidthRequest = 80,
                HeightRequest = 40,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End
            };
            m_btnPickDate.Clicked += PickDate;
            m_Layout.Children.Add (m_btnPickDate, 1, 0);

            m_lblTime = new ATLabel {
                Text = "Time"
            };
            m_Layout.Children.Add (m_lblTime, 0, 1);

            m_timTime = new TimePicker ();
            m_timTime.Time = m_Date.TimeOfDay;
            m_Layout.Children.Add (m_timTime, 1, 1);

            SetDateTimeText ();

            //Setup Location
            m_lblLocation = new ATLabel {
                Text = "Select location",
                HeightRequest = 40,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            m_Layout.Children.Add (m_lblLocation, 0, 2);

            m_btnPickLocation = new Button {
                Text = "Pick",
                WidthRequest = 80,
                HeightRequest = 40,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End
            };
            m_btnPickLocation.Clicked += PickLocation;
            m_Layout.Children.Add (m_btnPickLocation, 1, 2);


            //Add the Target Face at the botton.
            m_lblTargetFace = new ATLabel {
                Text = "Select Target Face",
                VerticalTextAlignment = TextAlignment.Center
            };
            m_Layout.Children.Add (m_lblTargetFace, 0, 3);

            m_btnPickTargetFace = new Button {
                Text = "Pick",
                WidthRequest = 80,
                HeightRequest = 40,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End
            };
            m_btnPickTargetFace.Clicked += PickTargetFace;
            m_Layout.Children.Add (m_btnPickTargetFace, 1, 3);
        }

        public void SetupForm (Archer _archer, Practice _practice)
        {
            m_Archer = _archer;
            m_Practice = _practice;

            if (m_Practice != null) {
                m_Date = m_Practice.DateTime;

                if (m_Practice.RangeId != null) {
                    m_Location = ATManager.GetInstance ().GetRange (_practice.RangeId.Value);
                    SetRangeText ();
                }

                if (m_Practice.TargetFaceId != null) {
                    m_TargetFace = ATManager.GetInstance ().GetTargetFace (_practice.TargetFaceId.Value);
                    SetTargetFaceText ();
                }
            }
        }

        async private void PickTargetFace (object sender, EventArgs e)
        {
            TargetFacePicker picker = new TargetFacePicker ();
            picker.ItemPicked += TargetFacePicked;

            await Navigation.PushModalAsync (picker);
        }

        private void TargetFacePicked (TargetFace _targetFace)
        {
            m_TargetFace = _targetFace;
            SetTargetFaceText ();
        }

        private void SetTargetFaceText ()
        {
            if (m_TargetFace != null) {
                m_lblTargetFace.Text = m_TargetFace.Name;
            }
        }

        async private void PickLocation (object sender, EventArgs e)
        {
            RangePicker picker = new RangePicker ();
            picker.ItemPicked += RangePicked;

            await Navigation.PushModalAsync (picker);
        }

        private void RangePicked (Range _range)
        {
            m_Location = _range;
            SetRangeText ();
        }

        private void SetRangeText ()
        {
            m_lblLocation.Text = m_Location.Name;
        }

        async private void PickDate (object sender, EventArgs e)
        {
            DatePickerForm picker = new DatePickerForm ("Select Date");
            picker.OnDateSelected += DatePicked;

            await Navigation.PushModalAsync (picker);
        }

        private void DatePicked (DateTime _date)
        {
            m_Date = _date;
            m_bolDatePicked = true;
            SetDateTimeText ();
        }

        private void SetDateTimeText ()
        {
            m_lblDate.Text = m_Date.ToDisplayDate ("Date");
        }

        public override void ValidateForm (StringBuilder _sb)
        {
            if (m_TargetFace == null) {
                _sb.AppendLine ("You must pick a target face.");
            }
        }

        public override void Save ()
        {
            if (m_Practice == null) {
                m_Practice = new Practice ();
                m_Practice.Id = Guid.NewGuid ();
                m_Practice.ParentId = m_Archer.Id;
            }

            DateTime date = m_Date;
            date = date.AddTicks (m_timTime.Time.Ticks);
            m_Practice.DateTime = date;

            if (m_Location != null) {
                m_Practice.RangeId = m_Location.Id;
                m_Practice.RangeName = m_Location.Name;
            } else {
                m_Practice.RangeId = null;
                m_Practice.RangeName = null;
            }

            if (m_TargetFace != null) {
                m_Practice.TargetFaceId = m_TargetFace.Id;
            }

            ATManager.GetInstance ().Persist (m_Practice);
        }
    }
}
