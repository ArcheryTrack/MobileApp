using System;
using System.Collections.Generic;
using ATMobile.Objects;
using ATMobile.Forms;

namespace ATMobile.Data
{
    public class SettingsListData : List<MenuOption>
    {
        public SettingsListData ()
        {
            this.Add (new MenuOption () {
                Title = "Archers",
                TargetType = typeof (ArchersForm)
            });

            this.Add (new MenuOption () {
                Title = "My Ranges",
                TargetType = typeof (RangesForm)
            });

            this.Add (new MenuOption () {
                Title = "Tournament Types",
                TargetType = typeof (TournamentTypesForm)
            });

            this.Add (new MenuOption () {
                Title = "About",
                TargetType = typeof (AboutForm)
            });

            /*
            this.Add (new MenuItem () {
                Title = "Sync with Server",
                TargetType = typeof (LoginForm)
            });
            */
        }
    }
}

