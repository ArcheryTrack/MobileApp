using System;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class PracticeHistoryCell : ViewCell
    {
        private StackLayout m_Layout;
        private Label m_lblDateTime;
        private Label m_lblLocation;
        private Label m_lblArrows;

        public PracticeHistoryCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);

            StackLayout topRowLayout = new StackLayout { Orientation = StackOrientation.Horizontal };

            m_lblDateTime = new Label ();
            m_lblDateTime.SetBinding (Label.TextProperty, "DateTimeString");
            topRowLayout.Children.Add (m_lblDateTime);

            topRowLayout.Children.Add (new Label { Text = "  Total Arrows: " });

            m_lblArrows = new Label ();
            m_lblArrows.SetBinding (Label.TextProperty, "TotalArrowsShot");
            topRowLayout.Children.Add (m_lblArrows);

            m_Layout.Children.Add (topRowLayout);

            m_lblLocation = new Label ();
            m_lblLocation.SetBinding (Label.TextProperty, "RangeName");
            m_Layout.Children.Add (m_lblLocation);

            View = m_Layout;
        }
    }
}

