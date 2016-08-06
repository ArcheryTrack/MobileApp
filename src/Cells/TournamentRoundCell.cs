using System;
using ATMobile.Delegates;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class TournamentRoundCell : AbstractBaseCell
    {
        public static TournamentRoundEditClickedDelegate RoundEditClicked;

        public TournamentRoundCell () : base ("RoundText")
        {
            var editAction = new MenuItem { Text = "Edit", IsDestructive = false };
            editAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
            editAction.Clicked += EditClicked;
            ContextActions.Add (editAction);
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
    }
}

