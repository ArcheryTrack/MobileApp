using System;
using ATMobile.Helpers;
using ATMobile.PickerForms;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class ATDatePicker : ContentView, IDisposable
    {
        private string m_strLongDescription;
        private string m_strShortDescription;
        private DateTime? m_datSelected;
        private ATLabel m_lblDateText;
        private Grid m_gridLayout;
        private Button m_btnPickDate;
        private bool m_ShowDecade;

        public ATDatePicker (string _shortDescription, string _longDescription, bool _showDecade = false)
        {
            m_strShortDescription = _shortDescription;
            m_strLongDescription = _longDescription;
            m_ShowDecade = _showDecade;

            //Setup grid to hold the controls
            m_gridLayout = new Grid {
                Padding = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition {
                        Height = new GridLength(40, GridUnitType.Absolute)
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

            m_lblDateText = new ATLabel {
                Text = m_strLongDescription,
                HeightRequest = 40,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            m_gridLayout.Children.Add (m_lblDateText, 0, 0);

            m_btnPickDate = new Button {
                Text = "Pick",
                WidthRequest = 80,
                HeightRequest = 40,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End
            };
            m_btnPickDate.Clicked += PickDate;
            m_gridLayout.Children.Add (m_btnPickDate, 1, 0);

            Content = m_gridLayout;
        }

        async private void PickDate (object sender, EventArgs e)
        {
            DatePickerForm picker = new DatePickerForm (m_strLongDescription, m_ShowDecade);
            picker.SelectedDate = m_datSelected;

            picker.OnDateSelected += DatePicked;
            await Navigation.PushModalAsync (picker);
        }

        private void DatePicked (DateTime _date)
        {
            SelectedDate = _date;
        }

        public DateTime? SelectedDate {
            get {
                return m_datSelected;
            }
            set {
                m_datSelected = value;

                if (m_datSelected == null) {
                    m_lblDateText.Text = m_strLongDescription;
                } else {
                    m_lblDateText.Text = string.Format (
                        "{0} : {1}",
                        m_strShortDescription,
                        m_datSelected.Value.ToDisplayDate ());
                }
            }
        }

        public void Dispose ()
        {
            m_gridLayout = null;
        }
    }
}

