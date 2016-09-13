using System;
using ATMobile.Controls;
using ATMobile.Delegates;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class RangeCell : ViewCell
    {
        private StackLayout m_Layout;
        private ATLabel m_lblRangeName;
        private ATLabel m_lblRangeLocation;

        public static event RangeClickedDelegate RangeDeleteClicked;

        public RangeCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);

            m_lblRangeName = new ATLabel ();
            m_lblRangeName.SetBinding (Label.TextProperty, "Name");
            m_Layout.Children.Add (m_lblRangeName);

            m_lblRangeLocation = new ATLabel ();
            m_lblRangeLocation.SetBinding (Label.TextProperty, "Location");
            m_Layout.Children.Add (m_lblRangeLocation);

            View = m_Layout;

            var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true };
            deleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
            deleteAction.Clicked += DeleteClicked;
            ContextActions.Add (deleteAction);
        }

        void DeleteClicked (object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            Range range = (Range)menuItem.CommandParameter;

            var clicked = RangeDeleteClicked;
            if (clicked != null) {
                clicked (range);
            }
        }
    }
}

