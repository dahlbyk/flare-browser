using System;
using System.Net;
using System.Resources;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Microsoft.Win32;
using MSHTML;
using System.Runtime.InteropServices;
using System.IO;
using System.Deployment.Application;
using System.Threading;
using System.Diagnostics;

namespace campfirenow
{
    public partial class MainForm : Form
    {
        public const int FLASHW_STOP = 0;
        public const int FLASHW_CAPTION = 0x00000001;
        public const int FLASHW_TRAY = 0x00000002;
        public const int FLASHW_ALL = (FLASHW_CAPTION | FLASHW_TRAY);
        public const int FLASHW_TIMER = 0x00000004;
        public const int FLASHW_TIMERNOFG = 0x0000000C;

        public bool isInStartUpMode = false;

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public int dwFlags;
            [MarshalAs(UnmanagedType.U4)]
            public int uCount;
            [MarshalAs(UnmanagedType.U4)]
            public int dwTimeout;
        }

        [DllImport("user32.dll")]
        public static extern bool FlashWindowEx([MarshalAs(UnmanagedType.Struct)]
    ref FLASHWINFO pfwi);

        string accountname = "";
        string username = "";
        string password = "";
        string defaultRoom = "";
        bool usessl = false;
        bool loginAsGuest = false;
        string protocol = "http://";
        
        // Forming the title
        int newMessagesTotal = 0;
        string roomTitle = "";

        private bool isFirstLoad = false;

        private List<Message> messages = new List<Message>();

        private Message lastMessage;

