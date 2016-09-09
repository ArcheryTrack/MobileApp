using System;
using ATMobile.Controls;
using ATMobile.Delegates;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class TournamentHistoryCell : ViewCell
    {
        private StackLayout m_Layout;
        private ATLabel m_lblName;
        private ATLabel m_lblDateTime;

        public static TournamentClickedDelegate TournamentEditClicked;
        public static TournamentClickedDelegate TournamentDeleteClicked;

        public TournamentHistoryCell ()
        {
            m_Layout = new StackLayout {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness (0, 5)
            };

            m_lblName = new ATLabel ();
            m_lblName.SetBinding (Label.TextProperty, "NameString");
            m_Layout.Children.Add (m_lblName);

            m_lblDateTime = new ATLabel ();
            m_lblDateTime.SetBinding (Label.TextProperty, "DateTimeString");
            m_Layout.Children.Add (m_lblDateTime);

            View = m_Layout;

            var editAction = new MenuItem { Text = "Edit", IsDestructive = false };
            editAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
            editAction.Clicked += EditClicked;
            ContextActions.Add (editAction);

            var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true };
            deleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
            deleteAction.Clicked += DeleteClicked;
            ContextActions.Add (deleteAction);
        }

        private void DeleteClicked (object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            Tournament tournament = (Tournament)menuItem.CommandParameter;

            var clicked = TournamentDeleteClicked;
            if (clicked != null) {
                clicked (tournament);
            }
        }

        private void EditClicked (object sender, EventArgs e)
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

