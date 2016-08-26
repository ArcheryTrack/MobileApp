using System;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;

namespace ATMobile.PickerForms
{
    public class ArcherPicker : AbstractGenericPicker<Archer, ArcherCell>
    {
        public ArcherPicker () : base ("Pick Archer", "Archer")
        {
            List.ItemsSource = ATManager.GetInstance ().GetArchers ();
        }

        public override void AddPressed ()
        {
            throw new NotImplementedException ();
        }
    }
}

