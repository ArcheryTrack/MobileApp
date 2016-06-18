using System;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class ArcherCell : ViewCell
    {
        private StackLayout m_Layout;
        private Label m_lblArcherName;

        public ArcherCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);

            m_lblArcherName = new Label ();
            m_lblArcherName.SetBinding (Label.TextProperty, "FullName");
            m_Layout.Children.Add (m_lblArcherName);

            View = m_Layout;
        }
    }
}

