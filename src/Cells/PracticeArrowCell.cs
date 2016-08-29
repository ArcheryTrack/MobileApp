using System;
using ATMobile.Controls;
using ATMobile.Delegates;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class PracticeArrowCell : ViewCell
    {
        public static DeletePracticeArrowClickedDelegate DeletePracticeArrowClicked;

        private StackLayout m_Layout;
        private ATLabel m_lblScore;

        public PracticeArrowCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);

            m_lblScore = new ATLabel ();
            m_lblScore.SetBinding (Label.TextProperty, "Score");
            m_Layout.Children.Add (m_lblScore);


            var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
            deleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
            deleteAction.Clicked += DeleteClicked;
            ContextActions.Add (deleteAction);

            View = m_Layout;
        }

        void DeleteClicked (object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var deleteClicked = DeletePracticeArrowClicked;

            if (deleteClicked != null) {
                ShotArrow arrow = (ShotArrow)menuItem.CommandParameter;
                deleteClicked (arrow.ArrowNumber);
            }
        }
    }
}

