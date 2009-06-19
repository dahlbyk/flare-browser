using System;
using System.Windows.Forms;

namespace Flare
{
    public partial class SetupForm : Form
    {
        private Account _account;

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
                MessageBox.Show(
                    String.Format(
                        "The value you've entered for how long the notification window show display for ({0}), is invalid.\n\nPlease enter a whole number.",
                        notificationWindowDelayTextBox.Text), "Unable to save new notification window settings",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _account.Name = accountName.Text;
            _account.UseSsl = useSSL.Checked;

            if (_account.User == null)
                _account.User = new User();
            _account.User.Username = usernameBox.Text;
            _account.User.Password = passwordBox.Text;
            _account.User.Nickname = nicknameBox.Text;
            _account.User.NotifyOnlyWhenNicknameIsFound = nickNotifications.Checked;
            _account.User.NotifyWindowDelay = notifyWindowDelay;

            _account.Save();

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
            _account = Account.FromRegistry();

            if (_account != null)
            {
                accountName.Text = _account.Name;
                usernameBox.Text = _account.User.Username;
                passwordBox.Text = _account.User.Password;
                nicknameBox.Text = _account.User.Nickname;
                notificationWindowDelayTextBox.Text = _account.User.NotifyWindowDelay.ToString();
                nickNotifications.Checked = _account.User.NotifyOnlyWhenNicknameIsFound;
                useSSL.Checked = _account.UseSsl;

                NewAccountName = accountName.Text;
                NewUsername = usernameBox.Text;
                NewPassword = passwordBox.Text;
                NewNickname = nicknameBox.Text;
                NewNotifyOnlyWhenNicknameIsFound = nickNotifications.Checked;
            }
        }
    }
}