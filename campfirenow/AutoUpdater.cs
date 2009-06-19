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
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;

namespace Conversive.AutoUpdater
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public class AutoUpdater : Component
    {
        //Added 11/16/2004 For Proxy Clients, Thanks George for submitting these changes

        #region Delegates

        public delegate void AutoUpdateComplete();

        public delegate void AutoUpdateError(string stMessage, Exception e);

        public delegate void ConfigFileDownloaded(bool bNewVersionAvailable);

        #endregion

        //If true, the app will automatically download the latest version, if false the app will use the DownloadForm to prompt the user, if AutoDownload is false and DownloadForm is null, it doesn't download

        //If true, the app will restart automatically, if false the app will use the RestartForm to prompt the user, if AutoRestart is false and RestartForm is null, it doesn't restart

        private AutoUpdateConfig autoUpdateConfig;

        //Added 11/16/2004 For Proxy Clients, Thanks George for submitting these changes

        public AutoUpdater()
        {
            //
            // If it was easy, anybody could do it!
            //
        }

        [DefaultValue(false)]
        [Description("Set to True if you want to use http proxy."),
         Category("AutoUpdater Configuration")]
        public bool ProxyEnabled { get; set; }

        [DefaultValue(@"http://myproxy.com:8080/")]
        [Description("The Proxy server URL.(For example:http://myproxy.com:port)"),
         Category("AutoUpdater Configuration")]
        public string ProxyUrl { get; set; }

        [DefaultValue(@"")]
        [Description("The UserName to authenticate with."),
         Category("AutoUpdater Configuration")]
        public string LoginUserName { get; set; }

        [DefaultValue(@"")]
        [Description("The Password to authenticate with."),
         Category("AutoUpdater Configuration")]
        public string LoginUserPass { get; set; }

        [DefaultValue(@"http://localhost/UpdateConfig.xml")]
        [Description("The URL Path to the configuration file."),
         Category("AutoUpdater Configuration")]
        public string ConfigUrl { get; set; }

        [DefaultValue(true)]
        [Description(
            "Set to True if you want the app to restart automatically, set to False if you want to use the DownloadForm to prompt the user, if AutoDownload is false and DownloadForm is null, the app will not download the latest version."
            ),
         Category("AutoUpdater Configuration")]
        public bool AutoDownload { get; set; }

        public Form DownloadForm { get; set; }

        [DefaultValue(false)]
        [Description(
            "Set to True if you want the app to restart automatically, set to False if you want to use the RestartForm to prompt the user, if AutoRestart is false and RestartForm is null, the app will not restart."
            ),
         Category("AutoUpdater Configuration")]
        public bool AutoRestart { get; set; }

        public Form RestartForm { get; set; }

        [Browsable(false)]
        public string LatestConfigChanges
        {
            get
            {
                string stRet = null;
                //Protect against NPE's
                if (autoUpdateConfig != null)
                    stRet = autoUpdateConfig.LatestChanges;
                return stRet;
            }
        }

        [Browsable(false)]
        public Version CurrentAppVersion
        {
            get { return Assembly.GetEntryAssembly().GetName().Version; }
        }

        [Browsable(false)]
        public Version LatestConfigVersion
        {
            get
            {
                Version versionRet = null;
                //Protect against NPE's
                if (autoUpdateConfig != null)
                    versionRet = new Version(autoUpdateConfig.AvailableVersion);
                return versionRet;
            }
        }

        [Browsable(false)]
        public bool NewVersionAvailable
        {
            get { return LatestConfigVersion > CurrentAppVersion; }
        }

        [Browsable(false)]
        public AutoUpdateConfig AutoUpdateConfig
        {
            get { return autoUpdateConfig; }
        }

        public event ConfigFileDownloaded OnConfigFileDownloaded;

        public event AutoUpdateComplete OnAutoUpdateComplete;

        public event AutoUpdateError OnAutoUpdateError;

        /// <summary>
        /// TryUpdate: Invoke this method if you just want to load the config without autoupdating
        /// </summary>
        public void LoadConfig()
        {
            var backgroundLoadConfigThread = new Thread(new ThreadStart(LoadConfigThread));
            backgroundLoadConfigThread.IsBackground = true;
            backgroundLoadConfigThread.Start();
        } //TryUpdate()

        /// <summary>
        /// loadConfig: This method just loads the config file so the app can check the versions manually
        /// </summary>
        private void LoadConfigThread()
        {
            var config = new AutoUpdateConfig();
            config.OnLoadConfigError += ConfigOnLoadConfigError;

            //For using untrusted SSL Certificates
            ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();

            //Do the load of the config file
            if (config.LoadConfig(ConfigUrl, LoginUserName, LoginUserPass, ProxyUrl, ProxyEnabled))
            {
                autoUpdateConfig = config;
                if (OnConfigFileDownloaded != null)
                {
                    OnConfigFileDownloaded(NewVersionAvailable);
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
            var backgroundThread = new Thread(UpdateThread) {IsBackground = true};
            backgroundThread.Start();
        } //TryUpdate()		

        /// <summary>
        /// updateThread: This is the Thread that runs for checking updates against the config file
        /// </summary>
        private void UpdateThread()
        {
            const string stUpdateName = "update";
            if (autoUpdateConfig == null) //if we haven't already downloaded the config file, do so now
                LoadConfigThread();
            if (autoUpdateConfig != null) //make sure we were able to download it
            {
                //Check the file for an update
                if (LatestConfigVersion > CurrentAppVersion)
                {
                    //Download file if the user requests or AutoDownload is True
                    //if(this.AutoDownload || (this.DownloadForm != null && this.DownloadForm.ShowDialog() == DialogResult.Yes))
                    if (true)
                    {
                        //MessageBox.Show("New Version Available, New Version: " + vConfig.ToString() + "\r\nDownloading File from: " + config.AppFileURL);
                        var diDest = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                        string stPath = diDest.FullName + Path.DirectorySeparatorChar + stUpdateName + ".zip";
                        //There is a new version available

                        if (DownloadFile(autoUpdateConfig.AppFileUrl, stPath))
                        {
                            //MessageBox.Show("Downloaded New File");
                            string stDest = diDest.FullName + Path.DirectorySeparatorChar + stUpdateName +
                                            Path.DirectorySeparatorChar;
                            //Extract Zip File
                            Unzip(stPath, stDest);
                            //Delete Zip File
                            File.Delete(stPath);
                            if (OnAutoUpdateComplete != null)
                            {
                                OnAutoUpdateComplete();
                            }
                            //Restart App if Necessary
                            //If true, the app will restart automatically, if false the app will use the RestartForm to prompt the user, if RestartForm is null, it doesn't restart
                            if (AutoRestart || (RestartForm != null && RestartForm.ShowDialog() == DialogResult.Yes))
                                Restart();
                            //else don't restart
                        }
                        //else
                        //	MessageBox.Show("Didn't Download File");
                    }
                }
                //else
                //	MessageBox.Show("No New Version Available, Web Version: " + vConfig.ToString() + ", Current Version: " +  vCurrent.ToString());
            }
        } //updateThread()

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

                request = (HttpWebRequest) HttpWebRequest.Create(url);
                //Request.Headers.Add("Translate: f"); //Commented out 11/16/2004 Matt Palmerlee, this Header is more for DAV and causes a known security issue
                if (LoginUserName != null && LoginUserName != "")
                    request.Credentials = new NetworkCredential(LoginUserName, LoginUserPass);
                else
                    request.Credentials = CredentialCache.DefaultCredentials;

                //Added 11/16/2004 For Proxy Clients, Thanks George for submitting these changes
                if (ProxyEnabled == true)
                    request.Proxy = new WebProxy(ProxyUrl);

                response = (HttpWebResponse) request.GetResponse();

                Stream respStream = null;
                respStream = response.GetResponseStream();

                //Do the Download
                var buffer = new byte[4096];
                int length;

                FileStream fs = File.Open(path, FileMode.Create, FileAccess.Write);

                length = respStream.Read(buffer, 0, 4096);
                while (length > 0)
                {
                    fs.Write(buffer, 0, length);
                    length = respStream.Read(buffer, 0, 4096);
                }
                fs.Close();
            }
            catch (Exception e)
            {
                string stMessage = "Problem downloading and copying file from: " + url + " to: " + path;
                //MessageBox.Show(stMessage);
                if (File.Exists(path))
                    File.Delete(path);
                SendAutoUpdateError(stMessage, e);
                return false;
            }
            return true;
        } //downloadFile(string url, string path)

        /// <summary>
        /// unzip: Open the zip file specified by stZipPath, into the stDestPath Directory
        /// </summary>
        private void Unzip(string stZipPath, string stDestPath)
        {
            var s = new ZipInputStream(File.OpenRead(stZipPath));

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
                    var data = new byte[2048];
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
                    catch
                    {
                    }

                    streamWriter.Close();
                }
            }
            s.Close();
        } //unzip(string stZipPath, string stDestPath)

        /// <summary>
        /// restart: Restart the app, the AppStarter will be responsible for actually restarting the main application.
        /// </summary>
        private void Restart()
        {
            Environment.ExitCode = 2; //the surrounding AppStarter must look for this to restart the app.
            Application.Exit();
        } //restart()

        private void ConfigOnLoadConfigError(string stMessage, Exception e)
        {
            SendAutoUpdateError(stMessage, e);
        }

        private void SendAutoUpdateError(string stMessage, Exception e)
        {
            if (OnAutoUpdateError != null)
                OnAutoUpdateError(stMessage, e);
        }
    } //class AutoUpdater

    public class TrustAllCertificatePolicy : ICertificatePolicy
    {
        public TrustAllCertificatePolicy()
        {
        }

        #region ICertificatePolicy Members

        public bool CheckValidationResult(ServicePoint sp,
                                          X509Certificate cert, WebRequest req, int problem)
        {
            return true;
        }

        #endregion
    } //class TrustAllCertificatePolicy
} //namespace Conversive.AutoUpdater