        public MainForm(string[] args)
        {
            InitializeComponent();

            if (args.Length > 0)
            {
                if (args[0] == "-startup")
                {
                    isInStartUpMode = true;
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Attempt to open the key
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Flare", true);

                // If the return value is null, the key doesn't exist
                if (key == null)
                {
                    // The key doesn't exist; create it / open it
                    key = Registry.CurrentUser.CreateSubKey("Software\\Flare");
                }

                // Attempt to retrieve the value X; if null is returned, the value
                // doesn't exist in the registry.
                if (key.GetValue("accountname") == null)
                {
                    // Show the modal dialog and restart this method.
                    SetupForm sf = new SetupForm();
                    sf.ShowDialog();
                    if (key.GetValue("accountname") == null)
                    {
                        Application.Exit();
                    }
                }

                accountname = key.GetValue("accountname").ToString();
                loginAsGuest = (key.GetValue("loginAsGuest").ToString() == "1");
                username = key.GetValue("username").ToString();
                password = key.GetValue("password").ToString();
                showMessageNotificationToolStripMenuItem.Checked = (key.GetValue("showMsgNotify", "1").ToString() == "1");
                try
                {
                    defaultRoom = key.GetValue("defaultroom").ToString();
                }
                catch
                {
                    key.SetValue("defaultroom", "notset");
                    defaultRoom = "notset";
                }
                try
                {
                    usessl = (key.GetValue("usessl").ToString() == "1");
                }
                catch
                {
                    usessl = false;
                }

                isFirstLoad = true;

                if (usessl)
                    protocol = "https://";
                else
                    protocol = "http://";

                // Does the user use a proxy server?
                IWebProxy proxy = WebRequest.DefaultWebProxy;
                if (!proxy.IsBypassed(new Uri(protocol + accountname.Trim() + ".campfirenow.com/")))
                {
                    autoUpdater.ProxyEnabled = true;
                    autoUpdater.ProxyURL = proxy.GetProxy(new Uri(protocol + accountname.Trim() + ".campfirenow.com/")).AbsoluteUri;
                }

                webBrowser.Navigate(protocol + accountname.Trim() + ".campfirenow.com/");

                autoUpdater.TryUpdate();
            }
            catch (System.Exception err)
            {
                MessageBox.Show("Flare has misunderstood a request from Campfire and needs to close. If you would like to help make this beta software better, post the error message below on the CampfireNow support forum at mattbrindley.com/support.\n\nThanks for your patience.\n\nFlare Exception Details:\nProduct Version: " + Application.ProductVersion + "\n" + err.Message + "\n\n" + err.StackTrace);
            }
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
         {
             try
             {
                 if (isInStartUpMode)
                 {
                     isInStartUpMode = false;

                     Hide();
                     notifyIcon.Visible = true;
                     /*ResourceManager resources = new ResourceManager(typeof(MainForm));
                     notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.ico"))); 
                     */
                     notifyIcon.Text = this.Text;

                     // Click to enter the last room they were in (if this was recorded):

                 }

                 // Make sure we've not left campfire site:
                 if (webBrowser.Url.AbsoluteUri.Contains(Uri.EscapeUriString(Application.StartupPath.Replace("\\", "/")) + "/waiting.htm"))
                 {
                     waitingTimer.Enabled = true;
                     return;
                 }
                 else if (webBrowser.Document.Title == "Navigation Canceled" || webBrowser.Document.Url.AbsoluteUri.Contains("res://"))
                 {
                     loadingCover.Visible = false;
                     webBrowser.Navigate("file:///" + Application.StartupPath.Replace("\\", "/") + "/404.htm");
                     return;
                 }
                 else if (webBrowser.Url.AbsoluteUri.Contains(Uri.EscapeUriString(Application.StartupPath.Replace("\\", "/")) + "/404.htm"))
                 {
                     // wait for user
                     return;
                 }


                 // clear the lastMessage var:
                 lastMessage = null;

                 if (isFirstLoad)
                 {

                     //Are we on the login page?
                     if (webBrowser.Url.AbsoluteUri.Contains(".campfirenow.com/login"))
                     {
                         // make sure there's no error messages:
                         if (webBrowser.Document.Body.InnerHtml.Contains("id=errorMessage"))
                         {
                             DialogResult result = MessageBox.Show(webBrowser.Document.GetElementById("errorMessage").InnerText + "\n\nWould you like to try to recover your password?", "Invalid Login", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                             if (result == DialogResult.Yes)
                             {
                                 webBrowser.Navigate(protocol + accountname.Trim() + ".campfirenow.com/forgot_password");
                                 isFirstLoad = false;
                             }
                             return;
                         }

                         // fill in login info
                         ((MSHTML.HTMLInputElement)(webBrowser.Document.GetElementById("email_address").DomElement)).value = username;
                         ((MSHTML.HTMLInputElement)(webBrowser.Document.GetElementById("password").DomElement)).value = password;
                         //((MSHTML.HTMLInputElement)(webBrowser.Document.GetElementsByTagName("input")[2].DomElement)).sele = "1";
                         ((MSHTML.HTMLInputElement)(webBrowser.Document.GetElementsByTagName("input")[3].DomElement)).click();
                         //re
                     }
                     else
                     {
                         isFirstLoad = false;

                         // don't need to log in, update the title:
                         updateTitle(false);
                         updateRoomList();

                         // Navigate to default room (if one is listed):
                         if (defaultRoom.Contains("/room/") && webBrowser.Document.Body.InnerHtml.Contains(defaultRoom))
                             webBrowser.Navigate(protocol + accountname.Trim() + ".campfirenow.com" + defaultRoom);
                         else
                             loadingCover.Visible = false;
                     }
                 }
                 else
                 {
                     loadingCover.Visible = false;
                     updateTitle(false);
                     updateRoomList();
                 }

                 timer.Enabled = true;
             }
             catch (System.Exception err)
             {
                 MessageBox.Show("Sorry Flare has misunderstood a request from Campfire and needs to close. If you would like to help make this beta software better, post the error message below on the CampfireNow support forum at mattbrindley.com/support.\n\nThanks for your patience.\n\nFlare Exception Details:\n" + err.Message + "\n\n");
             }
        }

        private void updateRoomList()
        {
            int roomCount = 0;

            roomsToolStripMenuItem.DropDownItems.Clear();

            foreach (MSHTML.HTMLAnchorElement link in ((MSHTML.IHTMLDocument2)webBrowser.Document.DomDocument).anchors)
            {
                if (link.id.Contains("room_tab"))
                {
                    roomCount++;

                    Keys dKey;
                    switch (roomCount)
                    {
                        case 1:
                            dKey = Keys.D1;
                            break;
                            case 2:
                            dKey = Keys.D2;
                            break;
                            case 3:
                            dKey = Keys.D3;
                            break;
                            case 4:
                            dKey = Keys.D4;
                            break;
                            case 5:
                            dKey = Keys.D5;
                            break;
                            case 6:
                            dKey = Keys.D6;
                            break;
                            case 7:
                            dKey = Keys.D7;
                            break;
                            case 8:
                            dKey = Keys.D8;
                            break;
                            case 9:
                            dKey = Keys.D9;
                            break;
                        default:
                            dKey = Keys.Cancel;
                            break;
                    }
                    
                    ToolStripMenuItem newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                    newToolStripMenuItem.Tag = link;
                    newToolStripMenuItem.Name = "talkToolStripMenuItem";
                    if (dKey != Keys.Cancel)
                        newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | dKey)));
                    newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
                    newToolStripMenuItem.Text = link.innerText;
                    roomsToolStripMenuItem.DropDownItems.Add(newToolStripMenuItem);
                    newToolStripMenuItem.Click += new System.EventHandler(newToolStripMenuItem_Click);
                }
            }
        }

