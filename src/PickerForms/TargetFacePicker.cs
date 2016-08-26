using System;
using ATMobile.Cells;
using ATMobile.Data;
using ATMobile.Managers;
using ATMobile.Objects;

namespace ATMobile.PickerForms
{
    public class TargetFacePicker : AbstractGenericPicker<TargetFace, TargetFacePickerCell>
    {
        public TargetFacePicker () : base ("Pick the Target Face", "Target Face")
        {
            List.ItemsSource = ATManager.GetInstance ().GetTargetFaces ();
        }

        public override void AddPressed ()
        {
            throw new NotImplementedException ();
        }
    }
}

