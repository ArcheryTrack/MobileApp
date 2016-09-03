using System;
using ATMobile.Constants;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class ATLabel : Label
    {
        public ATLabel ()
        {
            FontFamily = UIConstants.FontFamily;
            LineBreakMode = LineBreakMode.WordWrap;

            if (Device.Idiom == TargetIdiom.Phone) {
                FontSize = Device.GetNamedSize (NamedSize.Small, typeof (Label));
            } else {
                FontSize = Device.GetNamedSize (NamedSize.Medium, typeof (Label));
            }
        }
    }
}

