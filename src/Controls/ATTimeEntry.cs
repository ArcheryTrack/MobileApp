using System;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class ATTimeEntry : ContentView
    {
        private ATLabel m_lblTitle;
        private TimePicker m_timePicker;
        private Grid m_EntryGrid;

        public ATTimeEntry ()
        {
            //Setup grid to hold the controls
            m_EntryGrid = new Grid {
                Padding = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition {
                        Height = new GridLength (40, GridUnitType.Absolute) //Name
                    }
                },
                ColumnDefinitions = {
                    new ColumnDefinition {
                        Width = new GridLength(1, GridUnitType.Star)
                    },
                    new ColumnDefinition {
                        Width = new GridLength(90, GridUnitType.Absolute)
                    }
                }
            };

            m_lblTitle = new ATLabel {
                VerticalTextAlignment = TextAlignment.Center
            };
            m_EntryGrid.Children.Add (m_lblTitle, 0, 0);

            m_timePicker = new TimePicker {

            };

            m_EntryGrid.Children.Add (m_timePicker, 1, 0);

            Content = m_EntryGrid;
        }

        public string Title {
            get {
                return m_lblTitle.Text;
            }
            set {
                m_lblTitle.Text = value;
            }
        }

        public TimeSpan Time {
            get {
                return m_timePicker.Time;
            }

            set {
                m_timePicker.Time = value;
            }
        }
    }
}

