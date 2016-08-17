using System;
namespace ATMobile.Messages
{
    public class LogMessage
    {
        public LogMessage ()
        {
            DateTime = DateTime.UtcNow;
        }

        public DateTime DateTime { get; set; }

        public string LogLevel { get; set; }

        public string Message { get; set; }
    }
}

