using System;
using ATMobile.Cells;
using ATMobile.Managers;
using ATMobile.Objects;

namespace ATMobile.PickerForms
{
    public class ArcherPicker : GenericPicker<Archer, ArcherCell>
    {
        public ArcherPicker () : base ("Pick Archer", "FullName")
        {
            List.ItemsSource = ATManager.GetInstance ().GetArchers ();
        }
    }
}

