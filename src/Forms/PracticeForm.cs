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
        ATDateEntry m_datDate;
        ATTimeEntry m_timTime;
        ATRangeEntry m_rngLocation;
        ATTargetEntry m_targetFace;
        ATToggleEntry m_togCountX;

        public PracticeForm () : base ("Practice")
        {
            //Setup the StartDate
            m_datDate = new ATDateEntry ("Date", "Date");
            m_datDate.SelectedDate = DateTime.Now;
            InsideLayout.Children.Add (m_datDate);

            m_timTime = new ATTimeEntry ();
            m_timTime.Title = "Time";
            InsideLayout.Children.Add (m_timTime);

            m_rngLocation = new ATRangeEntry ("Range", "Select the Range");
            InsideLayout.Children.Add (m_rngLocation);

            m_targetFace = new ATTargetEntry ("Target", "Select the target");
            InsideLayout.Children.Add (m_targetFace);

            m_togCountX = new ATToggleEntry ();
            m_togCountX.Title = "Count Xs";
            InsideLayout.Children.Add (m_togCountX);
        }

        public void SetupForm (Archer _archer, Practice _practice)
        {
            m_Archer = _archer;

            m_Practice = _practice;

            if (_practice == null) {
                m_Practice = new Practice {
                    DateTime = DateTime.Now
                };
            }

            m_datDate.SelectedDate = m_Practice.DateTime;
            m_timTime.Time = m_Practice.DateTime.TimeOfDay;

            if (m_Practice.RangeId != null) {
                m_rngLocation.Range = ATManager.GetInstance ().GetRange (_practice.RangeId.Value);
            }

            if (m_Practice.TargetFaceId != null) {
                m_targetFace.TargetFace = ATManager.GetInstance ().GetTargetFace (_practice.TargetFaceId.Value);
            }

            m_togCountX.IsToggled = m_Practice.CountX;
        }

        public override void Save ()
        {
            if (m_Practice == null) {
                m_Practice = new Practice ();
                m_Practice.Id = Guid.NewGuid ();
                m_Practice.ParentId = m_Archer.Id;
            }

            DateTime date = m_datDate.SelectedDate.Value;
            date = date.AddTicks (m_timTime.Time.Ticks);
            m_Practice.DateTime = date;

            if (m_rngLocation.Range != null) {
                m_Practice.RangeId = m_rngLocation.Range.Id;
                m_Practice.RangeName = m_rngLocation.Range.Name;
            } else {
                m_Practice.RangeId = null;
                m_Practice.RangeName = null;
            }

            if (m_targetFace.TargetFace != null) {
                m_Practice.TargetFaceId = m_targetFace.TargetFace.Id;
            }

            ATManager.GetInstance ().Persist (m_Practice);
        }

        public override void ValidateForm (StringBuilder _sb)
        {
            if (m_targetFace.TargetFace == null) {
                _sb.AppendLine ("You must pick a target face.");
            }
        }
    }
}
