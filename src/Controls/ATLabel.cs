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
        }
    }
}

