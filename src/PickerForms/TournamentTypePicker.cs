using System;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;

namespace ATMobile.PickerForms
{
    public class TournamentTypePicker : GenericPicker<TournamentType, TournamentTypePickerCell>
    {
        public TournamentTypePicker () : base ("Pick the Type of Tournament")
        {
            List.ItemsSource = ATManager.GetInstance ().GetTournamentTypes ();
        }
    }
}

