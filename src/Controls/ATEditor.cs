using System;
using ATMobile.Constants;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class ATEditor : ContentView
    {
        private StackLayout m_Layout;
        private Editor m_txtEditor;
        private Label m_lblTitle;

        public ATEditor (int _minimumHeightRequest = 80,
                         int? _maximumHeightRequest = null)
        {
            VerticalOptions = LayoutOptions.FillAndExpand;

            m_Layout = new StackLayout {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            m_lblTitle = new ATLabel {
                Text = "Note"
            };
            m_Layout.Children.Add (m_lblTitle);

            m_txtEditor = new Editor {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                MinimumHeightRequest = _minimumHeightRequest
            };
            m_Layout.Children.Add (m_txtEditor);

            if (_maximumHeightRequest != null) {
                m_txtEditor.HeightRequest = _maximumHeightRequest.Value;
            } else {
                m_txtEditor.VerticalOptions = LayoutOptions.FillAndExpand;
            }


            m_lblTitle.Margin = new Thickness (5, 0, 5, 0);
            m_txtEditor.Margin = new Thickness (5, 0, 5, 0);

            Content = m_Layout;
        }

        public string Text {
            get {
                return m_txtEditor.Text;
            }

            set {
                m_txtEditor.Text = value;
            }
        }

        public string Title {
            get {
                return m_lblTitle.Text;
            }

            set {
                m_lblTitle.Text = value;
            }
        }

        public new bool IsEnabled {
            get {
                return base.IsEnabled;
            }

            set {
                base.IsEnabled = value;
                m_txtEditor.IsEnabled = value;

                if (base.IsEnabled) {
                    m_txtEditor.BackgroundColor = Color.FromHex (UIConstants.TextBackgroundEnabled);
                } else {
                    m_txtEditor.BackgroundColor = Color.FromHex (UIConstants.TextBackgroundDisabled);
                }
            }
        }
    }
}

