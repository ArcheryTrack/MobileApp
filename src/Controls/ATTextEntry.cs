using System;
using ATMobile.Constants;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class ATTextEntry : ContentView
    {
        private ATLabel m_lblTitle;
        private Entry m_txtEntry;
        private Grid m_EntryGrid;

        public ATTextEntry ()
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
                        Width = new GridLength (90, GridUnitType.Absolute)
                    },
                    new ColumnDefinition {
                        Width = new GridLength (1, GridUnitType.Star)
                    }
                }
            };

            m_lblTitle = new ATLabel {
                VerticalTextAlignment = TextAlignment.Center
            };
            m_EntryGrid.Children.Add (m_lblTitle, 0, 0);

            m_txtEntry = new Entry {
                Keyboard = Keyboard.Plain
            };

            m_EntryGrid.Children.Add (m_txtEntry, 1, 0);

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

        public string Text {
            get {
                return m_txtEntry.Text;
            }
            set {
                m_txtEntry.Text = value;
            }
        }

        public string Placeholder {
            get {
                return m_txtEntry.Placeholder;
            }
            set {
                m_txtEntry.Placeholder = value;
            }
        }

        public Keyboard Keyboard {
            get {
                return m_txtEntry.Keyboard;
            }
            set {
                m_txtEntry.Keyboard = value;
            }
        }

        public new bool IsEnabled {
            get {
                return base.IsEnabled;
            }

            set {
                base.IsEnabled = value;
                m_txtEntry.IsEnabled = value;

                if (base.IsEnabled) {
                    m_txtEntry.BackgroundColor = Color.FromHex (UIConstants.TextBackgroundEnabled);
                } else {
                    m_txtEntry.BackgroundColor = Color.FromHex (UIConstants.TextBackgroundDisabled);
                }
            }
        }
    }
}

