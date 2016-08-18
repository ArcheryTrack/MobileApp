using System;
using System.Collections.Generic;
using ATMobile.Constants;
using ATMobile.Daos;
using ATMobile.Objects;

namespace ATMobile.Managers
{
    public class SettingManager : IDisposable
    {
        public ATManager m_Manager;

        public SettingManager (ATManager _manager)
        {
            m_Manager = _manager;
        }

        private List<Setting> GetSettings ()
        {
            SettingDao dao = new SettingDao (m_Manager.Database);
            return dao.GetAll ();
        }

        private void Persist (Setting _setting)
        {
            SettingDao dao = new SettingDao (m_Manager.Database);
            dao.Persist (_setting);
        }

        private Setting GetSetting (string _name)
        {
            SettingDao dao = new SettingDao (m_Manager.Database);
            return dao.GetSetting (_name);
        }

        private string GetSettingValue (string _name)
        {
            SettingDao dao = new SettingDao (m_Manager.Database);
            Setting setting = dao.GetSetting (_name);

            if (setting != null) {
                return setting.Value;
            }

            return null;
        }

        private void SetSetting (string _name, string _value)
        {
            SettingDao dao = new SettingDao (m_Manager.Database);
            Setting setting = dao.GetSetting (_name);

            if (setting == null) {
                setting = new Setting ();
                setting.Name = _name;
            }

            setting.Value = _value;
            dao.Persist (setting);
        }

        private void SetSetting (string _name, Guid _value)
        {
            SetSetting (_name, _value.ToString ());
        }

        private void SetSetting (string _name, bool _value)
        {
            SetSetting (_name, _value.ToString ());
        }

        private bool GetBoolSetting (string _name)
        {
            Setting setting = GetSetting (_name);

            if (setting == null) return false;

            bool output;
            if (bool.TryParse (setting.Value, out output)) {
                return output;
            }

            return false;
        }

        private Guid? GetGuidSetting (string _name)
        {
            Setting setting = GetSetting (_name);

            if (setting == null) return null;

            Guid output;
            if (Guid.TryParse (setting.Value, out output)) {
                return output;
            }

            return null;
        }

        public Guid GetInstallId ()
        {
            Guid? id = GetGuidSetting (SettingConstants.InstallId);

            if (id == null) {
                id = Guid.NewGuid ();

                SetSetting (SettingConstants.InstallId, id.Value);
            }

            return id.Value;
        }

        public string GetUsername ()
        {
            string userId = GetSettingValue (SettingConstants.Username);

            return userId;
        }

        public void SetUsername (string _username)
        {
            SetSetting (SettingConstants.Username, _username);
        }

        public Guid? GetCurrentArcher ()
        {
            return GetGuidSetting (SettingConstants.CurrentArcher);
        }

        public void SetCurrentArcher (Guid _currentArcherId)
        {
            SetSetting (SettingConstants.CurrentArcher, _currentArcherId);
        }

        public bool GetTargetFaceDataLoaded ()
        {
            return GetBoolSetting (SettingConstants.TargetFaceDataLoaded);
        }

        public void SetTargetFaceDataLoaded (bool _value)
        {
            SetSetting (SettingConstants.TargetFaceDataLoaded, _value);
        }

        public void Dispose ()
        {
            m_Manager = null;
        }
    }
}

