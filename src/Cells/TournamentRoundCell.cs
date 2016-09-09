using System;
using ATMobile.Delegates;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class TournamentRoundCell : AbstractBaseCell
    {
        public static TournamentRoundClickedDelegate RoundEditClicked;
        public static TournamentRoundClickedDelegate RoundDeleteClicked;

        public TournamentRoundCell () : base ("RoundText")
        {
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
            Round round = (Round)menuItem.CommandParameter;

            var clicked = RoundEditClicked;
            if (clicked != null) {
                clicked (round);
            }
        }

        void DeleteClicked (object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            Round round = (Round)menuItem.CommandParameter;

            var clicked = RoundDeleteClicked;
            if (clicked != null) {
                clicked (round);
            }
        }
    }
}

