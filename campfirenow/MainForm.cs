using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Flare
{
   public partial class MainForm : Form
   {
      // TODO LIST
      /*
       * Flash icon for new messages
       * Display Notifications when messages come in
       * When User selects tab, focus the input box
       * 
       */

      public MainForm(string[] args)
      {
         InitializeComponent();

         // isInStartUpMode = (args.Length > 0 && args[0] == "-startup");
      }

      public Account Account
      {
         get;
         set;
      }

      public String ProgressLabelText { get; set; }

      private void MainForm_Load(object sender, EventArgs e)
      {
         Startup();

         if (Account.User.MinimiseDuringStartup)
            WindowState = FormWindowState.Minimized;

         // Check for any Flare updates
         autoUpdater.TryUpdate();
      }

      private void Startup()
      {
         try
         {
            Account = Account.FromRegistry();
            
            if (Account == null)
            {
               // Show the modal dialog to fill these details
               var setupForm = new SetupDialog( null );
               setupForm.ShowDialog();

               // Try again to retreive the details from the registry
               Account = Account.FromRegistry();
            }

            // If the Account is still null, the user cancelled the dialog, the expected behaviour here is to quit
            if ( Account == null )
            {
               Application.Exit();
            }
            else
            {
               _fileNotificationsCheckboxMenuItem.Checked = Account.User.ShowMessageNotifications;

               // Does the user use a proxy server?
               IWebProxy proxy = WebRequest.DefaultWebProxy;
               if ( !proxy.IsBypassed( Account.CampfireUri ) )
               {
                  autoUpdater.ProxyEnabled = true;
                  autoUpdater.ProxyURL = proxy.GetProxy( Account.CampfireUri ).AbsoluteUri;
               }

               Text = string.Format( "{0} - Flare", Account.Name );

               // TODO Load the Lobby
            }
         }
         catch (Exception err)
         {
            FlareException.ShowFriendly(err);
         }
      }

      private void SaveOpenRooms()
      {
         Account.User.RoomNames = new List<string>();
         foreach (TabPage tabPage in tabControl.TabPages)
         {
            var browser = (WebBrowser)tabPage.Controls[0];
            if (browser.Document != null && browser.Document.Url != null &&
                browser.Document.Url.AbsoluteUri.Contains("/room/"))
            {
               Account.User.RoomNames.Add(Regex.Match(browser.Document.Url.AbsoluteUri, ".*(/room/.*)").Groups[1].Value);
            }
         }
         Account.Save();
      }

      private void changeSettingsToolStripMenuItem_Click(object sender, EventArgs e)
      {
         var setupForm = new SetupDialog(Account.FromRegistry());

         // if anything was changed, reload the page
         // TODO Check if anything changed
         if (setupForm.ShowDialog() == DialogResult.OK)
         {
            for (int i = 1; i < tabControl.TabPages.Count; i++)
            {
               tabControl.TabPages.RemoveAt(i);
               i--;
            }
            Startup();
         }
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

      private void notifyIcon_DoubleClick(object sender, EventArgs e)
      {
         ShowWindow();
      }

      public void ShowWindow()
      {
         Show();
         WindowState = FormWindowState.Normal;
         notifyIcon.Visible = false;
         TopMost = true;
         TopMost = false;
      }

      private void showMessageNotificationToolStripMenuItem_Click(object sender, EventArgs e)
      {
         if (_fileNotificationsCheckboxMenuItem.Checked)
         {
            _fileNotificationsCheckboxMenuItem.Checked = false;

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
            _fileNotificationsCheckboxMenuItem.Checked = true;

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

      private void OpenBtn_Click(object sender, EventArgs e)
      {
         ShowWindow();
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

      private void tabPageCloseBtn_Click(object sender, EventArgs e)
      {
         var tabPage = tabControl.SelectedTab;
         tabControl.TabPages.Remove(tabPage);
         SaveOpenRooms();
      }

      private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
      {

      }

      private void MainForm_Activated(object sender, EventArgs e)
      {

      }

      private void makeADonationToFlareToolStripMenuItem_Click(object sender, EventArgs e)
      {
         Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=ADLAU6DWJQVRY");
      }

      private bool _userWantsToQuit;

      private void exitToolStripMenuItem_Click( object sender, EventArgs e )
      {
         UserWantsToQuit();
      }

      private void CloseBtn_Click( object sender, EventArgs e )
      {
         UserWantsToQuit();
      }

      private void UserWantsToQuit()
      {
         _userWantsToQuit = true;
         Application.Exit();
      }

      private void MainForm_FormClosing( object sender, FormClosingEventArgs e )
      {
         if ( !_userWantsToQuit && Account.User.MinimiseInsteadOfQuitting )
         {
            if ( Environment.OSVersion.Version.Major < 6 )    // if we're not on Vista or above:
               Minimise();
            else
               WindowState = FormWindowState.Minimized;
            e.Cancel = true;
         }
      }
   }
}