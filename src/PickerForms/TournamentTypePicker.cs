using System;
using ATMobile.Cells;
using ATMobile.Forms;
using ATMobile.Managers;
using ATMobile.Objects;

namespace ATMobile.PickerForms
{
    public class TournamentTypePicker : AbstractGenericPicker<TournamentType, TournamentTypePickerCell>
    {
        public TournamentTypePicker () : base ("Pick the Type of Tournament", "Tournament Type")
        {
        }

        public override void AddPressed ()
        {
            TournamentTypeForm addTournamentType = new TournamentTypeForm ();
            addTournamentType.SetupForm (null);
            Navigation.PushModalAsync (addTournamentType);
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            List.ItemsSource = ATManager.GetInstance ().GetTournamentTypes ();
        }
    }
}

