using System;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class TournamentEndCell : ViewCell
    {
        private StackLayout m_Layout;
        private Label m_lblEndNumber;
        private Label m_lblScore;
        private Label m_lblTotal;

        public TournamentEndCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Orientation = StackOrientation.Horizontal;
            m_Layout.Padding = new Thickness (0, 5);

            m_Layout.Children.Add (new Label () { Text = "End " });

            m_lblEndNumber = new Label ();
            m_lblEndNumber.SetBinding (Label.TextProperty, "EndNumber");
            m_Layout.Children.Add (m_lblEndNumber);

            m_Layout.Children.Add (new Label () { Text = ":" });

            m_lblScore = new Label ();
            m_lblScore.SetBinding (Label.TextProperty, "ResultsString");
            m_Layout.Children.Add (m_lblScore);

            m_Layout.Children.Add (new Label () { Text = " = " });

            m_lblTotal = new Label ();
            m_lblTotal.SetBinding (Label.TextProperty, "TotalScore");
            m_Layout.Children.Add (m_lblTotal);

            View = m_Layout;
        }
    }
}

