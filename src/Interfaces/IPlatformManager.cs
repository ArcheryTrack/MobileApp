using System;
namespace ATMobile.Interfaces
{
    public interface IPlatformManager
    {
        bool HasNetworkConnectivity { get; }
    }
}

