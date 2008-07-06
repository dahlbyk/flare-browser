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
using System.Xml;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;

namespace Conversive.AutoUpdater
{
	/// <summary>
	/// Summary description for AutoUpdateConfig.
	/// </summary>
	public class AutoUpdateConfig
	{

		private string _AvailableVersion;
		public string AvailableVersion 
		{ get { return _AvailableVersion; } set { _AvailableVersion = value; } }

		private string _AppFileURL;
		public string AppFileUrl 
		{ get { return _AppFileURL; } set { _AppFileURL = value; } }

		private string _LatestChanges;
		public string LatestChanges 
		{ get { return _LatestChanges; } set { _LatestChanges = value; } }

		private string _ChangeLogURL;
		public string ChangeLogUrl 
		{ get { return _ChangeLogURL; } set { _ChangeLogURL = value; } }

		public delegate void LoadConfigError(string stMessage, Exception e);
		public event LoadConfigError OnLoadConfigError;

		/// <summary>
		/// LoadConfig: Invoke this method when you are ready to populate this object
		/// </summary>
		public bool LoadConfig(string url, string user, string pass, string proxyUrl, bool proxyEnabled)
		{
			try 
			{
				//Load the xml config file
				XmlDocument xmlDoc = new XmlDocument();
				HttpWebResponse response;
				HttpWebRequest request;
				//Retrieve the File

				request = (HttpWebRequest)HttpWebRequest.Create(url);
				//Request.Headers.Add("Translate: f"); //Commented out 11/16/2004 Matt Palmerlee, this Header is more for DAV and causes a known security issue
				if(user != null && user != "")
					request.Credentials = new NetworkCredential(user, pass);
				else
					request.Credentials = CredentialCache.DefaultCredentials;

				//Added 11/16/2004 For Proxy Clients, Thanks George for submitting these changes
				if(proxyEnabled == true)
					request.Proxy = new WebProxy(proxyUrl,true);

				response = (HttpWebResponse)request.GetResponse();

				Stream respStream = null;
				respStream = response.GetResponseStream();

				//Load the XML from the stream
				xmlDoc.Load(respStream);

				//Parse out the AvailableVersion
				XmlNode availableVersionNode = xmlDoc.SelectSingleNode(@"//AvailableVersion");
				this.AvailableVersion = availableVersionNode.InnerText;

				//Parse out the AppFileURL
				XmlNode appFileUrlNode = xmlDoc.SelectSingleNode(@"//AppFileURL");
				this.AppFileUrl = appFileUrlNode.InnerText;

				//Parse out the LatestChanges
				XmlNode latestChangesNode = xmlDoc.SelectSingleNode(@"//LatestChanges");
				if(latestChangesNode != null)
					this.LatestChanges = latestChangesNode.InnerText;
				else
					this.LatestChanges = "";

				//Parse out the ChangLogURL
				XmlNode changeLogUrlNode = xmlDoc.SelectSingleNode(@"//ChangeLogURL");
				if(changeLogUrlNode != null)
					this.ChangeLogUrl = changeLogUrlNode.InnerText;
				else
					this.ChangeLogUrl = "";
			
			} 
			catch (Exception e)
			{
				string stMessage = "Failed to read the config file at: " + url + "\r\nMake sure that the config file is present and has a valid format.";
				Debug.WriteLine(stMessage); 
				//MessageBox.Show(stMessage); 
				if(this.OnLoadConfigError != null)
					this.OnLoadConfigError(stMessage, e);

				return false;
			}
			return true;
		}//LoadConfig(string url, string user, string pass)


	}//class AutoUpdateConfig
}//namespace Conversive.AutoUpdater
