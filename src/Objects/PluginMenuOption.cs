using System;
using ATMobile.Interfaces;

namespace ATMobile.Objects
{
    public class PluginMenuOption : MenuOption
    {
        public PluginMenuOption (IPlugin _plugin)
        {
            Plugin = _plugin;
        }

        public IPlugin Plugin { get; private set; }
    }
}

