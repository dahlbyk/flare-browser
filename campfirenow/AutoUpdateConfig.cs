/*
 * AutoUpdateConfig.cs
 * This class is the definition of the remote XML configuration file
 * 
 * Copyright 2004 Conversive, Inc.
 * 
 */

/*
 * Conversive's C# AutoUpdater Component
 * Copyright 2004 Conversive, Inc.
 * 
 * This is a component which allows automatic software updates.
 * It is written in C# and was developed by Conversive, Inc. on April 14th 2004.
 * 
 * The C# AutoUpdater Component is licensed under the LGPL:
 * ------------------------------------------------------------------------
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 *  * 
 * ------------------------------------------------------------------------
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Xml;

namespace Conversive.AutoUpdater
{
    /// <summary>
    /// Summary description for AutoUpdateConfig.
    /// </summary>
    public class AutoUpdateConfig
    {
        #region Delegates

        public delegate void LoadConfigError(string stMessage, Exception e);

        #endregion

        public string AvailableVersion { get; set; }

        public string AppFileUrl { get; set; }

        public string LatestChanges { get; set; }

        public string ChangeLogUrl { get; set; }

        public event LoadConfigError OnLoadConfigError;

        /// <summary>
        /// LoadConfig: Invoke this method when you are ready to populate this object
        /// </summary>
        public bool LoadConfig(string url, string user, string pass, string proxyUrl, bool proxyEnabled)
        {
            try
            {
                //Load the xml config file
                var xmlDoc = new XmlDocument();
                //Retrieve the File

                var request = (HttpWebRequest) WebRequest.Create(url);
                //Request.Headers.Add("Translate: f"); //Commented out 11/16/2004 Matt Palmerlee, this Header is more for DAV and causes a known security issue
                request.Credentials = !string.IsNullOrEmpty(user) ? new NetworkCredential(user, pass) : CredentialCache.DefaultCredentials;

                //Added 11/16/2004 For Proxy Clients, Thanks George for submitting these changes
                if (proxyEnabled)
                    request.Proxy = new WebProxy(proxyUrl, true);

                var response = (HttpWebResponse) request.GetResponse();

                Stream respStream;
                respStream = response.GetResponseStream();

                //Load the XML from the stream
                xmlDoc.Load(respStream);

                //Parse out the AvailableVersion
                XmlNode availableVersionNode = xmlDoc.SelectSingleNode(@"//AvailableVersion");
                AvailableVersion = availableVersionNode.InnerText;

                //Parse out the AppFileURL
                XmlNode appFileUrlNode = xmlDoc.SelectSingleNode(@"//AppFileURL");
                AppFileUrl = appFileUrlNode.InnerText;

                //Parse out the LatestChanges
                XmlNode latestChangesNode = xmlDoc.SelectSingleNode(@"//LatestChanges");
                if (latestChangesNode != null)
                    LatestChanges = latestChangesNode.InnerText;
                else
                    LatestChanges = "";

                //Parse out the ChangLogURL
                XmlNode changeLogUrlNode = xmlDoc.SelectSingleNode(@"//ChangeLogURL");
                ChangeLogUrl = changeLogUrlNode != null ? changeLogUrlNode.InnerText : "";
            }
            catch (Exception e)
            {
                string stMessage = "Failed to read the config file at: " + url +
                                   "\r\nMake sure that the config file is present and has a valid format.";
                Debug.WriteLine(stMessage);
                if (OnLoadConfigError != null)
                    OnLoadConfigError(stMessage, e);

                return false;
            }
            return true;
        } //LoadConfig(string url, string user, string pass)
    } //class AutoUpdateConfig
} //namespace Conversive.AutoUpdater