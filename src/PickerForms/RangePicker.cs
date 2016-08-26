using System;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;

namespace ATMobile.PickerForms
{
    public class RangePicker : AbstractGenericPicker<Range, TournamentTypePickerCell>
    {
        public RangePicker () : base ("Pick the Range", "Range")
        {
            List.ItemsSource = ATManager.GetInstance ().GetRanges ();
        }

        public override void AddPressed ()
        {
            throw new NotImplementedException ();
        }
    }
}

