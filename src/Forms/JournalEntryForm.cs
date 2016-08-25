using System;
using System.Text;
using ATMobile.Controls;
using ATMobile.Helpers;
using ATMobile.Managers;
using ATMobile.Objects;
using ATMobile.PickerForms;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class JournalEntryForm : AbstractEntryForm
    {
        private JournalEntry m_Entry;
        private Archer m_Archer;

        private Label m_lblArcher;
        private ATDatePicker m_dpDate;
        private Editor m_txtNote;

        public JournalEntryForm () : base ("Journal Entry")
        {
            m_lblArcher = new Label {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center
            };
            InsideLayout.Children.Add (m_lblArcher);

            m_dpDate = new ATDatePicker (
                "Journal Date",
                "Select the date of the Journal Entry") {
                SelectedDate = DateTime.Now,
                Margin = new Thickness (0, 40, 0, 0)
            };
            InsideLayout.Children.Add (m_dpDate);

            m_txtNote = new Editor {
                Margin = new Thickness (0, 65, 0, 0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            InsideLayout.Children.Add (m_txtNote);
        }

        public override void ValidateForm (StringBuilder _sb)
        {

        }

        public override void Save ()
        {
            if (m_dpDate.SelectedDate.HasValue) {
                m_Entry.DateTime = m_dpDate.SelectedDate.Value;
            } else {
                m_Entry.DateTime = DateTime.Now;
            }
            m_Entry.EntryText = m_txtNote.Text;

            ATManager.GetInstance ().Persist (m_Entry);
        }

        public void SetupForm (JournalEntry _entry, Archer _archer)
        {
            m_Archer = _archer;

            if (_entry == null) {
                DateTime now = DateTime.Now;
                m_dpDate.SelectedDate = now;

                m_Entry = new JournalEntry ();
                m_Entry.DateTime = now;
                m_Entry.ParentId = _archer.Id;
            } else {
                m_Entry = _entry;
                m_dpDate.SelectedDate = _entry.DateTime;
            }

            m_lblArcher.Text = _archer.FullName;
            m_txtNote.Text = m_Entry.EntryText;
        }

    }
}

