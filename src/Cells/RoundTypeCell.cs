using System;
using ATMobile.Controls;
using ATMobile.Delegates;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class RoundTypeCell : ViewCell
    {
        private StackLayout m_Layout;
        private ATLabel m_lblName;
        private ATLabel m_lblDetails;

        public static RoundTypeClickedDelegate RoundTypeDeleteClicked;

        public RoundTypeCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);

            m_lblName = new ATLabel ();
            m_lblName.SetBinding (Label.TextProperty, "Name");
            m_Layout.Children.Add (m_lblName);

            m_lblDetails = new ATLabel ();
            m_lblDetails.SetBinding (Label.TextProperty, "Details");
            m_Layout.Children.Add (m_lblDetails);

            View = m_Layout;

            var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true };
            deleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
            deleteAction.Clicked += DeleteClicked;
            ContextActions.Add (deleteAction);
        }

        void DeleteClicked (object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            RoundType round = (RoundType)menuItem.CommandParameter;

            var clicked = RoundTypeDeleteClicked;
            if (clicked != null) {
                clicked (round);
            }
        }
    }
}

