using System;
using ATMobile.Constants;
using ATMobile.Controls;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class MenuCell : ViewCell
    {
        private StackLayout m_Layout;
        private ATLabel m_lblCellText;

        public MenuCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);
            m_Layout.Margin = new Thickness (20, 8, 20, 0);

            m_lblCellText = new ATLabel ();
            m_lblCellText.SetBinding (Label.TextProperty, "Title");
            m_lblCellText.TextColor = Color.FromHex (UIConstants.MenuListTextColor);
            m_lblCellText.VerticalTextAlignment = TextAlignment.Center;
            m_Layout.Children.Add (m_lblCellText);

            View = m_Layout;
        }
    }
}

