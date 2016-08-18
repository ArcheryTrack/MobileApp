using System;
using ATMobile.Delegates;
using ATMobile.Messages;
using ATMobile.Objects;
using Newtonsoft.Json;

namespace ATMobile.Managers
{
    public class MessagingManager : IDisposable
    {
        private ATManager m_Manager;

        public event MessageReceivedDelegate MessageReceived;

        public MessagingManager (ATManager _manager)
        {
            m_Manager = _manager;
        }

        public void Publish (ActionMessage _actionMessage)
        {
            BusMessage message = new BusMessage {
                InstallId = m_Manager.SettingManager.GetInstallId (),
                Username = m_Manager.SettingManager.GetUsername (),
                DataType = _actionMessage.GetType ().FullName,
                JsonBody = JsonConvert.SerializeObject (_actionMessage)
            };

            Publish (message);
        }

        public void Publish (BusMessage _message)
        {
            var mr = MessageReceived;

            if (mr != null) {
                mr (_message);
            }
        }

        public void Dispose ()
        {
            m_Manager = null;

            //TODO dispose of the bus
        }
    }
}

