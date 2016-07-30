using System;
using ATMobile.Cells;
using ATMobile.Data;
using ATMobile.Objects;

namespace ATMobile.PickerForms
{
    public class DistanceUnitsPicker : GenericPicker<DistanceUnit, DistanceUnitCell>
    {
        public DistanceUnitsPicker () : base ("Pick Distance Units")
        {
            List.ItemsSource = DistanceUnitData.GetDistanceUnits ();
        }
    }
}

