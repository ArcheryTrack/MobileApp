using System;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class PracticeHistoryCell : ViewCell
    {
        private StackLayout m_Layout;
        private Label m_lblDateTime;
        private Label m_lblLocation;

        public PracticeHistoryCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);

            m_lblDateTime = new Label ();
            m_lblDateTime.SetBinding (Label.TextProperty, "DateTimeString");
            m_Layout.Children.Add (m_lblDateTime);

            m_lblLocation = new Label ();
            m_lblLocation.SetBinding (Label.TextProperty, "RangeName");
            m_Layout.Children.Add (m_lblLocation);

            View = m_Layout;
        }
    }
}

