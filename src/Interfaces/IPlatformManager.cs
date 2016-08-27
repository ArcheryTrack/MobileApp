using System;
using ATMobile.Network;

namespace ATMobile.Interfaces
{
    public interface IPlatformManager
    {
        bool HasNetworkConnectivity { get; }

        ATRestResponse SendRestRequest (ATRestRequest _request);
    }
}

