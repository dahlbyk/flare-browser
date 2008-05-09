using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace Flare
{
    /// <summary>
    /// The current user's login details and Flare preferences
    /// </summary>
    public class User
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public Int32 NotifyWindowDelay { get; set; }
        private String _nickname;
        public String Nickname
        {
            get
            {
                if (String.IsNullOrEmpty(_nickname))
                    return Username.Contains("@") ? Username.Substring(0, Username.IndexOf('@')) : Username;
                else
                    return _nickname;
            }
            set { _nickname = value; }
        }
        public Boolean LoginAsGuest { get; set; }
        public Boolean ShowMessageNotifications { get; set; }
        public Boolean NotifyOnlyWhenNicknameIsFound { get; set; }
        public String DefaultRoomName { get; set; }

        /// <summary>
        /// Retreives the user's details and preferences from the default registry entries.
        /// </summary>
        public static User FromRegistry()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Flare", true);

            // If the return value is null, the key doesn't exist, create it
            if (key == null)
                key = Registry.CurrentUser.CreateSubKey("Software\\Flare");

            User user = new User();

            user.LoginAsGuest = (key.GetValue("loginAsGuest").ToString() == "1");
            user.Username = key.GetValue("username").ToString();
            user.Nickname = key.GetValue("nickname", String.Empty).ToString();
            user.Password = key.GetValue("password").ToString();
            try
            {
                user.NotifyWindowDelay = Int32.Parse(key.GetValue("notifydelay", "1500").ToString());
            }
            catch (FormatException)
            {
                user.NotifyWindowDelay = 1500;
            } 
            user.ShowMessageNotifications = (key.GetValue("showMsgNotify", "1").ToString() == "1");
            user.DefaultRoomName = key.GetValue("defaultroom", "notset").ToString();
            user.NotifyOnlyWhenNicknameIsFound = key.GetValue("nicknotifications", "0").ToString() == "1";

            key.Close();

            return user;
        }

        public void Save()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Flare", true);

            // If the return value is null, the key doesn't exist, create it
            if (key == null)
                key = Registry.CurrentUser.CreateSubKey("Software\\Flare");

            key.SetValue("loginAsGuest", LoginAsGuest ? "1" : "0");
            key.SetValue("username", Username);
            key.SetValue("password", Password);
            key.SetValue("nickname", Nickname);
            key.SetValue("notifydelay", NotifyWindowDelay.ToString());
            key.SetValue("defaultroom", DefaultRoomName);
            key.SetValue("nicknotifications", NotifyOnlyWhenNicknameIsFound ? "1" : "0");
            key.Close();
        }
    }
}
