using System;
using System.Collections.Generic;
using ATMobile.Data;
using ATMobile.Interfaces;

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

        public MenuListData GetMainMenu ()
        {
            var menu = new MenuListData ();

            IEnumerable<IPlugin> plugins = m_Manager.PluginManager.Plugins;

            foreach (var plugin in plugins) {
                var pluginMenuItems = plugin.GetMainMenuItems ();

                if (pluginMenuItems != null) {
                    foreach (var item in pluginMenuItems) {
                        menu.Add (item);
                    }
                }
            }

            return menu;
        }

        public SettingsListData GetSettingsMenu ()
        {
            var menu = new SettingsListData ();

            IEnumerable<IPlugin> plugins = m_Manager.PluginManager.Plugins;

            foreach (var plugin in plugins) {
                var pluginMenuItems = plugin.GetSettingsMenuItems ();

                if (pluginMenuItems != null) {
                    foreach (var item in pluginMenuItems) {
                        menu.Add (item);
                    }
                }
            }

            return menu;
        }
    }
}

