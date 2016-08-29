using System;
using ATMobile.Controls;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class TournamentTypeCell : ViewCell
    {
        private StackLayout m_Layout;
        private ATLabel m_lblName;
        private ATLabel m_lblDetails;

        public TournamentTypeCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);

            m_lblName = new ATLabel ();
            m_lblName.SetBinding (Label.TextProperty, "Name");
            m_Layout.Children.Add (m_lblName);

            m_lblDetails = new ATLabel ();
            m_lblDetails.SetBinding (Label.TextProperty, "Details");
            m_Layout.Children.Add (m_lblDetails);

            View = m_Layout;
        }
    }
}

