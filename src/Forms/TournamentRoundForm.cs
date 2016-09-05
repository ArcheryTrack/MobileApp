using System;
using System.Text;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentRoundForm : AbstractEntryForm
    {
        private Tournament m_Tournament;
        private Round m_Round;
        private ATDateEntry m_datDate;
        private ATDistanceEntry m_distMeasurement;
        private ATTextEntry m_txtNumberOfEnds;
        private ATTextEntry m_txtArrowsPerEnd;
        private ATTargetEntry m_targetEntry;
        private ATToggleEntry m_togCountX;
        private ATEditor m_txtNote;
        private bool m_New;

        public TournamentRoundForm () : base ("Edit Round")
        {
            m_datDate = new ATDateEntry ("Date", "Date");
            InsideLayout.Children.Add (m_datDate);

            m_distMeasurement = new ATDistanceEntry ();
            InsideLayout.Children.Add (m_distMeasurement);

            m_txtNumberOfEnds = new ATTextEntry {
                Title = "Number of Ends",
                Keyboard = Keyboard.Numeric
            };
            InsideLayout.Children.Add (m_txtNumberOfEnds);

            m_txtArrowsPerEnd = new ATTextEntry {
                Title = "Arrows Per End",
                Keyboard = Keyboard.Numeric
            };
            InsideLayout.Children.Add (m_txtArrowsPerEnd);

            m_targetEntry = new ATTargetEntry ("Target", "Target Face");
            InsideLayout.Children.Add (m_targetEntry);

            m_togCountX = new ATToggleEntry {
                Title = "Count Xs?"
            };
            InsideLayout.Children.Add (m_togCountX);

            m_txtNote = new ATEditor (80, 300);
            InsideLayout.Children.Add (m_txtNote);
        }

        public override void ValidateForm (StringBuilder _sb)
        {

        }

        public override void Save ()
        {
            m_Round.Date = m_datDate.SelectedDate.Value;
            m_Round.Note = m_txtNote.Text;

            ATManager.GetInstance ().Persist (m_Round);
        }

        public void SetupForm (Tournament _tournament, Round _round, bool _new = false)
        {
            m_Tournament = _tournament;
            m_Round = _round;
            m_New = _new;

            if (m_Round.Date == DateTime.MinValue) {
                m_Round.Date = _tournament.StartDateTime;
            }

            m_datDate.SelectedDate = m_Round.Date;
            m_distMeasurement.Distance = m_Round.Distance;
            m_txtNumberOfEnds.Text = Convert.ToString (m_Round.NumberOfEnds);
            m_txtArrowsPerEnd.Text = Convert.ToString (m_Round.ArrowsPerEnd);

            if (!Guid.Empty.Equals (m_Round.TargetFaceId)) {
                TargetFace face = ATManager.GetInstance ().GetTargetFace (m_Round.TargetFaceId);
                m_targetEntry.TargetFace = face;
            }
            m_togCountX.IsToggled = m_Round.CountX;

            m_txtNote.Text = m_Round.Note;

            if (!_new) {
                m_togCountX.IsEnabled = false;
                m_txtArrowsPerEnd.IsEnabled = false;
                m_txtNumberOfEnds.IsEnabled = false;
                m_targetEntry.IsEnabled = false;
            }
        }
    }
}

