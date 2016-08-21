using System;
using System.Collections.Generic;
using ATMobile.Objects;
using ATMobile.Forms;

namespace ATMobile.Data
{
    public static class MenuListData
    {
        public static List<MenuOption> GetMenu (IEnumerable<PluginMenuOption> _pluginMenuItems)
        {
            List<MenuOption> menuItems = new List<MenuOption> ();

            menuItems.Add (new MenuOption () {
                Title = "Home",
                TargetType = typeof (DefaultForm)
            });

            menuItems.Add (new MenuOption () {
                Title = "Practice",
                TargetType = typeof (PracticeHistoryForm)
            });

            menuItems.Add (new MenuOption () {
                Title = "Tournaments",
                TargetType = typeof (TournamentHistoryForm)
            });

            menuItems.Add (new MenuOption () {
                Title = "Journal",
                TargetType = typeof (JournalEntriesForm)
            });

            if (_pluginMenuItems != null) {
                foreach (var item in _pluginMenuItems) {
                    menuItems.Add (item);
                }
            }

            menuItems.Add (new MenuOption () {
                Title = "Settings",
                TargetType = typeof (SettingsForm)
            });

            return menuItems;
        }
    }
}

