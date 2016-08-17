using System;
using System.Collections.Generic;
using ATMobile.Interfaces;

namespace ATMobile.Managers
{
    public class PluginManager
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
            plugins.Add (_plugin);
        }

    }

}