        private void updateTitle(bool checkForNewMessages)
        {
            if (checkForNewMessages)
                checkForMessages();

            int oldNewMsgTotal = newMessagesTotal;

            if (webBrowser.DocumentTitle.ToLower() == "chat rooms")
                roomTitle = "lobby";
            else
            {
                roomTitle = webBrowser.DocumentTitle.Replace("Campfire: ", "");
                if (roomTitle[0] == '(')
                {
                    try
                    {
                        if (roomTitle.IndexOf("(") > -1 && roomTitle.IndexOf(")") > -1)
                        {
                            //newMessagesTotal = int.Parse(roomTitle.Substring(1, roomTitle.IndexOf(")") - 1));
                            roomTitle = roomTitle.Substring(roomTitle.IndexOf(")") + 1);
                        }
                       // else
                        //    newMessagesTotal = 0;
                    }
                    catch
                    {
                        //newMessagesTotal = 0;
                    }
                }
            }

            if (newMessagesTotal > 0)
                this.Text = "(" + newMessagesTotal + ") " + firstLetterToUpper(accountname) + " | " + firstLetterToUpper(roomTitle);
            else
                this.Text = firstLetterToUpper(accountname) + " | " + firstLetterToUpper(roomTitle);

            
            if (newMessagesTotal > oldNewMsgTotal && this.Focused == false && webBrowser.Focused == false)
            {
                FLASHWINFO f = new FLASHWINFO();
                f.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(FLASHWINFO));
                f.hwnd = this.Handle;

                f.dwFlags = FLASHW_ALL;
                f.uCount = 2; // Flash 10 times.
                f.dwTimeout = 0; // Use default cursor blink rate.
                FlashWindowEx(ref f);

            }
            

        }

        private void checkForMessages()
        {
            try
            {
                // Don't check for messages if we're in the lobby:
                if (webBrowser.Document != null && webBrowser.Document.Url != null && webBrowser.Document.Url.AbsoluteUri.Contains("/room/"))
                {
                    // Don't do this if the form is focused.
                    if (!this.Focused && !webBrowser.Focused)
                    {
                        try
                        {
                            if (lastMessage == null || lastMessage.ElementID.Length == 0)
                            {
                                try
                                {
                                    foreach (HtmlElement table in webBrowser.Document.Body.All)
                                    {
                                        if (table.TagName.ToLower().Contains("table"))
                                        {
                                            foreach (HtmlElement ele in table.All)
                                            {
                                                if (((MSHTML.IHTMLElement)ele.DomElement).className != null && ele.TagName.ToLower().Contains("tr") && (((MSHTML.IHTMLElement)ele.DomElement).className.Contains("text_message") || ((MSHTML.IHTMLElement)ele.DomElement).className.Contains("enter_message") || ((MSHTML.IHTMLElement)ele.DomElement).className.Contains("upload_message") || ((MSHTML.IHTMLElement)ele.DomElement).className.Contains("paste_message")))
                                                {
                                                    lastMessage = new Message(ele.InnerText, ele.InnerText, ele.Id);
                                                }
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    // ignore any errors while the document is loading here
                                }
                            }
                            else
                            {
                                // Find the last message's new element (it will change each time the html does:
                                HtmlElement nextElement = webBrowser.Document.All[lastMessage.ElementID].NextSibling;
                                while (nextElement.DomElement != null)
                                {
                                    string name = "";
                                    string message = "";

                                    foreach (HtmlElement td in nextElement.All)
                                    {
                                        if (td.DomElement == null || ((MSHTML.IHTMLElement)td.DomElement).className == null)
                                            continue;
                                        else if (((MSHTML.IHTMLElement)td.DomElement).className.Contains("person"))
                                            name = td.InnerText;
                                        else if (((MSHTML.IHTMLElement)td.DomElement).className.Contains("body"))
                                            message = td.InnerText;
                                    }

                                    // Get message element:
                                    lastMessage = new Message(name, message, nextElement.Id);

                                    // Make sure it isn't from "you"
                                    if (!((MSHTML.IHTMLElement)nextElement.DomElement).className.Contains(" you") && (((MSHTML.IHTMLElement)nextElement.DomElement).className.Contains("text_message") || ((MSHTML.IHTMLElement)nextElement.DomElement).className.Contains("enter_message") || ((MSHTML.IHTMLElement)nextElement.DomElement).className.Contains("upload_message") || ((MSHTML.IHTMLElement)nextElement.DomElement).className.Contains("paste_message")))
                                    {
                                        // SHOW THE NOTIFY
                                        if (showMessageNotificationToolStripMenuItem.Checked)
                                        {
                                            NotifyForm nf = new NotifyForm(this.Text, lastMessage.Name, lastMessage.TextMessage, this);
                                            nf.Show();
                                        }

                                        // Increase the unread message count:
                                        newMessagesTotal++;
                                    }

                                    nextElement = nextElement.NextSibling;
                                }
                            }
                        }
                        catch (System.Exception e)
                        {
                            //MessageBox.Show(e.Message + "\n\n" + e.StackTrace);
                        }
                    }
                    else
                    {
                        newMessagesTotal = 0;

                        if (lastMessage != null && lastMessage.ElementID.Length > 0)
                        {
                            HtmlElement nextElement = webBrowser.Document.All[lastMessage.ElementID].NextSibling;
                            while (nextElement.DomElement != null)
                            {
                                lastMessage = new Message("", "", webBrowser.Document.All[lastMessage.ElementID].NextSibling.Id);
                                nextElement = nextElement.NextSibling;
                            }
                        }
                        else
                        {
                            try
                            {

                                foreach (HtmlElement table in webBrowser.Document.Body.All)
                                {
                                    if (table.TagName.ToLower().Contains("table"))
                                    {
                                        foreach (HtmlElement ele in table.All)
                                        {
                                            if (((MSHTML.IHTMLElement)ele.DomElement).className != null && ele.TagName.ToLower().Contains("tr") && ((MSHTML.IHTMLElement)ele.DomElement).className.Contains("text_message"))
                                            {
                                                lastMessage = new Message(ele.InnerText, ele.InnerText, ele.Id);
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                // ignore any errors while the document is loading here
                            }
                        }

                    }
                }
            }
            catch
            {
                // do nothing
            }
            
            // Update the notify icon:
            ResourceManager resources = new ResourceManager(typeof(flare.Properties.Resources));
            if (newMessagesTotal == 0)
            {
                notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("noNewMsgs")));
            }
            else
            {
                notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("newMsg")));
            }

        }

        private string firstLetterToUpper(string inStr)
        {
            return inStr[0].ToString().ToUpper() + inStr.ToLower().Substring(1);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
                updateTitle(true);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Run closing routine:
            exitingRoutine();
            Application.Exit();
        }

        private void exitingRoutine()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Flare", true);

                // If the return value is null, the key doesn't exist
                if (key == null)
                {
                    // The key doesn't exist; create it / open it
                    key = Registry.CurrentUser.CreateSubKey("Software\\Flare");
                }

                if (webBrowser.Url.AbsoluteUri.Contains("/room/"))
                {
                    defaultRoom = webBrowser.Url.AbsoluteUri.Substring(webBrowser.Url.AbsoluteUri.IndexOf("/room/"));

                    key.SetValue("defaultroom", defaultRoom);
                }
            }
            catch
            {
                //
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((MSHTML.HTMLAnchorElement)((ToolStripMenuItem)sender).Tag).click();
        }

        private void talkToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changeSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupForm sf = new SetupForm();
            sf.ShowDialog();
            if (sf.newAccountName != accountname || sf.newUsername != username || sf.newPassword != password)
            {
                MainForm_Load(sender, e);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Run closing routine:
            exitingRoutine();
            Application.Exit();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            //if the form has been resised to minimize the hide the form
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
                /*ResourceManager resources = new ResourceManager(typeof(MainForm));
                notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.ico"))); 
                */notifyIcon.Text = this.Text;
            }
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            //showFormHideIcon();
        }

        public void ShowFormHideIcon()
        {
            showFormHideIcon();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            showFormHideIcon();
        }

        private void showFormHideIcon()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            //this.Focus();
            notifyIcon.Visible = false;
            this.TopMost = true;
            this.TopMost = false;
        }

        private void showMessageNotificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showMessageNotificationToolStripMenuItem.Checked)
            {
                showMessageNotificationToolStripMenuItem.Checked = false;

                // Attempt to open the key
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Flare", true);

                // If the return value is null, the key doesn't exist
                if (key == null)
                {
                    // The key doesn't exist; create it / open it
                    key = Registry.CurrentUser.CreateSubKey("Software\\Flare");
                }

                key.SetValue("showMsgNotify", "0");
            }
            else
            {
                showMessageNotificationToolStripMenuItem.Checked = true;

                // Attempt to open the key
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Flare", true);

                // If the return value is null, the key doesn't exist
                if (key == null)
                {
                    // The key doesn't exist; create it / open it
                    key = Registry.CurrentUser.CreateSubKey("Software\\Flare");
                }

                key.SetValue("showMsgNotify", "1");
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Flare\nv" + Application.ProductVersion + "\n\nMatt Brindley\nhttp://mattbrindley.com/", "Flare", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void waitingTimer_Tick(object sender, EventArgs e)
        {
            waitingTimer.Enabled = false;
            isFirstLoad = true;
            webBrowser.Navigate(protocol + accountname.Trim() + ".campfirenow.com/login");
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            // Run closing routine:
            exitingRoutine();
            Application.Exit();
        }

        private void OpenBtn_Click(object sender, EventArgs e)
        {
            showFormHideIcon();
        }

        private void onlineSupportForumsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://mattbrindley.com/support/?CategoryID=2");
        }

        private void autoUpdater_OnAutoUpdateComplete()
        {
            MessageBox.Show("Flare has been updated and needs to very quickly restart itself.\n\nPress OK to restart Flare.");
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {

                MessageBox.Show(file);

            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {   
            e.Effect = DragDropEffects.Copy;
        }

        private void MainForm_DragLeave(object sender, EventArgs e)
        {

        }

        private void MainForm_DragOver(object sender, DragEventArgs e)
        {

        }


    }

    public class Message
    {
        public string Name;
        public string TextMessage;
        public string ElementID;

        public Message(string name, string message, string elementID)
        {
            Name = name;
            TextMessage = message;
            ElementID = elementID;
        }
    }
}