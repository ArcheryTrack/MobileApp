using System;
using System.Collections.Generic;
using ATMobile.Controls;
using ATMobile.Forms;

namespace ATMobile.Data
{
    public class SettingsListData : List<MenuItem>
    {
        public SettingsListData ()
        {
            this.Add (new MenuItem () {
                Title = "Archers",
                TargetType = typeof (ArchersForm)
            });

            this.Add (new MenuItem () {
                Title = "Ranges",
                TargetType = typeof (RangesForm)
            });

            this.Add (new MenuItem () {
                Title = "Tournament Types",
                TargetType = typeof (TournamentTypesForm)
            });

            this.Add (new MenuItem () {
                Title = "Sync with Server",
                TargetType = typeof (LoginForm)
            });
        }
    }
}

