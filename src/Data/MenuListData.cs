using System;
using System.Collections.Generic;
using ATMobile.Controls;
using ATMobile.Forms;

namespace ATMobile.Data
{
    public class MenuListData : List<MenuItem>
    {
        public MenuListData ()
        {
            this.Add (new MenuItem () {
                Title = "Practice",
                TargetType = typeof (PracticeHistoryForm)
            });

            this.Add (new MenuItem () {
                Title = "Tournaments",
                TargetType = typeof (TournamentHistoryForm)
            });

            this.Add (new MenuItem () {
                Title = "Estimate Distance",
                TargetType = typeof (SightEstimateForm)
            });

            this.Add (new MenuItem () {
                Title = "Sight Setup",
                TargetType = typeof (SightSetupForm)
            });

            this.Add (new MenuItem () {
                Title = "Settings",
                TargetType = typeof (SettingsForm)
            });

        }
    }
}

