using System;
using System.Collections.Generic;
using ATMobile.Interfaces;

namespace ATMobile.Managers
{
    public class PluginManager : IDisposable
    {
        private ATManager m_Manager;

        public PluginManager (ATManager _manager)
        {
            Plugins = new List<IPlugin> ();
            m_Manager = _manager;
        }

        public IEnumerable<IPlugin> Plugins { get; private set; }

        public void AddPlugin (IPlugin _plugin)
        {
            var plugins = Plugins as List<IPlugin>;

            _plugin.InitializePlugin (m_Manager);

            plugins.Add (_plugin);
        }

        public void AppendPlugins (List<IPlugin> plugins)
        {
            foreach (var item in plugins) {
                AddPlugin (item);
            }
        }

        public void Dispose ()
        {
            m_Manager = null;
        }

        public IPlugin FindPlugin (string type)
        {
            foreach (var plugin in Plugins) {
                if (plugin.GetType ().Name == type) {
                    return plugin;
                }
            }

            return null;
        }
    }

}

