using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Flare
{
    public partial class SetupForm : Form
    {
        private Account account;
        private bool nicknameHasBeenManuallySet;

        public SetupForm()
        {
            InitializeComponent();
        }

        public String NewUsername { get; set; }
        public String NewPassword { get; set; }
        public String NewAccountName { get; set; }
        public String NewNickname { get; set; }
        public bool NewNotifyOnlyWhenNicknameIsFound { get; set; }
        public Int32 NewNotifyWindowDelay { get; set; }
        public bool MinimiseInsteadOfQuitting { get; set; }

        private void okBtn_Click(object sender, EventArgs e)
        {
            // Validation
            Int32 notifyWindowDelay;
            if (!Int32.TryParse(notificationWindowDelayTextBox.Text, out notifyWindowDelay))
            {
                MessageBox.Show("The value you've entered for how long the notification window should display for is invalid.\n\nPlease enter a whole number.", 
                    "Unable to save new notification window settings",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            account.Name = accountName.Text;
            account.UseSsl = useSSL.Checked;

            if (account.User == null)
                account.User = new User();
            if (openIdCheckBox.Checked)
            {
                account.User.UseOpenId = true;
                account.User.OpenIdUrl = usernameBox.Text;
            }
            else
            {
                account.User.UseOpenId = false;
                account.User.Username = usernameBox.Text;
                account.User.Password = passwordBox.Text;
            }
            account.User.MinimiseDuringStartup = minimiseAtStartupCheckBox.Checked;
            account.User.MinimiseInsteadOfQuitting = dontQuitCheckBox.Checked;
            account.User.Nickname = nicknameBox.Text;
            account.User.NotifyOnlyWhenNicknameIsFound = nickNotifications.Checked;
            account.User.NotifyWindowDelay = notifyWindowDelay;

            account.Save();

            NewAccountName = accountName.Text;
            NewUsername = usernameBox.Text;
            NewPassword = passwordBox.Text;
            NewNickname = nicknameBox.Text;
            NewNotifyOnlyWhenNicknameIsFound = nickNotifications.Checked;
            NewNotifyWindowDelay = notifyWindowDelay;
            MinimiseInsteadOfQuitting = account.User.MinimiseInsteadOfQuitting;

            SetStartupSituation(startUpCheckbox.Checked);

            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetupForm_Load(object sender, EventArgs e)
        {
            account = Account.FromRegistry();

            if (account != null)
            {
                accountName.Text = account.Name;
                if (account.User.UseOpenId)
                {
                    openIdCheckBox.Checked = true;
                    usernameBox.Text = account.User.OpenIdUrl;
                }
                else
                {
                    usernameBox.Text = account.User.Username;
                    passwordBox.Text = account.User.Password;
                }
                nicknameBox.Text = account.User.Nickname;
                notificationWindowDelayTextBox.Text = account.User.NotifyWindowDelay.ToString();
                nickNotifications.Checked = account.User.NotifyOnlyWhenNicknameIsFound;
                useSSL.Checked = account.UseSsl;
                minimiseAtStartupCheckBox.Checked = account.User.MinimiseDuringStartup;
                startUpCheckbox.Checked = GetStartupSituation();
                dontQuitCheckBox.Checked = account.User.MinimiseInsteadOfQuitting;

                NewAccountName = accountName.Text;
                NewUsername = usernameBox.Text;
                NewPassword = passwordBox.Text;
                NewNickname = nicknameBox.Text;
                NewNotifyOnlyWhenNicknameIsFound = nickNotifications.Checked;
            }
            else
                account = new Account();
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

        private void usernameBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nicknameBox.Text) || !nicknameHasBeenManuallySet)
            {
                nicknameBox.Text = Regex.Replace(usernameBox.Text, @"(\@.*)", "");
                nicknameHasBeenManuallySet = false;
            }
        }

        private void nicknameBox_TextChanged(object sender, EventArgs e)
        {
            nicknameHasBeenManuallySet = true;
        }

        private void openIdCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (openIdCheckBox.Checked)
            {
                passwordBox.Enabled = false;
                passwordLabel.Enabled = false;
                usernameLabel.Text = "OpenID:";
            }
            else
            {
                passwordBox.Enabled = true;
                passwordLabel.Enabled = true;
                usernameLabel.Text = "Username:";
            }
        }
    }
}