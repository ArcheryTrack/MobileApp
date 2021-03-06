﻿using System;
using ATMobile.Cells;
using ATMobile.Data;
using ATMobile.Objects;

namespace ATMobile.PickerForms
{
    public class DistanceUnitsPicker : AbstractGenericPicker<DistanceUnit, DistanceUnitCell>
    {
        public DistanceUnitsPicker () : base ("Pick Distance Units", "Unit", false)
        {
            List.ItemsSource = DistanceUnitData.GetDistanceUnits ();
        }

        public override void AddPressed ()
        {

        }
    }
}

