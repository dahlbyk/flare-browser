/*
 * AutoUpdater.cs
 * This class is the main component of the AutoUpdater
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
 * 
 * ------------------------------------------------------------------------
 */

using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using System.Net;

using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace Conversive.AutoUpdater
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class AutoUpdater: Component
	{
		public AutoUpdater()
		{
			//
			// If it was easy, anybody could do it!
			//
		}

		//Added 11/16/2004 For Proxy Clients, Thanks George for submitting these changes
		private bool _ProxyEnabled;
		[DefaultValue(false)]
		[Description("Set to True if you want to use http proxy."), 
		Category("AutoUpdater Configuration")]
		public bool ProxyEnabled 
		{ get { return _ProxyEnabled; } set { _ProxyEnabled = value; } }

		//Added 11/16/2004 For Proxy Clients, Thanks George for submitting these changes
		private string _ProxyURL;
		[DefaultValue(@"http://myproxy.com:8080/")]
		[Description("The Proxy server URL.(For example:http://myproxy.com:port)"), 
		Category("AutoUpdater Configuration")]
		public string ProxyUrl 
		{ get { return _ProxyURL; } set { _ProxyURL = value; } }

		private string _LoginUserName;
		[DefaultValue(@"")]
		[Description("The UserName to authenticate with."), 
		Category("AutoUpdater Configuration")]
		public string LoginUserName 
		{ get { return _LoginUserName; } set { _LoginUserName = value; } }

		private string _LoginUserPass;
		[DefaultValue(@"")]
		[Description("The Password to authenticate with."), 
		Category("AutoUpdater Configuration")]
		public string LoginUserPass 
		{ get { return _LoginUserPass; } set { _LoginUserPass = value; } }

		private string _ConfigURL;
		[DefaultValue(@"http://localhost/UpdateConfig.xml")]
		[Description("The URL Path to the configuration file."), 
		Category("AutoUpdater Configuration")]
		public string ConfigUrl 
		{ get { return _ConfigURL; } set { _ConfigURL = value; } }

		private bool _AutoDownload;//If true, the app will automatically download the latest version, if false the app will use the DownloadForm to prompt the user, if AutoDownload is false and DownloadForm is null, it doesn't download
		[DefaultValue(true)]
		[Description("Set to True if you want the app to restart automatically, set to False if you want to use the DownloadForm to prompt the user, if AutoDownload is false and DownloadForm is null, the app will not download the latest version."), 
		Category("AutoUpdater Configuration")]
		public bool AutoDownload 
		{ get { return _AutoDownload; } set { _AutoDownload = value; } }

		private Form _DownloadForm;
		public Form DownloadForm 
		{ get { return _DownloadForm; } set { _DownloadForm = value; } }

		private bool _AutoRestart;//If true, the app will restart automatically, if false the app will use the RestartForm to prompt the user, if AutoRestart is false and RestartForm is null, it doesn't restart
		[DefaultValue(false)]
		[Description("Set to True if you want the app to restart automatically, set to False if you want to use the RestartForm to prompt the user, if AutoRestart is false and RestartForm is null, the app will not restart."), 
		Category("AutoUpdater Configuration")]
		public bool AutoRestart 
		{ get { return _AutoRestart; } set { _AutoRestart = value; } }

		private Form _RestartForm;
		public Form RestartForm 
		{ get { return _RestartForm; } set { _RestartForm = value; } }

		[BrowsableAttribute(false)]
		public string LatestConfigChanges
		{ 
			get 
			{ 
				string stRet = null;
				//Protect against NPE's
				if(this._AutoUpdateConfig != null)
					stRet = _AutoUpdateConfig.LatestChanges;
				return stRet; 
			} 
		}

		[BrowsableAttribute(false)]
		public Version CurrentAppVersion
		{ get { return System.Reflection.Assembly.GetEntryAssembly().GetName().Version; } }

		[BrowsableAttribute(false)]
		public Version LatestConfigVersion
		{ 
			get 
			{ 
				Version versionRet = null;
				//Protect against NPE's
				if(this._AutoUpdateConfig != null)
					versionRet = new Version(this._AutoUpdateConfig.AvailableVersion);
				return versionRet; 
			} 
		}

		[BrowsableAttribute(false)]
		public bool NewVersionAvailable
		{ get { return this.LatestConfigVersion > this.CurrentAppVersion; } }

		private AutoUpdateConfig _AutoUpdateConfig;
		[BrowsableAttribute(false)]
		public AutoUpdateConfig AutoUpdateConfig
		{ get { return _AutoUpdateConfig; } }

		public delegate void ConfigFileDownloaded(bool bNewVersionAvailable);
		public event ConfigFileDownloaded OnConfigFileDownloaded;

		public delegate void AutoUpdateComplete();
		public event AutoUpdateComplete OnAutoUpdateComplete;

		public delegate void AutoUpdateError(string stMessage, Exception e);
		public event AutoUpdateError OnAutoUpdateError;

		/// <summary>
		/// TryUpdate: Invoke this method if you just want to load the config without autoupdating
		/// </summary>
		public void LoadConfig()
		{
			Thread backgroundLoadConfigThread = new Thread(new ThreadStart(this.LoadConfigThread));
			backgroundLoadConfigThread.IsBackground = true;
			backgroundLoadConfigThread.Start();
		}//TryUpdate()

		/// <summary>
		/// loadConfig: This method just loads the config file so the app can check the versions manually
		/// </summary>
		private void LoadConfigThread()
		{
			AutoUpdateConfig config = new AutoUpdateConfig();
			config.OnLoadConfigError += new Conversive.AutoUpdater.AutoUpdateConfig.LoadConfigError(ConfigOnLoadConfigError);
			
			//For using untrusted SSL Certificates
			System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();

			//Do the load of the config file
			if(config.LoadConfig(this.ConfigUrl, this.LoginUserName, this.LoginUserPass, this.ProxyUrl, this.ProxyEnabled))
			{
				this._AutoUpdateConfig = config;
				if(this.OnConfigFileDownloaded != null)
				{
					this.OnConfigFileDownloaded(this.NewVersionAvailable);
				}
			}
			//else
			//	MessageBox.Show("Problem loading config file, from: " + this.ConfigURL);
		}

		/// <summary>
		/// TryUpdate: Invoke this method when you are ready to run the update checking thread
		/// </summary>
		public void TryUpdate()
		{
            Thread backgroundThread = new Thread(new ThreadStart(this.UpdateThread));
			backgroundThread.IsBackground = true;
			backgroundThread.Start();
         }//TryUpdate()		

		/// <summary>
		/// updateThread: This is the Thread that runs for checking updates against the config file
		/// </summary>
		private void UpdateThread()
		{
            string stUpdateName = "update";
			if(this._AutoUpdateConfig == null)//if we haven't already downloaded the config file, do so now
				this.LoadConfigThread();
			if(this._AutoUpdateConfig != null)//make sure we were able to download it
			{
				//Check the file for an update
				if(this.LatestConfigVersion > this.CurrentAppVersion)
				{
					//Download file if the user requests or AutoDownload is True
					//if(this.AutoDownload || (this.DownloadForm != null && this.DownloadForm.ShowDialog() == DialogResult.Yes))
					if (true)
                    {
						//MessageBox.Show("New Version Available, New Version: " + vConfig.ToString() + "\r\nDownloading File from: " + config.AppFileURL);
						DirectoryInfo diDest = new DirectoryInfo(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));
						string stPath = diDest.FullName + System.IO.Path.DirectorySeparatorChar + stUpdateName + ".zip";
						//There is a new version available
                        
						if(this.DownloadFile(this._AutoUpdateConfig.AppFileUrl, stPath))
						{
							//MessageBox.Show("Downloaded New File");
							string stDest = diDest.FullName + System.IO.Path.DirectorySeparatorChar + stUpdateName + System.IO.Path.DirectorySeparatorChar;
							//Extract Zip File
							this.Unzip(stPath, stDest);
							//Delete Zip File
							File.Delete(stPath);
							if(this.OnAutoUpdateComplete != null)
							{
								this.OnAutoUpdateComplete();
							}
							//Restart App if Necessary
							//If true, the app will restart automatically, if false the app will use the RestartForm to prompt the user, if RestartForm is null, it doesn't restart
							if(this.AutoRestart || (this.RestartForm != null && this.RestartForm.ShowDialog() == DialogResult.Yes))
								this.Restart();
							//else don't restart
						}
						//else
						//	MessageBox.Show("Didn't Download File");
					}
					
				}
				//else
				//	MessageBox.Show("No New Version Available, Web Version: " + vConfig.ToString() + ", Current Version: " +  vCurrent.ToString());
			}

		}//updateThread()

		/// <summary>
		/// downloadFile: Download a file from the specified url and copy it to the specified path
		/// </summary>
		private bool DownloadFile(string url, string path)
		{
			try
			{
				//create web request/response
				HttpWebResponse response;
				HttpWebRequest request;

				request = (HttpWebRequest)HttpWebRequest.Create(url);
				//Request.Headers.Add("Translate: f"); //Commented out 11/16/2004 Matt Palmerlee, this Header is more for DAV and causes a known security issue
				if(this.LoginUserName != null && this.LoginUserName != "")
					request.Credentials = new NetworkCredential(this.LoginUserName, this.LoginUserPass);
				else
					request.Credentials = CredentialCache.DefaultCredentials;

				//Added 11/16/2004 For Proxy Clients, Thanks George for submitting these changes
				if(this.ProxyEnabled == true)
					request.Proxy = new WebProxy(this.ProxyUrl);

				response = (HttpWebResponse)request.GetResponse();

				Stream respStream = null;
				respStream = response.GetResponseStream();

				//Do the Download
				byte[] buffer = new byte[4096];
				int length;

				FileStream fs = File.Open(path, FileMode.Create, FileAccess.Write);
			
				length = respStream.Read(buffer, 0, 4096);
				while(length > 0)
				{
					fs.Write(buffer, 0, length);
					length = respStream.Read(buffer, 0, 4096);
				}
				fs.Close();	
			}
			catch(Exception e)
			{
				string stMessage = "Problem downloading and copying file from: " + url + " to: " + path;
				//MessageBox.Show(stMessage);
				if(File.Exists(path))
					File.Delete(path);
				this.SendAutoUpdateError(stMessage, e);
				return false;
			}
			return true;
		}//downloadFile(string url, string path)

		/// <summary>
		/// unzip: Open the zip file specified by stZipPath, into the stDestPath Directory
		/// </summary>
		private void Unzip(string stZipPath, string stDestPath)
		{
			ZipInputStream s = new ZipInputStream(File.OpenRead(stZipPath));
		
			ZipEntry theEntry;
			while ((theEntry = s.GetNextEntry()) != null) 
			{
			
				string fileName = stDestPath + Path.GetDirectoryName(theEntry.Name) + Path.GetFileName(theEntry.Name);
			
				//create directory for file (if necessary)
				Directory.CreateDirectory(Path.GetDirectoryName(fileName));
				if (!theEntry.IsDirectory) 
				{
					FileStream streamWriter = File.Create(fileName);
				
					int size = 2048;
					byte[] data = new byte[2048];
					try
					{
						while (true) 
						{
							size = s.Read(data, 0, data.Length);
							if (size > 0) 
							{
								streamWriter.Write(data, 0, size);
							} 
							else 
							{
								break;
							}
						}
					}
					catch{}
				
					streamWriter.Close();
				}
			}
			s.Close();
		}//unzip(string stZipPath, string stDestPath)

		/// <summary>
		/// restart: Restart the app, the AppStarter will be responsible for actually restarting the main application.
		/// </summary>
		private void Restart()
		{
			Environment.ExitCode = 2; //the surrounding AppStarter must look for this to restart the app.
			Application.Exit();
		}//restart()

		private void ConfigOnLoadConfigError(string stMessage, Exception e)
		{
			this.SendAutoUpdateError(stMessage, e);
		}

		private void SendAutoUpdateError(string stMessage, Exception e)
		{
			if(this.OnAutoUpdateError != null)
				this.OnAutoUpdateError(stMessage, e);
		}
	}//class AutoUpdater

	public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
	{
		public TrustAllCertificatePolicy()
		{}

		public bool CheckValidationResult(ServicePoint sp,
			System.Security.Cryptography.X509Certificates.X509Certificate cert,WebRequest req, int problem)
		{
			return true;
		}
	}//class TrustAllCertificatePolicy
}//namespace Conversive.AutoUpdater
