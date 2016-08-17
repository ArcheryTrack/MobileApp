using System;
using System.Collections.Generic;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Interfaces
{
    public interface IPlugin
    {
        List<PluginMenuOption> GetMainMenuItems ();

        List<PluginMenuOption> GetSettingsMenuItems ();

        Page GetPage (PluginMenuOption _menuOption);
    }
}

