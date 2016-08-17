using System;
namespace ATMobile.Messages
{
    public class BusMessage
    {
        public Guid InstallId { get; set; }

        public string Username { get; set; }

        public string DataType { get; set; }

        public string JsonBody { get; set; }
    }
}

