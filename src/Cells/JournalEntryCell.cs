using System;
using ATMobile.Delegates;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class JournalEntryCell : AbstractBaseCell
    {
        public static event JournalEntryClickedDelegate JournalEntryDeleteClicked;

        public JournalEntryCell () : base ("DisplayText")
        {
            var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true };
            deleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
            deleteAction.Clicked += DeleteClicked;
            ContextActions.Add (deleteAction);
        }

        void DeleteClicked (object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            JournalEntry journalEntry = (JournalEntry)menuItem.CommandParameter;

            var clicked = JournalEntryDeleteClicked;
            if (clicked != null) {
                clicked (journalEntry);
            }
        }
    }
}

