using System;
using System.Collections.Generic;
using ATMobile.Data;
using ATMobile.Interfaces;
using ATMobile.Objects;

namespace ATMobile.Managers
{
    public class MenuManager : IDisposable
    {
        private ATManager m_Manager;

        public MenuManager (ATManager _manager)
        {
            m_Manager = _manager;
        }

        public void Dispose ()
        {
            m_Manager = null;
        }

        public List<MenuOption> GetMainMenu ()
        {
            IEnumerable<IPlugin> plugins = m_Manager.PluginManager.Plugins;

            List<PluginMenuOption> pluginMenuOptions = new List<PluginMenuOption> ();

            foreach (var plugin in plugins) {
                var pluginMenuItems = plugin.GetMainMenuItems ();

                if (pluginMenuItems != null) {
                    foreach (var item in pluginMenuItems) {
                        pluginMenuOptions.Add (item);
                    }
                }
            }

            return MenuListData.GetMenu (pluginMenuOptions);
        }

        public List<MenuOption> GetSettingsMenu ()
        {
            IEnumerable<IPlugin> plugins = m_Manager.PluginManager.Plugins;

            List<PluginMenuOption> pluginMenuOptions = new List<PluginMenuOption> ();

            foreach (var plugin in plugins) {
                var pluginMenuItems = plugin.GetSettingsMenuItems ();

                if (pluginMenuItems != null) {
                    foreach (var item in pluginMenuItems) {
                        pluginMenuOptions.Add (item);
                    }
                }
            }

            return SettingsListData.GetMenu (pluginMenuOptions);
        }
    }
}

