using System;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;

namespace ATMobile.PickerForms
{
    public class TournamentTypePicker : AbstractGenericPicker<TournamentType, TournamentTypePickerCell>
    {
        public TournamentTypePicker () : base ("Pick the Type of Tournament", "Tournament Type")
        {
            List.ItemsSource = ATManager.GetInstance ().GetTournamentTypes ();
        }

        public override void AddPressed ()
        {
            throw new NotImplementedException ();
        }
    }
}

