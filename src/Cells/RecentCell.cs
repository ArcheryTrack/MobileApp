using System;
using ATMobile.Controls;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class RecentCell : ViewCell
    {
        private StackLayout m_Layout;
        private ATLabel m_lblType;
        private ATLabel m_lblArchers;

        public RecentCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);

            m_lblType = new ATLabel ();
            m_lblType.SetBinding (Label.TextProperty, "RecentTypeText");
            m_Layout.Children.Add (m_lblType);

            m_lblArchers = new ATLabel ();
            m_lblArchers.SetBinding (Label.TextProperty, "ArcherText");
            m_Layout.Children.Add (m_lblArchers);

            View = m_Layout;
        }
    }
}

