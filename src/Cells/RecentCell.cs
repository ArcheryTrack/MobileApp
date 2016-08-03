using System;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class RecentCell : ViewCell
    {
        private StackLayout m_Layout;
        private Label m_lblType;
        private Label m_lblArchers;

        public RecentCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);

            m_lblType = new Label ();
            m_lblType.SetBinding (Label.TextProperty, "RecentTypeText");
            m_Layout.Children.Add (m_lblType);

            m_lblArchers = new Label ();
            m_lblArchers.SetBinding (Label.TextProperty, "ArcherText");
            m_Layout.Children.Add (m_lblArchers);

            View = m_Layout;
        }
    }
}

