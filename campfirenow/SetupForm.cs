using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Flare
{
    public partial class SetupForm : Form
    {
        public String NewUsername;
        public String NewPassword;
        public String NewAccountName;
        public String NewNickName;
        private Account _account;

        public SetupForm()
        {
            InitializeComponent();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            _account.Name = accountName.Text;
            _account.UseSsl = useSSL.Checked;

            if (_account.User == null)
                _account.User = new User();
            _account.User.Username = usernameBox.Text;
            _account.User.Password = passwordBox.Text;
            _account.User.Nickname = nicknameBox.Text;
            _account.User.NotifyOnlyWhenNicknameIsFound = nickNotifications.Checked;

            _account.Save();

            NewAccountName = accountName.Text;
            NewUsername = usernameBox.Text;
            NewPassword = passwordBox.Text;
            NewNickName = nicknameBox.Text;

            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
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
                nickNotifications.Checked = _account.User.NotifyOnlyWhenNicknameIsFound;
                useSSL.Checked = _account.UseSsl;

                NewAccountName = accountName.Text;
                NewUsername = usernameBox.Text;
                NewPassword = passwordBox.Text;
                NewNickName = nicknameBox.Text;
            }
        }
    }
}
