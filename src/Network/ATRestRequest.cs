using System;
using System.Collections.Generic;

namespace ATMobile.Network
{
    public class ATRestRequest
    {
        public ATRestRequest ()
        {
            Headers = new Dictionary<string, string> ();
        }

        public string Host { get; set; }
        public string Path { get; set; }
        public ATRestMethods Method { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }
    }
}

