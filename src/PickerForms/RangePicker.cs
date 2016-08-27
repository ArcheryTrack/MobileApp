using System;
using ATMobile.Cells;
using ATMobile.Forms;
using ATMobile.Managers;
using ATMobile.Objects;

namespace ATMobile.PickerForms
{
    public class RangePicker : AbstractGenericPicker<Range, TournamentTypePickerCell>
    {
        public RangePicker () : base ("Pick the Range", "Range")
        {
        }

        public override void AddPressed ()
        {
            RangeForm addRange = new RangeForm ();
            Navigation.PushModalAsync (addRange);
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            List.ItemsSource = ATManager.GetInstance ().GetRanges ();
        }
    }
}

