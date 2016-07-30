using System;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;

namespace ATMobile.PickerForms
{
    public class RangePicker : GenericPicker<Range, TournamentTypePickerCell>
    {
        public RangePicker () : base ("Pick the Type of Tournament")
        {
            List.ItemsSource = ATManager.GetInstance ().GetRanges ();
        }
    }
}

