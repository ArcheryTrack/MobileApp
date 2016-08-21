using System;
using System.Collections.Generic;
using ATMobile.Objects;
using ATMobile.Forms;

namespace ATMobile.Data
{
    public static class SettingsListData
    {
        public static List<MenuOption> GetMenu (IEnumerable<PluginMenuOption> _pluginMenuItems)
        {
            List<MenuOption> menuItems = new List<MenuOption> ();

            menuItems.Add (new MenuOption () {
                Title = "Archers",
                TargetType = typeof (ArchersForm)
            });

            menuItems.Add (new MenuOption () {
                Title = "My Ranges",
                TargetType = typeof (RangesForm)
            });

            menuItems.Add (new MenuOption () {
                Title = "Tournament Types",
                TargetType = typeof (TournamentTypesForm)
            });

            if (_pluginMenuItems != null) {
                foreach (var item in _pluginMenuItems) {
                    menuItems.Add (item);
                }
            }

            menuItems.Add (new MenuOption () {
                Title = "About",
                TargetType = typeof (AboutForm)
            });

            return menuItems;
        }
    }
}

