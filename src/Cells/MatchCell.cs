using System;
using ATMobile.Delegates;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class MatchCell : AbstractBaseCell
    {
        public static MatchClickedDelegate MatchDeleteClicked;

        public MatchCell () : base ("DisplayText")
        {
            var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true };
            deleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
            deleteAction.Clicked += DeleteClicked;
            ContextActions.Add (deleteAction);
        }

        private void DeleteClicked (object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            Match match = (Match)menuItem.CommandParameter;

            var clicked = MatchDeleteClicked;
            if (clicked != null) {
                clicked (match);
            }
        }
    }
}

