using System;
using ATMobile.Delegates;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class TextButton : Button
    {
        private string m_Value;

        public event OnTextButtonClickedDelegate OnClicked;

        public TextButton (string _value)
        {
            m_Value = _value;
            Text = _value;
            this.Clicked += OnButtonClicked;

            BorderWidth = 1;
            BorderRadius = 5;
        }

        private void OnButtonClicked (object sender, EventArgs e)
        {
            var clicked = OnClicked;

            if (clicked != null) {
                clicked (m_Value);
            }
        }
    }
}

