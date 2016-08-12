using System;
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

        private Grid m_Layout;

        private Label m_lblArcher;
        private Label m_lblDate;
        private Button m_btnPickDate;
        private bool m_bolDatePicked;
        private DateTime m_Date;

        private Editor m_txtNote;

        public JournalEntryForm () : base ("Journal Entry")
        {
            m_lblArcher = new Label {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center
            };
            InsideLayout.Children.Add (m_lblArcher);

            //Setup grid to hold the controls
            m_Layout = new Grid {
                Padding = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute) //Date
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

            m_lblDate = new Label {
                Text = "Date/Time"
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

            m_txtNote = new Editor {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            InsideLayout.Children.Add (m_txtNote);
        }

        async private void PickDate (object sender, EventArgs e)
        {
            DatePickerForm picker = new DatePickerForm ("Select Date");
            picker.OnDateSelected += DatePicked;

            await Navigation.PushModalAsync (picker);
        }

        private void DatePicked (DateTime _start)
        {
            //TODO allow user to pick time
            m_Date = _start.Date;
            m_bolDatePicked = true;
            m_lblDate.Text = m_Date.ToString ("d");
        }

        public override void Save ()
        {
            m_Entry.DateTime = m_Date;
            m_Entry.EntryText = m_txtNote.Text;

            ATManager.GetInstance ().Persist (m_Entry);
        }

        public void SetupForm (JournalEntry _entry, Archer _archer)
        {
            m_Archer = _archer;

            if (_entry == null) {
                m_Date = DateTime.Now;

                m_Entry = new JournalEntry ();
                m_Entry.DateTime = m_Date;
                m_Entry.ParentId = _archer.Id;
            } else {
                m_Entry = _entry;
                m_Date = _entry.DateTime;
            }

            m_lblArcher.Text = _archer.FullName;
            m_lblDate.Text = _entry.DateTime.ToDisplayDate ();
            m_txtNote.Text = _entry.EntryText;
        }

    }
}

