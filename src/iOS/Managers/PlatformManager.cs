using System;
using ATMobile.iOS.Helpers;
using ATMobile.Interfaces;
using ATMobile.Network;
using RestSharp;

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

        public ATRestResponse SendRestRequest (ATRestRequest _request)
        {
            //TODO save client in dictionary based on host
            RestClient client = new RestClient (_request.Host);

            RestRequest request = new RestRequest (_request.Path, GetMethod (_request.Method));

            //Process Headers
            foreach (var key in _request.Headers.Keys) {
                request.AddHeader (key, _request.Headers [key]);
            }

            //Add Body
            if (_request.Body != null) {
                request.AddBody (_request.Body);
            }

            //Execute Rest Method
            var response = client.Execute (request);

            //Create Response
            ATRestResponse atResponse = new ATRestResponse ();

            //Process Headers, set status and body
            foreach (var parameter in response.Headers) {
                atResponse.Headers.Add (parameter.Name, parameter.Value.ToString ());
            }
            atResponse.Status = GetStatus (response.ResponseStatus);
            atResponse.Body = response.Content;

            return atResponse;
        }

        private ATResponseStatus GetStatus (ResponseStatus status)
        {
            switch (status) {
            case ResponseStatus.None:
                return ATResponseStatus.None;
            case ResponseStatus.Aborted:
                return ATResponseStatus.Aborted;
            case ResponseStatus.Completed:
                return ATResponseStatus.Completed;
            case ResponseStatus.Error:
                return ATResponseStatus.Error;
            case ResponseStatus.TimedOut:
                return ATResponseStatus.Error;
            }

            throw new ArgumentException ("Unexpected rest status");
        }

        private Method GetMethod (ATRestMethods method)
        {
            switch (method) {
            case ATRestMethods.Post:
                return Method.POST;
            case ATRestMethods.Get:
                return Method.GET;
            case ATRestMethods.Delete:
                return Method.DELETE;
            }

            throw new ArgumentException ("Unexpected rest method");
        }
    }
}