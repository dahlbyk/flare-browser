using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace campfirenow
{
    public partial class SetupForm : Form
    {
        public string newUsername;
        public string newPassword;
        public string newAccountName;

        public SetupForm()
        {
            InitializeComponent();
        }

        /*
        private void guestCheck_CheckedChanged(object sender, EventArgs e)
        {
                usernameBox.Enabled = !guestCheck.Checked;
                passwordBox.Enabled = !guestCheck.Checked;
        }
        */

        private void okBtn_Click(object sender, EventArgs e)
        {
            /*
            if (guestCheck.Checked)
            {
                MessageBox.Show("Sorry, logging in as a guest is not yet supported.");
                return;
            }
            */

            // Attempt to open the key
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Flare", true);

            // If the return value is null, the key doesn't exist
            if (key == null)
            {
                // The key doesn't exist; create it / open it
                key = Registry.CurrentUser.CreateSubKey("Software\\Flare");
            }

            key.SetValue("accountname", accountName.Text);
            /*
            if (guestCheck.Checked)
            {
                key.SetValue("loginAsGuest", "1");
                key.SetValue("username", "");
                key.SetValue("password", "");
            }
            else
            {
            */
            key.SetValue("loginAsGuest", "0");
            key.SetValue("username", usernameBox.Text);
            key.SetValue("password", passwordBox.Text);
            key.SetValue("defaultroom", "notset");
            if (useSSL.Checked)
            {
                key.SetValue("usessl", "1");
            }
            else
            {
                key.SetValue("usessl", "0");
            }
                //}
            newAccountName = accountName.Text;
            newUsername = usernameBox.Text;
            newPassword = passwordBox.Text;

            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetupForm_Load(object sender, EventArgs e)
        {
            // Get the existing info from the database:
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
            }
            else
            {
                accountName.Text = key.GetValue("accountname").ToString();
                //guestCheck.Checked = (key.GetValue("loginAsGuest").ToString() == "1");
                usernameBox.Text = key.GetValue("username").ToString();
                passwordBox.Text = key.GetValue("password").ToString();
                try
                {
                    useSSL.Checked = (key.GetValue("usessl", "0").ToString() == "1");
                }
                catch
                {
                    useSSL.Checked = false;
                }

                newAccountName = accountName.Text;
                newUsername = usernameBox.Text;
                newPassword = passwordBox.Text;
            }
        }

        private void SetupForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}