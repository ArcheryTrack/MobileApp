using System;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class TournamentEndCell : ViewCell
    {
        private StackLayout m_Layout;
        private Label m_lblScore;

        public TournamentEndCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Orientation = StackOrientation.Horizontal;
            m_Layout.Padding = new Thickness (0, 5);

            m_lblScore = new Label () {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center
            };
            m_lblScore.SetBinding (Label.TextProperty, "CellOutput");
            m_Layout.Children.Add (m_lblScore);

            View = m_Layout;
        }
    }
}

