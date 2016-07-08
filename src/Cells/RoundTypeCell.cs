using System;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class RoundTypeCell : ViewCell
    {
        private StackLayout m_Layout;
        private Label m_lblName;
        private Label m_lblDetails;

        public RoundTypeCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);

            m_lblName = new Label ();
            m_lblName.SetBinding (Label.TextProperty, "Name");
            m_Layout.Children.Add (m_lblName);

            m_lblDetails = new Label ();
            m_lblDetails.SetBinding (Label.TextProperty, "Details");
            m_Layout.Children.Add (m_lblDetails);

            View = m_Layout;
        }
    }
}

