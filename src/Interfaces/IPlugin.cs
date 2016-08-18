using System;
using System.Collections.Generic;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Interfaces
{
    public interface IPlugin
    {
        void InitializePlugin (ATManager _manager);

        List<PluginMenuOption> GetMainMenuItems ();

        List<PluginMenuOption> GetSettingsMenuItems ();

        Page GetPage (PluginMenuOption _menuOption);
    }
}

