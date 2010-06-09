using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
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
        private String roomTitle = String.Empty;

        private bool forcedShutdown;

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
        public static extern bool FlashWindowEx([MarshalAs(UnmanagedType.Struct)] ref FLASHWINFO pfwi);


        private void MainForm_Load(object sender, EventArgs e)
        {
            Startup();

            if (account.User.MinimiseDuringStartup)
                WindowState = FormWindowState.Minimized;

            // Check for any Flare updates
            autoUpdater.TryUpdate();
        }

        private void Startup()
        {
            try
            {
                account = Account.FromRegistry();
                if (account == null)
                {
                    // Show the modal dialog to fill these details
                    var setupForm = new SetupForm();
                    setupForm.ShowDialog();

                    // Try again to retreive the details from the registry
                    account = Account.FromRegistry();
                }

                // If the account is still null, the user cancelled the dialog, the expected behaviour here is to quit
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

                    Text = string.Format("{0} - Flare", account.Name);

                    // Start opening the user's Campfire account
                    lobbyWebBrowser.Navigate(account.CampfireUri);
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
                var browser = (WebBrowser)sender;

                if (isInStartUpMode)
                {
                    isInStartUpMode = false;

                    Hide();
                    notifyIcon.Visible = true;
                    notifyIcon.Text = Text;
                }

                // Make sure we've not left campfire site:
                if (
                    browser.Url.AbsoluteUri.Contains(
                        Uri.EscapeUriString(Application.StartupPath.Replace("\\", "/")) + "/waiting.htm"))
                {
                    waitingTimer.Enabled = true;
                    return;
                }
                if (browser.Document != null)
                    if (browser.Document.Url != null)
                        if (browser.Document.Title == "Navigation Canceled" ||
                            browser.Document.Url.AbsoluteUri.Contains("res://"))
                        {
                            loadingCover.Visible = false;
                            browser.Navigate("file:///" + Application.StartupPath.Replace("\\", "/") + "/404.htm");
                            return;
                        }
                        else if (browser.Document.Url.AbsolutePath == "/")
                        {
                            // We're in the lobby, was this supposed to be a room tab?
                            if (browser != lobbyWebBrowser)  // If the current browser is not the lobby web browser, and is one of the room browsers
                            {
                                var doc = (MSHTML.IHTMLDocument2) browser.Document.DomDocument;
                                if (!string.IsNullOrEmpty(doc.referrer))
                                {
                                    if (account.User.RoomNames.Contains(new Uri(doc.referrer).AbsolutePath))
                                    {
                                        // We've been logged out of the room, restart flare automatically
                                        RestartFlare();
                                    }
                                }
                            }
                        }
                if (
                    browser.Url.AbsoluteUri.Contains(
                        Uri.EscapeUriString(Application.StartupPath.Replace("\\", "/")) + "/404.htm"))
                {
                    // Wait for user
                    return;
                }

                // clear the lastMessage var:
                browser.Tag = null;

                if (isFirstLoad)
                {
                    //Are we on the login page?
                    if (!browser.Url.AbsoluteUri.Contains(".campfirenow.com"))
                    {
                        // We've left the campfire site. We may be trying to login via an external openid login page.
                    }
                    else if (browser.Url.AbsoluteUri.Contains(".campfirenow.com/login"))
                    {
                        // make sure there's no error messages:
                        if (browser.Document != null)
                        {
                            if (browser.Document.Body != null)
                                if (browser.Document.Body.InnerHtml.Contains("id=errorMessage"))
                                {
                                    DialogResult result =
                                        MessageBox.Show(
                                            string.Format("{0}\n\nWould you like to try to recover your password?",
                                                          browser.Document.GetElementById("errorMessage").InnerText),
                                            "Invalid Login",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                                    if (result == DialogResult.Yes)
                                    {
                                        browser.Navigate(account.CampfireForgotPasswordUri);
                                        isFirstLoad = false;
                                    }
                                    return;
                                }

                            // Fill in login info for the user
                            if (account.User.UseOpenId)
                            {
                                browser.Navigate(string.Format("javascript:Login.loginWithOpenId(); $('openid_identifier').value='{0}'; $('openid_identifier').form.submit(); void(0);", account.User.OpenIdUrl));
                                Thread.Sleep(3000);
                                loadingCover.Visible = false;
                            }
                            else
                            {
                                ((MSHTML.HTMLInputElement)(browser.Document.GetElementById("username").DomElement)).
                                    value = account.User.Username;
                                ((MSHTML.HTMLInputElement)(browser.Document.GetElementById("password").DomElement)).
                                    value =
                                    account.User.Password;
                                ((MSHTML.HTMLInputElement)(browser.Document.GetElementById("username").DomElement)).
                                    form.submit();
                            }
                        }
                    }
                    else
                    {
                        isFirstLoad = false;

                        // Don't need to log in, update the title:
                        PreparePageHtml(browser);
                        UpdateTitle(false, browser);
                        UpdateRoomList();

                        // Navigate to default room (if one is listed):
                        foreach (string roomName in account.User.RoomNames)
                        {
                            // final sanity check
                            if (roomName.Contains("/room/"))
                                AddTabForRoom(account.GetCampfireRoomUri(roomName));
                        }
                        loadingCover.Visible = false;
                    }
                }
                else
                {
                    PreparePageHtml(browser);
                    loadingCover.Visible = false;
                    UpdateTitle(false, browser);
                    UpdateRoomList();
                    SaveOpenRooms();
                }

                timer.Interval = 10000;
                timer.Enabled = true;
            }
            catch (Exception err)
            {
                FlareException.ShowFriendly(err);
            }
        }

        private static void RestartFlare()
        {
            Process.Start("flare.exe");
            Process.GetCurrentProcess().Kill();
        }

        private void AddTabForNonRoom(Uri nonRoomUri)
        {
            AddTabForUri(nonRoomUri, "Loading...");
        }

        private void AddTabForRoom(Uri roomUri)
        {
            AddTabForUri(roomUri, "Entering room...");
        }

        private void AddTabForUri(Uri uri, string loadingText)
        {
            // 
            // tab page
            // 
            var newTabPage = new TabPage();
            newTabPage.Location = new Point(4, 22);
            newTabPage.Name = "lobbyTabPage";
            newTabPage.Size = new Size(976, 614);
            newTabPage.TabIndex = 0;
            newTabPage.Text = loadingText;
            newTabPage.UseVisualStyleBackColor = true;
            // 
            // web browser
            // 
            var newWebBrowser = new WebBrowser();
            newWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            newWebBrowser.IsWebBrowserContextMenuEnabled = false;
            newWebBrowser.Location = new System.Drawing.Point(0, 0);
            newWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            newWebBrowser.Name = "webBrowser";
            newWebBrowser.ScriptErrorsSuppressed = true;
            newWebBrowser.Size = new System.Drawing.Size(976, 614);
            newWebBrowser.TabIndex = 1;
            newWebBrowser.NewWindow += WebBrowserNewWindow;
            newWebBrowser.DocumentCompleted += webBrowser_DocumentCompleted;

            tabControl.TabPages.Add(newTabPage);
            newTabPage.Controls.Add(newWebBrowser);

            newWebBrowser.Navigate(uri);

            tabControl.SelectedTab = newTabPage;
        }

        private void UpdateRoomList()
        {
            try
            {
                int roomCount = 0;

                roomsToolStripMenuItem.DropDownItems.Clear();

                foreach (
                    MSHTML.HTMLAnchorElement link in ((MSHTML.IHTMLDocument2)lobbyWebBrowser.Document.DomDocument).anchors)
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
                            newToolStripMenuItem.ShortcutKeys = ((Keys)((Keys.Control | dKey)));
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

        private void PreparePageHtml(WebBrowser browser)
        {
            // Make sure the Campfire header (tab bar) is hidden
            if (lobbyWebBrowser.Document != null)
            {
                if (browser.Document.GetElementById("header") != null)
                    browser.Document.GetElementById("header").Style = "display: none;";
                if (browser.Document.GetElementById("open_bar") != null)
                    browser.Document.GetElementById("open_bar").Style = "display: none;";
                if (browser.Document.GetElementById("sidebar") != null)
                    browser.Document.GetElementById("sidebar").Style = "top: 5px;";
            }
        }

        private int oldNewMsgTotal = 0;

        private void UpdateTitle(bool checkForNewMessages, WebBrowser browser)
        {
            try
            {
                if (checkForNewMessages)
                    CheckForMessages(browser);

                int newMessagesTotal = (int) (browser.Parent.Tag ?? 0);
                
                if (browser.DocumentTitle.ToLower() == "chat rooms")
                {
                    browser.Parent.Text = " Lobby ";
                    return;
                }
                else if (browser.Url != null && (browser.Url.AbsoluteUri.Contains("/files+transcripts") ||
                         browser.Url.AbsoluteUri.Contains("/account/")))
                {
                    browser.Parent.Text = string.Format(" {0} ", browser.DocumentTitle.Replace("Campfire: ", ""));
                    return;
                }
                else
                {
                    roomTitle = browser.DocumentTitle.Replace("Campfire: ", "");
                    if (roomTitle != string.Empty && roomTitle[0] == '(')
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

                roomTitle = roomTitle.Trim();

                if (newMessagesTotal > 0)
                {
                    roomTitle = " (" + newMessagesTotal + ") " + FirstLetterToUpper(roomTitle) +
                                (roomTitle.Trim().EndsWith("room") ? " " : " room ");
                }
                else
                {
                    roomTitle = string.Format(" {0} {1}", FirstLetterToUpper(roomTitle),
                                              roomTitle.Trim().EndsWith("room") ? string.Empty : "room ");
                }

                if (browser.Parent.Text != roomTitle)
                    browser.Parent.Text = roomTitle;

            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                // Ignore updatetitle errors
            }
        }

        private void CheckForMessages(WebBrowser browser)
        {
            Message lastMessage = null;
            try
            {
                if (browser.Tag != null)
                    lastMessage = (Message)browser.Tag;

                // Don't check for messages if we're in the lobby:
                if (browser.Document != null && !browser.IsBusy && browser.Document.Url != null && browser.Document.Body != null &&
                    browser.Document.Url.AbsoluteUri.Contains("/room/"))
                {
                    // Don't do this if the form is focused.
                    if (!Focused || !browser.Focused)
                    {
                        if (lastMessage == null || lastMessage.ElementId.Length == 0)
                        {
                            try
                            {
                                foreach (HtmlElement table in browser.Document.Body.All)
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
                        }
                        else
                        {
                            // Find the last message's new element (it will change each time the html does:
                            HtmlElement lastElement = browser.Document.All[lastMessage.ElementId];

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
                                if (!((MSHTML.IHTMLElement)nextElement.DomElement).className.Contains(" you") &&
                                    (((MSHTML.IHTMLElement)nextElement.DomElement).className.Contains("text_message") ||
                                     ((MSHTML.IHTMLElement)nextElement.DomElement).className.Contains("enter_message") ||
                                     ((MSHTML.IHTMLElement)nextElement.DomElement).className.Contains("upload_message") ||
                                     ((MSHTML.IHTMLElement)nextElement.DomElement).className.Contains("paste_message")))
                                {
                                    if (!account.User.NotifyOnlyWhenNicknameIsFound ||
                                        lastMessage.TextMessage.ToLower().Contains(account.User.Nickname))
                                    {
                                        // Show the notification
                                        if (showMessageNotificationToolStripMenuItem.Checked)
                                        {
                                            var notifyForm = new NotifyForm(browser.Parent.Text, lastMessage.Name, lastMessage.TextMessage.Replace("View paste \r\n", string.Empty),
                                                                    this);
                                            notifyForm.ShowInactiveTopmost();
                                        }

                                        // Increase the unread message count for this tab
                                        browser.Parent.Tag = (int)(browser.Parent.Tag ?? 0) + 1;
                                    }
                                }

                                nextElement = nextElement.NextSibling;
                            }
                        }
                    }
                    else
                    {
                        browser.Parent.Tag = 0;

                        if (lastMessage != null && lastMessage.ElementId != null && lastMessage.ElementId.Length > 0)
                        {
                            HtmlElement nextElement = browser.Document.All[lastMessage.ElementId].NextSibling;
                            while (nextElement != null)
                            {
                                lastMessage = new Message("", "",
                                                           browser.Document.All[lastMessage.ElementId].NextSibling.
                                                               Id);
                                nextElement = nextElement.NextSibling;
                            }
                        }
                        else
                        {
                            try
                            {
                                foreach (HtmlElement table in browser.Document.Body.All)
                                    if (table.TagName.ToLower().Contains("table"))
                                        foreach (HtmlElement ele in table.All)
                                            if (((MSHTML.IHTMLElement)ele.DomElement).className != null &&
                                                ele.TagName.ToLower().Contains("tr") &&
                                                ((MSHTML.IHTMLElement)ele.DomElement).className.Contains("text_message"))
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
            catch (Exception e)
            {
                // TODO: stop try..catching such a huge block *and* using a catch-all :( 
                // Break this down and catch specific errors
                Debug.WriteLine(e);
            }
            finally
            {
                if (lastMessage != null)
                    browser.Tag = lastMessage;
            }


            Debug.WriteLine("new messages total: " + (browser.Parent.Tag ?? 0));
        }

        private static string FirstLetterToUpper(string inStr)
        {
            if (string.IsNullOrEmpty(inStr))
                return inStr;
            return inStr[0].ToString().ToUpper() + inStr.ToLower().Substring(1);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            try
            {
                var anyNewMessages = false;
                foreach (TabPage tabPage in tabControl.TabPages)
                {
                    var browser = (WebBrowser) tabPage.Controls[0];
                    if (browser.Document != null && browser.Document.Url != null && browser.Document.Url.AbsoluteUri.Contains("/room/"))
                    {
                        UpdateTitle(true, browser);
                        if ((int)(browser.Parent.Tag ?? 0) > 0)
                            anyNewMessages = true;
                    }
                }

                // If we have any new messages unread, set the correct notify icon and highlight
                // Update the notify icon:
                var resources = new ResourceManager(typeof(Resources));
                if (anyNewMessages && !Focused && !lobbyWebBrowser.Focused)
                {
                    // Update notify icon
                    notifyIcon.Icon = ((Icon)(resources.GetObject("newMsg")));

                    // Update toolbar icon to flash (if it's not already highlighted)
                    if (!iconIsHighlighted)
                    {
                        var f = new FLASHWINFO
                                    {
                                        CbSize = Marshal.SizeOf(typeof (FLASHWINFO)),
                                        Hwnd = Handle,
                                        DWFlags = FlashwTimernofg,
                                        UCount = 2,
                                        DWTimeout = 0
                                    };
                        FlashWindowEx(ref f);
                        iconIsHighlighted = true;
                    }
                }
                else
                {
                    iconIsHighlighted = false;
                    notifyIcon.Icon = ((Icon) (resources.GetObject("noNewMsgs")));
                }
            }
            catch
            {
                
            }
            finally
            {
                timer.Interval = 1000;
                timer.Start();
            }
        }

        private static bool iconIsHighlighted = false;

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Run closing routine:
            Application.Exit();
        }

        private void SaveOpenRooms()
        {
            account.User.RoomNames = new List<string>();
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                var browser = (WebBrowser)tabPage.Controls[0];
                if (browser.Document != null && browser.Document.Url != null &&
                    browser.Document.Url.AbsoluteUri.Contains("/room/"))
                {
                    account.User.RoomNames.Add(Regex.Match(browser.Document.Url.AbsoluteUri, ".*(/room/.*)").Groups[1].Value);
                }
            }
            account.Save();
        }

        private static void NewToolStripMenuItemClick(object sender, EventArgs e)
        {
            ((MSHTML.HTMLAnchorElement)((ToolStripMenuItem)sender).Tag).click();
        }

        private void changeSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var setupForm = new SetupForm();
            setupForm.ShowDialog();

            // if anything was changed, reload the page
            if (setupForm.NewAccountName != account.Name ||
                setupForm.NewUsername != account.User.Username ||
                setupForm.NewPassword != account.User.Password ||
                setupForm.NewNickname != account.User.Nickname ||
                setupForm.NewNotifyOnlyWhenNicknameIsFound != account.User.NotifyOnlyWhenNicknameIsFound ||
                setupForm.NewNotifyWindowDelay != account.User.NotifyWindowDelay ||
                setupForm.MinimiseInsteadOfQuitting != account.User.MinimiseInsteadOfQuitting)
            {
                for (int i = 1; i < tabControl.TabPages.Count; i++)
                {
                    tabControl.TabPages.RemoveAt(i);
                    i--;
                }
                Startup();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            forcedShutdown = true;
            Application.Exit();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // if the form has been resized to minimize the hide the form
            if (WindowState == FormWindowState.Minimized && Environment.OSVersion.Version.Major < 6)
                Minimise();
        }

        private void Minimise()
        {
            Hide();
            notifyIcon.Visible = true;
            notifyIcon.Text = Text;
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
            lobbyWebBrowser.Navigate(account.CampfireLoginUri);
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
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
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            account.User.UploadFileToCurrentRoom(account.CampfireUri + "/upload.cgi/room/36735/uploads/new", files[0],
                                                  lobbyWebBrowser.Document.Cookie, uploadLabel);
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

        private void WebBrowserNewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Process.Start(((WebBrowser)sender).StatusText);
        }

        #region Nested type: FLASHWINFO

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int CbSize;
            public IntPtr Hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public int DWFlags;
            [MarshalAs(UnmanagedType.U4)]
            public int UCount;
            [MarshalAs(UnmanagedType.U4)]
            public int DWTimeout;
        }

        #endregion

        private void tabPageCloseBtn_Click(object sender, EventArgs e)
        {
            var tabPage = tabControl.SelectedTab;
            tabControl.TabPages.Remove(tabPage);
            SaveOpenRooms();
            var browser = ((WebBrowser) tabPage.Controls[0]);
            // Leave the room
            if (browser.Document != null && browser.Document.Url != null &&
                browser.Document.Url.AbsoluteUri.Contains("/room/"))
            {
                ((MSHTML.HTMLAnchorElement) browser.Document.GetElementById("leave_link").FirstChild.DomElement).click();
            }
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.AbsoluteUri.Contains("/room/"))
            {
                // Open in a tab instead
                e.Cancel = true;
                AddTabForRoom(e.Url);
                SaveOpenRooms();
            }
        }

        private void filesTranscriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabForNonRoom(account.GetCampfireRoomUri("/files+transcripts"));
        }

        private void membersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabForNonRoom(account.GetCampfireRoomUri("/account/people"));
        }

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddTabForNonRoom(account.GetCampfireRoomUri("/account/settings"));
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabPageCloseBtn.Enabled = (tabControl.SelectedIndex != 0);
            tabControl.SelectedTab.Tag = 0;     // Reset our unread messages count
            UpdateTitle(true, (WebBrowser)tabControl.SelectedTab.Controls[0]);
            FocusInputBox();
        }

        private void FocusInputBox()
        {
            var browser = ((WebBrowser) tabControl.SelectedTab.Controls[0]);
            if (!isFirstLoad && browser.Document != null && !browser.IsBusy && browser.Document.Url.AbsoluteUri.Contains("/room/"))
                browser.Navigate("javascript:try{$('input').focus();void(0);}catch(e){}");
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            tabControl.SelectedTab.Tag = 0;     // Reset our unread messages count
            UpdateTitle(true, (WebBrowser)tabControl.SelectedTab.Controls[0]);
            tabControl.SelectedTab.Controls[0].Focus();
            FocusInputBox();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!forcedShutdown && account.User.MinimiseInsteadOfQuitting)
            {
                if (Environment.OSVersion.Version.Major < 6)    // if we're not on Vista or above:
                    Minimise();
                else
                    WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
        }

        private void makeADonationToFlareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=ADLAU6DWJQVRY");
        }
    }
}