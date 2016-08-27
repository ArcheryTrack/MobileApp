using System;
using ATMobile.iOS.Helpers;
using ATMobile.Interfaces;

namespace ATMobile.iOS.Managers
{
    public class PlatformManager : IPlatformManager
    {
        public PlatformManager ()
        {
        }

        public bool HasNetworkConnectivity {
            get {
                NetworkStatus internetStatus = Reachability.InternetConnectionStatus ();

                if (internetStatus == NetworkStatus.NotReachable) {
                    return false;
                }

                return true;
            }
        }
    }
}