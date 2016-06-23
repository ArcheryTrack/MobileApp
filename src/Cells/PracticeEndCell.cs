using System;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class PracticeEndCell : ViewCell
    {
        private StackLayout m_Layout;
        private Label m_lblEndNumber;
        private Label m_lblScore;

        public PracticeEndCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);

            m_lblEndNumber = new Label ();
            m_lblEndNumber.SetBinding (Label.TextProperty, "EndNumber");
            m_Layout.Children.Add (m_lblEndNumber);

            m_lblScore = new Label ();
            m_lblScore.SetBinding (Label.TextProperty, "ResultsString");
            m_Layout.Children.Add (m_lblScore);

            View = m_Layout;
        }
    }
}

