using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Flare.Properties;
using Microsoft.Win32;

namespace Flare
{
    public partial class MainForm : Form
    {
        public const int FlashwAll = (FlashwCaption | FlashwTray);
        public const int FlashwCaption = 0x00000001;
        public const int FlashwStop = 0;
        public const int FlashwTimer = 0x00000004;
        public const int FlashwTimernofg = 0x0000000C;
        public const int FlashwTray = 0x00000002;
        private Account account;

        private Boolean isFirstLoad;
        private Boolean isInStartUpMode;
        private Message lastMessage;
        private Int32 newMessagesTotal;
        private String roomTitle = String.Empty;

        public MainForm(string[] args)
        {
            InitializeComponent();

            isInStartUpMode = (args.Length > 0 && args[0] == "-startup");
        }

        public Account Account
        {
            get { return account; }
        }

        public String ProgressLabelText { get; set; }

        [DllImport("user32.dll")]
        public static extern bool FlashWindowEX([MarshalAs(UnmanagedType.Struct)] ref FLASHWINFO pfwi);


        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                account = Account.FromRegistry();
                if (account == null)
                {
                    // Show the modal dialog to fill these details
                    var sf = new SetupForm();
                    sf.ShowDialog();

                    // Try again to retreive the details from the registry
                    account = Account.FromRegistry();
                }

                // If the account is still null, we have a problem
                if (account == null)
                    Application.Exit();
                else
                {
                    showMessageNotificationToolStripMenuItem.Checked = account.User.ShowMessageNotifications;

                    isFirstLoad = true;

                    // Does the user use a proxy server?
                    IWebProxy proxy = WebRequest.DefaultWebProxy;
                    if (!proxy.IsBypassed(account.CampfireUri))
                    {
                        autoUpdater.ProxyEnabled = true;
                        autoUpdater.ProxyUrl = proxy.GetProxy(account.CampfireUri).AbsoluteUri;
                    }

                    // Start opening the user's Campfire account
                    webBrowser.Navigate(account.CampfireUri);

                    // Check for any Flare updates
                    autoUpdater.TryUpdate();
                }
            }
            catch (Exception err)
            {
                FlareException.ShowFriendly(err);
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
                    notifyIcon.Text = Text;
                }

                // Make sure we've not left campfire site:
                if (
                    webBrowser.Url.AbsoluteUri.Contains(
                        Uri.EscapeUriString(Application.StartupPath.Replace("\\", "/")) + "/waiting.htm"))
                {
                    waitingTimer.Enabled = true;
                    return;
                }
                if (webBrowser.Document != null)
                    if (webBrowser.Document.Url != null)
                        if (webBrowser.Document.Title == "Navigation Canceled" ||
                            webBrowser.Document.Url.AbsoluteUri.Contains("res://"))
                        {
                            loadingCover.Visible = false;
                            webBrowser.Navigate("file:///" + Application.StartupPath.Replace("\\", "/") + "/404.htm");
                            return;
                        }
                if (
                    webBrowser.Url.AbsoluteUri.Contains(
                        Uri.EscapeUriString(Application.StartupPath.Replace("\\", "/")) + "/404.htm"))
                {
                    // Wait for user
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
                        if (webBrowser.Document != null)
                        {
                            if (webBrowser.Document.Body != null)
                                if (webBrowser.Document.Body.InnerHtml.Contains("id=errorMessage"))
                                {
                                    DialogResult result =
                                        MessageBox.Show(
                                            string.Format("{0}\n\nWould you like to try to recover your password?", webBrowser.Document.GetElementById("errorMessage").InnerText), "Invalid Login",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                                    if (result == DialogResult.Yes)
                                    {
                                        webBrowser.Navigate(account.CampfireForgotPasswordUri);
                                        isFirstLoad = false;
                                    }
                                    return;
                                }

                            // Fill in login info for the user
                            ((MSHTML.HTMLInputElement) (webBrowser.Document.GetElementById("email_address").DomElement)).
                                value = account.User.Username;
                            ((MSHTML.HTMLInputElement) (webBrowser.Document.GetElementById("password").DomElement)).value =
                                account.User.Password;
                            ((MSHTML.HTMLInputElement) (webBrowser.Document.GetElementsByTagName("input")[3].DomElement)).
                                click();
                        }
                    }
                    else
                    {
                        isFirstLoad = false;

                        // Don't need to log in, update the title:
                        UpdateTitle(false);
                        UpdateRoomList();

                        // Navigate to default room (if one is listed):
                        if (account.User.DefaultRoomName.Contains("/room/") &&
                            webBrowser.Document.Body.InnerHtml.Contains(account.User.DefaultRoomName))
                            webBrowser.Navigate(account.CampfireDefaultRoomUri);
                        else
                            loadingCover.Visible = false;
                    }
                }
                else
                {
                    loadingCover.Visible = false;
                    UpdateTitle(false);
                    UpdateRoomList();
                }

