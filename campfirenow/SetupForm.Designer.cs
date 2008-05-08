namespace Flare
{
    partial class SetupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.accountTab = new System.Windows.Forms.TabPage();
            this.optionsTab = new System.Windows.Forms.TabPage();
            this.accountDetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.useSSL = new System.Windows.Forms.CheckBox();
            this.accountName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.accountNameLabel = new System.Windows.Forms.Label();
            this.sslSupportLabel = new System.Windows.Forms.Label();
            this.userDetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nicknameGroupBox = new System.Windows.Forms.GroupBox();
            this.nickNotifications = new System.Windows.Forms.CheckBox();
            this.nicknameBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.accountTab.SuspendLayout();
            this.optionsTab.SuspendLayout();
            this.accountDetailsGroupBox.SuspendLayout();
            this.userDetailsGroupBox.SuspendLayout();
            this.nicknameGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // okBtn
            // 
            this.okBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okBtn.Location = new System.Drawing.Point(197, 220);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 3;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelBtn.Location = new System.Drawing.Point(278, 220);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 4;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.accountTab);
            this.tabControl.Controls.Add(this.optionsTab);
            this.tabControl.Location = new System.Drawing.Point(4, 6);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(349, 208);
            this.tabControl.TabIndex = 11;
            // 
            // accountTab
            // 
            this.accountTab.Controls.Add(this.userDetailsGroupBox);
            this.accountTab.Controls.Add(this.accountDetailsGroupBox);
            this.accountTab.Location = new System.Drawing.Point(4, 22);
            this.accountTab.Name = "accountTab";
            this.accountTab.Padding = new System.Windows.Forms.Padding(3);
            this.accountTab.Size = new System.Drawing.Size(341, 182);
            this.accountTab.TabIndex = 0;
            this.accountTab.Text = "Account";
            this.accountTab.UseVisualStyleBackColor = true;
            // 
            // optionsTab
            // 
            this.optionsTab.Controls.Add(this.nicknameGroupBox);
            this.optionsTab.Location = new System.Drawing.Point(4, 22);
            this.optionsTab.Name = "optionsTab";
            this.optionsTab.Padding = new System.Windows.Forms.Padding(3);
            this.optionsTab.Size = new System.Drawing.Size(341, 182);
            this.optionsTab.TabIndex = 1;
            this.optionsTab.Text = "Notifications";
            this.optionsTab.UseVisualStyleBackColor = true;
            // 
            // accountDetailsGroupBox
            // 
            this.accountDetailsGroupBox.Controls.Add(this.sslSupportLabel);
            this.accountDetailsGroupBox.Controls.Add(this.accountNameLabel);
            this.accountDetailsGroupBox.Controls.Add(this.useSSL);
            this.accountDetailsGroupBox.Controls.Add(this.accountName);
            this.accountDetailsGroupBox.Controls.Add(this.label2);
            this.accountDetailsGroupBox.Location = new System.Drawing.Point(7, 7);
            this.accountDetailsGroupBox.Name = "accountDetailsGroupBox";
            this.accountDetailsGroupBox.Size = new System.Drawing.Size(328, 81);
            this.accountDetailsGroupBox.TabIndex = 14;
            this.accountDetailsGroupBox.TabStop = false;
            this.accountDetailsGroupBox.Text = "Your account";
            // 
            // useSSL
            // 
            this.useSSL.Location = new System.Drawing.Point(93, 50);
            this.useSSL.Name = "useSSL";
            this.useSSL.Size = new System.Drawing.Size(174, 21);
            this.useSSL.TabIndex = 17;
            this.useSSL.Text = "This account uses SSL";
            this.useSSL.UseVisualStyleBackColor = true;
            // 
            // accountName
            // 
            this.accountName.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.accountName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountName.Location = new System.Drawing.Point(93, 24);
            this.accountName.Name = "accountName";
            this.accountName.Size = new System.Drawing.Size(95, 20);
            this.accountName.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(188, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = ".campfirenow.com";
            this.label2.UseCompatibleTextRendering = true;
            // 
            // accountNameLabel
            // 
            this.accountNameLabel.AutoSize = true;
            this.accountNameLabel.Location = new System.Drawing.Point(7, 27);
            this.accountNameLabel.Name = "accountNameLabel";
            this.accountNameLabel.Size = new System.Drawing.Size(82, 13);
            this.accountNameLabel.TabIndex = 18;
            this.accountNameLabel.Text = "Account name: ";
            // 
            // sslSupportLabel
            // 
            this.sslSupportLabel.AutoSize = true;
            this.sslSupportLabel.Location = new System.Drawing.Point(7, 53);
            this.sslSupportLabel.Name = "sslSupportLabel";
            this.sslSupportLabel.Size = new System.Drawing.Size(68, 13);
            this.sslSupportLabel.TabIndex = 19;
            this.sslSupportLabel.Text = "SSL support:";
            // 
            // userDetailsGroupBox
            // 
            this.userDetailsGroupBox.Controls.Add(this.passwordBox);
            this.userDetailsGroupBox.Controls.Add(this.label4);
            this.userDetailsGroupBox.Controls.Add(this.usernameBox);
            this.userDetailsGroupBox.Controls.Add(this.label3);
            this.userDetailsGroupBox.Location = new System.Drawing.Point(7, 95);
            this.userDetailsGroupBox.Name = "userDetailsGroupBox";
            this.userDetailsGroupBox.Size = new System.Drawing.Size(328, 81);
            this.userDetailsGroupBox.TabIndex = 15;
            this.userDetailsGroupBox.TabStop = false;
            this.userDetailsGroupBox.Text = "Your details";
            // 
            // passwordBox
            // 
            this.passwordBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordBox.Location = new System.Drawing.Point(93, 45);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(130, 20);
            this.passwordBox.TabIndex = 14;
            this.passwordBox.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "Password:";
            this.label4.UseCompatibleTextRendering = true;
            // 
            // usernameBox
            // 
            this.usernameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameBox.Location = new System.Drawing.Point(93, 19);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(191, 20);
            this.usernameBox.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "Email:";
            this.label3.UseCompatibleTextRendering = true;
            // 
            // nicknameGroupBox
            // 
            this.nicknameGroupBox.Controls.Add(this.nickNotifications);
            this.nicknameGroupBox.Controls.Add(this.nicknameBox);
            this.nicknameGroupBox.Controls.Add(this.label6);
            this.nicknameGroupBox.Location = new System.Drawing.Point(7, 7);
            this.nicknameGroupBox.Name = "nicknameGroupBox";
            this.nicknameGroupBox.Size = new System.Drawing.Size(328, 92);
            this.nicknameGroupBox.TabIndex = 0;
            this.nicknameGroupBox.TabStop = false;
            this.nicknameGroupBox.Text = "Nickname";
            // 
            // nickNotifications
            // 
            this.nickNotifications.Checked = true;
            this.nickNotifications.CheckState = System.Windows.Forms.CheckState.Checked;
            this.nickNotifications.Location = new System.Drawing.Point(86, 52);
            this.nickNotifications.Name = "nickNotifications";
            this.nickNotifications.Size = new System.Drawing.Size(189, 33);
            this.nickNotifications.TabIndex = 13;
            this.nickNotifications.Text = "Alert me ONLY when someone uses my Nickname";
            this.nickNotifications.UseVisualStyleBackColor = true;
            // 
            // nicknameBox
            // 
            this.nicknameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nicknameBox.Location = new System.Drawing.Point(86, 26);
            this.nicknameBox.Name = "nicknameBox";
            this.nicknameBox.Size = new System.Drawing.Size(189, 20);
            this.nicknameBox.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Nickname:";
            this.label6.UseCompatibleTextRendering = true;
            // 
            // SetupForm
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(358, 249);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SetupForm_Load);
            this.tabControl.ResumeLayout(false);
            this.accountTab.ResumeLayout(false);
            this.optionsTab.ResumeLayout(false);
            this.accountDetailsGroupBox.ResumeLayout(false);
            this.accountDetailsGroupBox.PerformLayout();
            this.userDetailsGroupBox.ResumeLayout(false);
            this.userDetailsGroupBox.PerformLayout();
            this.nicknameGroupBox.ResumeLayout(false);
            this.nicknameGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage accountTab;
        private System.Windows.Forms.TabPage optionsTab;
        private System.Windows.Forms.GroupBox accountDetailsGroupBox;
        private System.Windows.Forms.CheckBox useSSL;
        private System.Windows.Forms.TextBox accountName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label sslSupportLabel;
        private System.Windows.Forms.Label accountNameLabel;
        private System.Windows.Forms.GroupBox userDetailsGroupBox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox nicknameGroupBox;
        private System.Windows.Forms.CheckBox nickNotifications;
        private System.Windows.Forms.TextBox nicknameBox;
        private System.Windows.Forms.Label label6;
    }
}
