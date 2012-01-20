using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Flare
{
   public partial class SetupDialog : Form
   {
      /*
       * TODO
       * Allow the user to set his nickname, if nickname not set default to Account name
       * Allow user to login
       */

      public Account Account
      {
         private set;
         get;
      }

      public SetupDialog( Account account )
      {
         Account = account;

         InitializeComponent();

         AcceptButton = _okButton;
         CancelButton = _cancelButton;
      }

      private void OkClicked(object sender, EventArgs e)
      {
         // Validation
         Int32 notifyWindowDelay;
         if (!Int32.TryParse(_notificationWindowDelayTextBox.Text, out notifyWindowDelay))
         {
            MessageBox.Show("The value you've entered for how long the notification window should display for isn't a whole number.\n\nPlease enter a whole number.",
                "Unable to save new notification window settings",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
         }

         Account.Name = _accountNameTextBox.Text;
         Account.UseSsl = _useSslCheckBox.Checked;

         if (Account.User == null)
            Account.User = new User();

         // TODO set token

         Account.User.MinimiseDuringStartup = _minimiseFlareOnStartupCheckBox.Checked;
         Account.User.MinimiseInsteadOfQuitting = _minimizeFlareWhenUserClosesWindowCheckBox.Checked;
         Account.User.Nickname = _nicknameTextBox.Text;
         Account.User.NotifyOnlyWhenNicknameIsFound = _alertOnNicknameCheckBox.Checked;
         Account.User.NotifyWindowDelay = notifyWindowDelay;

         Account.Save();

         SetStartupSituation(_startFlareOnStartUpCheckbox.Checked);

         DialogResult = DialogResult.OK;
      }

      private void CancelClicked(object sender, EventArgs e)
      {
         DialogResult = DialogResult.Cancel;
      }

      private void OnLoaded(object sender, EventArgs e)
      {
         if (Account != null)
         {
            _accountNameTextBox.Text = Account.Name;

            // display account name

            _nicknameTextBox.Text = Account.User.Nickname;
            _notificationWindowDelayTextBox.Text = Account.User.NotifyWindowDelay.ToString();
            _alertOnNicknameCheckBox.Checked = Account.User.NotifyOnlyWhenNicknameIsFound;
            _useSslCheckBox.Checked = Account.UseSsl;
            _minimiseFlareOnStartupCheckBox.Checked = Account.User.MinimiseDuringStartup;
            _startFlareOnStartUpCheckbox.Checked = GetStartupSituation();
            _minimizeFlareWhenUserClosesWindowCheckBox.Checked = Account.User.MinimiseInsteadOfQuitting;
         }
         else
         {
            Account = new Account();
         }
      }

      private static bool GetStartupSituation()
      {
         // If the user has an entry in the startup directory
         var runKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
         if (!string.IsNullOrEmpty(runKey.GetValue("Flare", string.Empty).ToString())) return true;

         // Or the user has an entry in the startup registry entry
         var startupShortcut = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Microsoft\Windows\Start Menu\Programs\Startup\Flare.lnk");
         if (File.Exists(startupShortcut)) return true;

         startupShortcut = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Flare.lnk");
         if (File.Exists(startupShortcut)) return true;

         return false;
      }

      private static void SetStartupSituation(bool startup)
      {
         var runKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
         if (startup)
            runKey.SetValue("Flare", Application.ExecutablePath);
         else if (runKey.GetValue("Flare", null) != null)
            runKey.DeleteValue("Flare");

         // Delete the older method of starting up if it's there
         var startupShortcut = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "Flare.lnk");
         if (File.Exists(startupShortcut))
            File.Delete(startupShortcut);

         startupShortcut = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Microsoft\Windows\Start Menu\Programs\Startup\Flare.lnk");
         if (File.Exists(startupShortcut))
            File.Delete(startupShortcut);
      }

      private void nicknameBox_TextChanged(object sender, EventArgs e)
      {
      }
   }
}