using System;
using ATMobile.Delegates;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class PracticeArrowCell : ViewCell
    {
        public static DeletePracticeArrowClickedDelegate DeletePracticeArrowClicked;

        private StackLayout m_Layout;
        private Label m_lblScore;

        public PracticeArrowCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);

            m_lblScore = new Label ();
            m_lblScore.SetBinding (Label.TextProperty, "Score");
            m_Layout.Children.Add (m_lblScore);


            var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
            deleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
            deleteAction.Clicked += (sender, e) => {
                var deleteClicked = DeletePracticeArrowClicked;

                if (deleteClicked != null) {
                    ShotArrow arrow = (ShotArrow)((MenuItem)sender).CommandParameter;
                    deleteClicked (arrow.ArrowNumber);
                }
            };
            ContextActions.Add (deleteAction);

            View = m_Layout;
        }
    }
}

