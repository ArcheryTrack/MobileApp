using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class ATButtonBar : ContentView
    {
        private StackLayout m_Buttons;

        public ATButtonBar ()
        {
            m_Buttons = new StackLayout {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Padding = 0,
                Spacing = 0
            };

            if (Device.Idiom == TargetIdiom.Phone) {
                m_Buttons.Margin = new Thickness (0, 10, 0, 10);
            } else {
                m_Buttons.Margin = new Thickness (0, 10, 0, 20);
            }

            Content = m_Buttons;
        }

        public void Add (Button button)
        {
            m_Buttons.Children.Add (button);
        }

        public Button Add (string _text, LayoutOptions _horizontalOptions, int _width = 100)
        {
            Button button = new Button () {
                Text = _text,
                Margin = new Thickness (5, 0, 5, 0),
                HorizontalOptions = _horizontalOptions,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = _width
            };

            m_Buttons.Children.Add (button);

            return button;
        }
    }
}

