using System;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class PracticeHistoryCell : ViewCell
    {
        private StackLayout m_Layout;
        private Label m_lblDateTime;

        public PracticeHistoryCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);

            m_lblDateTime = new Label ();
            m_lblDateTime.SetBinding (Label.TextProperty, "DateTimeString");
            m_Layout.Children.Add (m_lblDateTime);

            View = m_Layout;
        }
    }
}

