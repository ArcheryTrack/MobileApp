using System;
using ATMobile.Controls;
using ATMobile.Delegates;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class PracticeHistoryCell : ViewCell
    {
        private StackLayout m_Layout;
        private ATLabel m_lblDateTime;
        private ATLabel m_lblLocation;
        private ATLabel m_lblArrows;

        public static PracticeClickedDelegate PracticeEditClicked;
        public static PracticeClickedDelegate PracticeDeleteClicked;

        public PracticeHistoryCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);

            StackLayout topRowLayout = new StackLayout { Orientation = StackOrientation.Horizontal };

            m_lblDateTime = new ATLabel ();
            m_lblDateTime.SetBinding (Label.TextProperty, "DateTimeString");
            topRowLayout.Children.Add (m_lblDateTime);

            topRowLayout.Children.Add (new Label { Text = "  Total Arrows: " });

            m_lblArrows = new ATLabel ();
            m_lblArrows.SetBinding (Label.TextProperty, "TotalArrowsShot");
            topRowLayout.Children.Add (m_lblArrows);

            m_Layout.Children.Add (topRowLayout);

            m_lblLocation = new ATLabel ();
            m_lblLocation.SetBinding (Label.TextProperty, "RangeName");
            m_Layout.Children.Add (m_lblLocation);

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

        void EditClicked (object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            Practice practice = (Practice)menuItem.CommandParameter;

            var clicked = PracticeEditClicked;
            if (clicked != null) {
                clicked (practice);
            }
        }

        void DeleteClicked (object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            Practice practice = (Practice)menuItem.CommandParameter;

            var clicked = PracticeDeleteClicked;
            if (clicked != null) {
                clicked (practice);
            }
        }
    }
}

