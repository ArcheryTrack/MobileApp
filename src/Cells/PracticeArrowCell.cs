using System;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class PracticeArrowCell : ViewCell
    {
        private StackLayout m_Layout;
        private Label m_lblScore;

        public PracticeArrowCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);

            m_lblScore = new Label ();
            m_lblScore.SetBinding (Label.TextProperty, "Score");
            m_Layout.Children.Add (m_lblScore);

            View = m_Layout;
        }
    }
}

