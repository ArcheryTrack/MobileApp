using System;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class RangeCell : ViewCell
    {
        private StackLayout m_Layout;
        private Label m_lblRangeName;
        private Label m_lblRangeLocation;

        public RangeCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);

            m_lblRangeName = new Label ();
            m_lblRangeName.SetBinding (Label.TextProperty, "Name");
            m_Layout.Children.Add (m_lblRangeName);

            m_lblRangeLocation = new Label ();
            m_lblRangeLocation.SetBinding (Label.TextProperty, "Location");
            m_Layout.Children.Add (m_lblRangeLocation);

            View = m_Layout;
        }
    }
}

