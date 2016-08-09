using System;
using System.Collections.Generic;
using ATMobile.Objects;
using ATMobile.Forms;

namespace ATMobile.Data
{
    public class MenuListData : List<MenuOption>
    {
        public MenuListData ()
        {
            this.Add (new MenuOption () {
                Title = "Home",
                TargetType = typeof (DefaultForm)
            });

            this.Add (new MenuOption () {
                Title = "Practice",
                TargetType = typeof (PracticeHistoryForm)
            });

            this.Add (new MenuOption () {
                Title = "Tournaments",
                TargetType = typeof (TournamentHistoryForm)
            });

            this.Add (new MenuOption () {
                Title = "Journal",
                TargetType = typeof (JournalEntriesForm)
            });

            /*
            this.Add (new MenuItem () {
                Title = "Estimate Distance",
                TargetType = typeof (SightEstimateForm)
            });
            */

            this.Add (new MenuOption () {
                Title = "Sight Setup",
                TargetType = typeof (SightSetupForm)
            });

            this.Add (new MenuOption () {
                Title = "Settings",
                TargetType = typeof (SettingsForm)
            });

        }
    }
}

