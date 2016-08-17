using System;
namespace ATMobile.Messages
{
    public class ActionMessage
    {
        public ActionMessage ()
        {
            DateTime = DateTime.UtcNow;
        }

        public string Form { get; set; }

        public string Action { get; set; }

        public DateTime DateTime { get; set; }
    }
}

