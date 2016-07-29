using System;
using ATMobile.Cells;
using ATMobile.Data;
using ATMobile.Objects;

namespace ATMobile.PickerForms
{
    public class TargetFacePicker : GenericPicker<TargetFace, TargetFacePickerCell>
    {
        public TargetFacePicker () : base ("Pick the Target Face", "FullName")
        {
            List.ItemsSource = TargetFaceData.GetData ();
        }
    }
}

