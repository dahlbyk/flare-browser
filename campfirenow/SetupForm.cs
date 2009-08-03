using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
        public Boolean NewNotifyOnlyWhenNicknameIsFound { get; set; }
        public Int32 NewNotifyWindowDelay { get; set; }

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
            account.User.Username = usernameBox.Text;
            account.User.Password = passwordBox.Text;
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
                usernameBox.Text = account.User.Username;
                passwordBox.Text = account.User.Password;
                nicknameBox.Text = account.User.Nickname;
                notificationWindowDelayTextBox.Text = account.User.NotifyWindowDelay.ToString();
                nickNotifications.Checked = account.User.NotifyOnlyWhenNicknameIsFound;
                useSSL.Checked = account.UseSsl;

                NewAccountName = accountName.Text;
                NewUsername = usernameBox.Text;
                NewPassword = passwordBox.Text;
                NewNickname = nicknameBox.Text;
                NewNotifyOnlyWhenNicknameIsFound = nickNotifications.Checked;
            }
            else
                account = new Account();
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
    }
}