using System;
using ATMobile.Forms;
using Xamarin.Forms;

namespace ATMobile.Factories
{
    public static class FormFactory
    {
        public static ContentPage GetForm (string targetType)
        {
            ContentPage page = null;

            switch (targetType) {
            case "Archers":
                return new ArchersForm ();
            case "Ranges":
                return new RangesForm ();
            case "Tournament Type":
                return new TournamentTypesForm ();
            }

            return page;
        }
    }
}