                timer.Enabled = true;
            }
            catch (Exception err)
            {
                FlareException.ShowFriendly(err);
            }
        }

        private void UpdateRoomList()
        {
            try
            {
                int roomCount = 0;

                roomsToolStripMenuItem.DropDownItems.Clear();

                foreach (
                    MSHTML.HTMLAnchorElement link in ((MSHTML.IHTMLDocument2) webBrowser.Document.DomDocument).anchors)
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

                        var newToolStripMenuItem = new ToolStripMenuItem();
                        newToolStripMenuItem.Tag = link;
                        newToolStripMenuItem.Name = "talkToolStripMenuItem";
                        if (dKey != Keys.Cancel)
                            newToolStripMenuItem.ShortcutKeys = ((Keys) ((Keys.Control | dKey)));
                        newToolStripMenuItem.Size = new Size(152, 22);
                        newToolStripMenuItem.Text = link.innerText;
                        roomsToolStripMenuItem.DropDownItems.Add(newToolStripMenuItem);
                        newToolStripMenuItem.Click += NewToolStripMenuItemClick;
                    }
                }
            }
            catch
            {
            }
        }

        private void UpdateTitle(bool checkForNewMessages)
        {
            if (checkForNewMessages)
                CheckForMessages();

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
                            roomTitle = roomTitle.Substring(roomTitle.IndexOf(")") + 1);
                        }
                    }
                    catch
                    {
                    }
                }
            }

            if (newMessagesTotal > 0)
                Text = "(" + newMessagesTotal + ") " + FirstLetterToUpper(account.Name) + " | " +
                       FirstLetterToUpper(roomTitle);
            else
                Text = FirstLetterToUpper(account.Name) + " | " + FirstLetterToUpper(roomTitle);


            if (newMessagesTotal > oldNewMsgTotal && Focused == false && webBrowser.Focused == false)
            {
                var f = new FLASHWINFO
                            {
                                CbSize = Marshal.SizeOf(typeof (FLASHWINFO)),
                                Hwnd = Handle,
                                DWFlags = FlashwAll,
                                UCount = 2,
                                DWTimeout = 0
                            };

                FlashWindowEX(ref f);
            }
        }

        private void CheckForMessages()
        {
            try
            {
                // Don't check for messages if we're in the lobby:
                if (webBrowser.Document != null && webBrowser.Document.Url != null && webBrowser.Document.Body != null &&
                    webBrowser.Document.Url.AbsoluteUri.Contains("/room/"))
                {
                    // Don't do this if the form is focused.
                    if (!Focused && !webBrowser.Focused)
                    {
                        if (lastMessage == null || lastMessage.ElementId.Length == 0)
                            try
                            {
                                foreach (HtmlElement table in webBrowser.Document.Body.All)
                                    if (table.TagName.ToLower().Contains("table"))
                                        foreach (HtmlElement ele in table.All)
                                            if (((MSHTML.IHTMLElement) ele.DomElement).className != null &&
                                                ele.TagName.ToLower().Contains("tr") &&
                                                (((MSHTML.IHTMLElement) ele.DomElement).className.Contains(
                                                     "text_message") ||
                                                 ((MSHTML.IHTMLElement) ele.DomElement).className.Contains(
                                                     "enter_message") ||
                                                 ((MSHTML.IHTMLElement) ele.DomElement).className.Contains(
                                                     "upload_message") ||
                                                 ((MSHTML.IHTMLElement) ele.DomElement).className.Contains(
                                                     "paste_message")))
                                                lastMessage = new Message(ele.InnerText, ele.InnerText, ele.Id);
                            }
                            catch
                            {
                                // ignore any errors while the document is loading here
                            }
                        else
                        {
                            // Find the last message's new element (it will change each time the html does:
                            HtmlElement lastElement = webBrowser.Document.All[lastMessage.ElementId];
                            
                            // Make sure our last element still exists
                            if (lastElement == null)
                                return;

                            HtmlElement nextElement = lastElement.NextSibling;
                            while (nextElement != null)
                            {
                                string name = "";
                                string message = "";

                                foreach (HtmlElement td in nextElement.All)
                                {
                                    if (td.DomElement == null || ((MSHTML.IHTMLElement) td.DomElement).className == null)
                                        continue;
                                    else if (((MSHTML.IHTMLElement) td.DomElement).className.Contains("person"))
                                        name = td.InnerText;
                                    else if (((MSHTML.IHTMLElement) td.DomElement).className.Contains("body"))
                                        message = td.InnerText;
                                }

                                // Get message element:
                                lastMessage = new Message(name, message, nextElement.Id);

                                // Make sure it isn't from "you"
                                if (!((MSHTML.IHTMLElement) nextElement.DomElement).className.Contains(" you") &&
                                    (((MSHTML.IHTMLElement) nextElement.DomElement).className.Contains("text_message") ||
                                     ((MSHTML.IHTMLElement) nextElement.DomElement).className.Contains("enter_message") ||
                                     ((MSHTML.IHTMLElement) nextElement.DomElement).className.Contains("upload_message") ||
                                     ((MSHTML.IHTMLElement) nextElement.DomElement).className.Contains("paste_message")))
                                {
                                    if (!account.User.NotifyOnlyWhenNicknameIsFound ||
                                        lastMessage.TextMessage.ToLower().Contains(account.User.Nickname))
                                    {
                                        // Show the notification
                                        if (showMessageNotificationToolStripMenuItem.Checked)
                                        {
                                            var nf = new NotifyForm(Text, lastMessage.Name, lastMessage.TextMessage,
                                                                    this);
                                            nf.Show();
                                        }

                                        // Increase the unread message count:
                                        newMessagesTotal++;
                                    }
                                }

                                nextElement = nextElement.NextSibling;
                            }
                        }
                    }
                    else
                    {
                        newMessagesTotal = 0;

                        if (lastMessage != null && lastMessage.ElementId.Length > 0)
                        {
                            HtmlElement nextElement = webBrowser.Document.All[lastMessage.ElementId].NextSibling;
                            while (nextElement != null)
                            {
                                lastMessage = new Message("", "",
                                                           webBrowser.Document.All[lastMessage.ElementId].NextSibling.
                                                               Id);
                                nextElement = nextElement.NextSibling;
                            }
                        }
                        else
                        {
                            try
                            {
                                foreach (HtmlElement table in webBrowser.Document.Body.All)
                                    if (table.TagName.ToLower().Contains("table"))
                                        foreach (HtmlElement ele in table.All)
                                            if (((MSHTML.IHTMLElement) ele.DomElement).className != null &&
                                                ele.TagName.ToLower().Contains("tr") &&
                                                ((MSHTML.IHTMLElement) ele.DomElement).className.Contains("text_message"))
                                                lastMessage = new Message(ele.InnerText, ele.InnerText, ele.Id);
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
            var resources = new ResourceManager(typeof (Resources));
            if (newMessagesTotal == 0)
                notifyIcon.Icon = ((Icon) (resources.GetObject("noNewMsgs")));
            else
                notifyIcon.Icon = ((Icon) (resources.GetObject("newMsg")));
        }

        private static string FirstLetterToUpper(string inStr)
        {
            return inStr[0].ToString().ToUpper() + inStr.ToLower().Substring(1);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateTitle(true);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Run closing routine:
            ExitingRoutine();
            Application.Exit();
        }

        private void ExitingRoutine()
        {
            if (webBrowser != null && webBrowser.Url != null && webBrowser.Url.AbsoluteUri.Contains("/room/"))
            {
                account.User.DefaultRoomName =
                    webBrowser.Url.AbsoluteUri.Substring(webBrowser.Url.AbsoluteUri.IndexOf("/room/"));
                account.Save();
            }
        }

        private static void NewToolStripMenuItemClick(object sender, EventArgs e)
        {
            ((MSHTML.HTMLAnchorElement) ((ToolStripMenuItem) sender).Tag).click();
        }

        private void changeSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sf = new SetupForm();
            sf.ShowDialog();

            // if anything was changed, reload the page
            if (sf.NewAccountName != account.Name ||
                sf.NewUsername != account.User.Username ||
                sf.NewPassword != account.User.Password ||
                sf.NewNickname != account.User.Nickname ||
                sf.NewNotifyOnlyWhenNicknameIsFound != account.User.NotifyOnlyWhenNicknameIsFound ||
                sf.NewNotifyWindowDelay != account.User.NotifyWindowDelay)
            {
                MainForm_Load(sender, e);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Run closing routine:
            ExitingRoutine();
            Application.Exit();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            //if the form has been resised to minimize the hide the form
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
                notifyIcon.Text = Text;
            }
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
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
            TopMost = true;
            TopMost = false;
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
            MessageBox.Show("Flare\nv" + Application.ProductVersion + "\n\nMatt Brindley\nhttp://mattbrindley.com/",
                            "Flare", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void waitingTimer_Tick(object sender, EventArgs e)
        {
            waitingTimer.Enabled = false;
            isFirstLoad = true;
            webBrowser.Navigate(account.CampfireLoginUri);
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            // Run closing routine:
            ExitingRoutine();
            Application.Exit();
        }

        private void OpenBtn_Click(object sender, EventArgs e)
        {
            showFormHideIcon();
        }

        private void onlineSupportForumsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://code.google.com/p/flare-browser/issues/list");
        }

        private void AutoUpdaterOnAutoUpdateComplete()
        {
            MessageBox.Show(
                "Flare has been updated and needs to very quickly restart itself.\n\nPress OK to restart Flare.");
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[]) e.Data.GetData(DataFormats.FileDrop);
            account.User.UploadFileToCurrentRoom(account.CampfireUri + "/upload.cgi/room/36735/uploads/new", files[0],
                                                  webBrowser.Document.Cookie, uploadLabel);
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

        private void webBrowser_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Process.Start(webBrowser.StatusText);
        }

        #region Nested type: FLASHWINFO

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            [MarshalAs(UnmanagedType.U4)] public int CbSize;
            public IntPtr Hwnd;
            [MarshalAs(UnmanagedType.U4)] public int DWFlags;
            [MarshalAs(UnmanagedType.U4)] public int UCount;
            [MarshalAs(UnmanagedType.U4)] public int DWTimeout;
        }

        #endregion
    }

    public class Message
    {
        public Message(string name, string message, string elementId)
        {
            Name = name;
            TextMessage = message;
            ElementId = elementId;
        }

        public String Name { get; set; }
        public String TextMessage { get; set; }
        public String ElementId { get; set; }
    }
}