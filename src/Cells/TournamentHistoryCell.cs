using System;
using ATMobile.Delegates;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class TournamentHistoryCell : ViewCell
    {
        private StackLayout m_Layout;
        private Label m_lblName;
        private Label m_lblDateTime;

        public static TournamentEditClickedDelegate TournamentEditClicked;

        public TournamentHistoryCell ()
        {
            m_Layout = new StackLayout {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness (0, 5)
            };

            m_lblName = new Label ();
            m_lblName.SetBinding (Label.TextProperty, "NameString");
            m_Layout.Children.Add (m_lblName);

            m_lblDateTime = new Label ();
            m_lblDateTime.SetBinding (Label.TextProperty, "DateTimeString");
            m_Layout.Children.Add (m_lblDateTime);

            View = m_Layout;

            var editAction = new MenuItem { Text = "Edit", IsDestructive = false };
            editAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
            editAction.Clicked += EditClicked;
            ContextActions.Add (editAction);
        }

        void EditClicked (object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            Tournament tournament = (Tournament)menuItem.CommandParameter;

            var clicked = TournamentEditClicked;
            if (clicked != null) {
                clicked (tournament);
            }
        }
    }
}

