﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace Flare
{
    class Account 
    {
        public String Name { get; set; }
        public Boolean UseSsl { get; set; }
        public User User { get; set; }
        
        public String Protocol
        {
            get { return String.Format("http{0}://", UseSsl ? "s" : ""); }
        }

        public Uri CampfireUri
        {
            get { return new Uri(String.Format("{0}{1}.campfirenow.com/", this.Protocol, this.Name)); }
        }

        public Uri CampfireLoginUri
        {
            get { return new Uri(this.CampfireUri, "/login"); }
        }

        public Uri CampfireForgotPasswordUri
        {
            get { return new Uri(this.CampfireUri, "/forgot_password"); }
        }

        public Uri CampfireDefaultRoomUri
        {
            get { return new Uri(this.CampfireUri, User.DefaultRoomName ?? "/"); }
        }

        public static Account FromRegistry()
        {
            // Attempt to open the key
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Flare", true);

            // If the return value is null, the key doesn't exist
            if (key == null)
                // The key doesn't exist; create it / open it
                key = Registry.CurrentUser.CreateSubKey("Software\\Flare");

            // Attempt to retrieve the value X; if null is returned, the value
            // doesn't exist in the registry.
            if (key.GetValue("accountname") == null)
                return null;

            Account account = new Account();

            account.Name = key.GetValue("accountname").ToString();
            account.UseSsl = (key.GetValue("usessl", "0").ToString() == "1");

            key.Close();

            account.User = User.FromRegistry();

            return account;
        }

        public void Save()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Flare", true);

            // If the return value is null, the key doesn't exist, create it
            if (key == null)
                key = Registry.CurrentUser.CreateSubKey("Software\\Flare");

            key.SetValue("accountname", Name);
            key.SetValue("usessl", UseSsl ? "1" : "0");
            
            if (User != null)
                User.Save();
        }
    }
}