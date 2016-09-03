using System;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class ATToggleEntry : ContentView
    {
        private ATLabel m_lblTitle;
        private Switch m_swChoice;
        private Grid m_EntryGrid;

        public ATToggleEntry ()
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
                        Width = new GridLength(80, GridUnitType.Absolute)
                    }
                }
            };

            m_lblTitle = new ATLabel {
                VerticalTextAlignment = TextAlignment.Center
            };
            m_EntryGrid.Children.Add (m_lblTitle, 0, 0);

            m_swChoice = new Switch {

            };
            m_EntryGrid.Children.Add (m_swChoice, 1, 0);

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

        public bool IsToggled {
            get {
                return m_swChoice.IsToggled;
            }

            set {
                m_swChoice.IsToggled = value;
            }
        }
    }
}

