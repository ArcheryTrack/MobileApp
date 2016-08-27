using System;
using System.Collections.Generic;

namespace ATMobile.Network
{
    public class ATRestResponse
    {
        public ATRestResponse ()
        {
            Headers = new Dictionary<string, string> ();
        }

        public ATResponseStatus Status { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }

    }
}

