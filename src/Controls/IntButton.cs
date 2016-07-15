using System;
using ATMobile.Delegates;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class IntButton : Button
    {
        private int m_Value;

        public event OnIntButtonClickedDelegate OnClicked;

        public IntButton (int _value)
        {
            SetValue (_value);
            this.Clicked += OnButtonClicked;
        }

        public void SetValue (int _value)
        {
            m_Value = _value;

            if (m_Value == 0) {
                Text = "";
            } else {
                Text = Convert.ToString (_value);
            }
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

