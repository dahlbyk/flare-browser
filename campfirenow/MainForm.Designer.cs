namespace Flare
{
    partial class MainForm
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
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
         this._menuStrip = new System.Windows.Forms.MenuStrip();
         this._fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this._fileNotificationsCheckboxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
         this._fileChangeSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this._fileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.onlineSupportForumsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
         this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
         this.roomsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.makeADonationToFlareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
         this.notifyContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.OpenBtn = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
         this.CloseBtn = new System.Windows.Forms.ToolStripMenuItem();
         this.uploadPanel = new System.Windows.Forms.Panel();
         this.pictureBox1 = new System.Windows.Forms.PictureBox();
         this.uploadLabel = new System.Windows.Forms.Label();
         this.tabControl = new System.Windows.Forms.TabControl();
         this.lobbyTabPage = new System.Windows.Forms.TabPage();
         this.tabPageCloseBtn = new System.Windows.Forms.Button();
         this.autoUpdater = new Conversive.AutoUpdater.AutoUpdater();
         this._menuStrip.SuspendLayout();
         this.notifyContextMenu.SuspendLayout();
         this.uploadPanel.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
         this.tabControl.SuspendLayout();
         this.SuspendLayout();
         // 
         // _menuStrip
         // 
         this._menuStrip.AllowDrop = true;
         this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileMenuItem,
            this.aboutToolStripMenuItem,
            this.roomsToolStripMenuItem,
            this.donateToolStripMenuItem});
         this._menuStrip.Location = new System.Drawing.Point(0, 0);
         this._menuStrip.Name = "_menuStrip";
         this._menuStrip.Size = new System.Drawing.Size(984, 24);
         this._menuStrip.TabIndex = 1;
         this._menuStrip.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
         // 
         // _fileMenuItem
         // 
         this._fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileNotificationsCheckboxMenuItem,
            this.toolStripSeparator2,
            this._fileChangeSettingsMenuItem,
            this.toolStripSeparator1,
            this._fileExitMenuItem});
         this._fileMenuItem.Name = "_fileSettingsMenuItem";
         this._fileMenuItem.Size = new System.Drawing.Size(37, 20);
         this._fileMenuItem.Text = "&File";
         // 
         // _fileNotificationsCheckboxMenuItem
         // 
         this._fileNotificationsCheckboxMenuItem.Checked = true;
         this._fileNotificationsCheckboxMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
         this._fileNotificationsCheckboxMenuItem.Name = "showMessageNotificationToolStripMenuItem";
         this._fileNotificationsCheckboxMenuItem.Size = new System.Drawing.Size(218, 22);
         this._fileNotificationsCheckboxMenuItem.Text = "Show Message Notification";
         this._fileNotificationsCheckboxMenuItem.Click += new System.EventHandler(this.showMessageNotificationToolStripMenuItem_Click);
         // 
         // toolStripSeparator2
         // 
         this.toolStripSeparator2.Name = "toolStripSeparator2";
         this.toolStripSeparator2.Size = new System.Drawing.Size(215, 6);
         // 
         // _fileChangeSettingsMenuItem
         // 
         this._fileChangeSettingsMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_fileChangeSettingsMenuItem.Image")));
         this._fileChangeSettingsMenuItem.Name = "changeSettingsToolStripMenuItem";
         this._fileChangeSettingsMenuItem.Size = new System.Drawing.Size(218, 22);
         this._fileChangeSettingsMenuItem.Text = "Change &Settings...";
         this._fileChangeSettingsMenuItem.Click += new System.EventHandler(this.changeSettingsToolStripMenuItem_Click);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(215, 6);
         // 
         // _fileExitMenuItem
         // 
         this._fileExitMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_fileExitMenuItem.Image")));
         this._fileExitMenuItem.Name = "exitToolStripMenuItem";
         this._fileExitMenuItem.Size = new System.Drawing.Size(218, 22);
         this._fileExitMenuItem.Text = "E&xit";
         this._fileExitMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
         // 
         // aboutToolStripMenuItem
         // 
         this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineSupportForumsToolStripMenuItem,
            this.toolStripSeparator4,
            this.aboutToolStripMenuItem1});
         this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
         this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
         this.aboutToolStripMenuItem.Text = "&About";
         // 
         // onlineSupportForumsToolStripMenuItem
         // 
         this.onlineSupportForumsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("onlineSupportForumsToolStripMenuItem.Image")));
         this.onlineSupportForumsToolStripMenuItem.Name = "onlineSupportForumsToolStripMenuItem";
         this.onlineSupportForumsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
         this.onlineSupportForumsToolStripMenuItem.Text = "Online Support Forums...";
         this.onlineSupportForumsToolStripMenuItem.Click += new System.EventHandler(this.onlineSupportForumsToolStripMenuItem_Click);
         // 
         // toolStripSeparator4
         // 
         this.toolStripSeparator4.Name = "toolStripSeparator4";
         this.toolStripSeparator4.Size = new System.Drawing.Size(203, 6);
         // 
         // aboutToolStripMenuItem1
         // 
         this.aboutToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem1.Image")));
         this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
         this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
         this.aboutToolStripMenuItem1.Text = "&About...";
         this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
         // 
         // roomsToolStripMenuItem
         // 
         this.roomsToolStripMenuItem.Name = "roomsToolStripMenuItem";
         this.roomsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
         this.roomsToolStripMenuItem.Text = "&Rooms";
         this.roomsToolStripMenuItem.Visible = false;
         // 
         // donateToolStripMenuItem
         // 
         this.donateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.makeADonationToFlareToolStripMenuItem});
         this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
         this.donateToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
         this.donateToolStripMenuItem.Text = "Donate";
         // 
         // makeADonationToFlareToolStripMenuItem
         // 
         this.makeADonationToFlareToolStripMenuItem.Name = "makeADonationToFlareToolStripMenuItem";
         this.makeADonationToFlareToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
         this.makeADonationToFlareToolStripMenuItem.Text = "Make a donation to Flare";
         this.makeADonationToFlareToolStripMenuItem.Click += new System.EventHandler(this.makeADonationToFlareToolStripMenuItem_Click);
         // 
         // notifyIcon
         // 
         this.notifyIcon.ContextMenuStrip = this.notifyContextMenu;
         this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
         this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
         // 
         // notifyContextMenu
         // 
         this.notifyContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenBtn,
            this.toolStripSeparator3,
            this.CloseBtn});
         this.notifyContextMenu.Name = "notifyContextMenu";
         this.notifyContextMenu.Size = new System.Drawing.Size(153, 76);
         // 
         // OpenBtn
         // 
         this.OpenBtn.Image = ((System.Drawing.Image)(resources.GetObject("OpenBtn.Image")));
         this.OpenBtn.Name = "OpenBtn";
         this.OpenBtn.Size = new System.Drawing.Size(152, 22);
         this.OpenBtn.Text = "Show Flare";
         this.OpenBtn.Click += new System.EventHandler(this.OpenBtn_Click);
         // 
         // toolStripSeparator3
         // 
         this.toolStripSeparator3.Name = "toolStripSeparator3";
         this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
         // 
         // CloseBtn
         // 
         this.CloseBtn.Image = ((System.Drawing.Image)(resources.GetObject("CloseBtn.Image")));
         this.CloseBtn.Name = "CloseBtn";
         this.CloseBtn.Size = new System.Drawing.Size(152, 22);
         this.CloseBtn.Text = "Exit";
         this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
         // 
         // uploadPanel
         // 
         this.uploadPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
         this.uploadPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.uploadPanel.Controls.Add(this.pictureBox1);
         this.uploadPanel.Controls.Add(this.uploadLabel);
         this.uploadPanel.Location = new System.Drawing.Point(689, 588);
         this.uploadPanel.Name = "uploadPanel";
         this.uploadPanel.Size = new System.Drawing.Size(168, 58);
         this.uploadPanel.TabIndex = 4;
         this.uploadPanel.Visible = false;
         // 
         // pictureBox1
         // 
         this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
         this.pictureBox1.Location = new System.Drawing.Point(15, 16);
         this.pictureBox1.Name = "pictureBox1";
         this.pictureBox1.Size = new System.Drawing.Size(31, 29);
         this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
         this.pictureBox1.TabIndex = 1;
         this.pictureBox1.TabStop = false;
         // 
         // uploadLabel
         // 
         this.uploadLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.uploadLabel.Location = new System.Drawing.Point(52, 7);
         this.uploadLabel.Name = "uploadLabel";
         this.uploadLabel.Size = new System.Drawing.Size(100, 47);
         this.uploadLabel.TabIndex = 0;
         this.uploadLabel.Text = "To upload a file, drop it here.";
         this.uploadLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // tabControl
         // 
         this.tabControl.Controls.Add(this.lobbyTabPage);
         this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tabControl.Location = new System.Drawing.Point(0, 24);
         this.tabControl.Margin = new System.Windows.Forms.Padding(10);
         this.tabControl.Name = "tabControl";
         this.tabControl.Padding = new System.Drawing.Point(5, 5);
         this.tabControl.SelectedIndex = 0;
         this.tabControl.Size = new System.Drawing.Size(984, 640);
         this.tabControl.TabIndex = 5;
         this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
         // 
         // lobbyTabPage
         // 
         this.lobbyTabPage.Location = new System.Drawing.Point(4, 26);
         this.lobbyTabPage.Name = "lobbyTabPage";
         this.lobbyTabPage.Size = new System.Drawing.Size(976, 610);
         this.lobbyTabPage.TabIndex = 0;
         this.lobbyTabPage.Text = " Lobby ";
         this.lobbyTabPage.UseVisualStyleBackColor = true;
         // 
         // tabPageCloseBtn
         // 
         this.tabPageCloseBtn.Enabled = false;
         this.tabPageCloseBtn.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.tabPageCloseBtn.Location = new System.Drawing.Point(962, 26);
         this.tabPageCloseBtn.Name = "tabPageCloseBtn";
         this.tabPageCloseBtn.Size = new System.Drawing.Size(21, 20);
         this.tabPageCloseBtn.TabIndex = 6;
         this.tabPageCloseBtn.Text = "X";
         this.tabPageCloseBtn.UseVisualStyleBackColor = true;
         this.tabPageCloseBtn.Click += new System.EventHandler(this.tabPageCloseBtn_Click);
         // 
         // autoUpdater
         // 
         this.autoUpdater.AutoDownload = false;
         this.autoUpdater.AutoRestart = true;
         this.autoUpdater.ConfigURL = "http://mattbrindley.com/campfirewin/updates.xml";
         this.autoUpdater.DownloadForm = null;
         this.autoUpdater.LoginUserName = null;
         this.autoUpdater.LoginUserPass = null;
         this.autoUpdater.ProxyURL = null;
         this.autoUpdater.RestartForm = null;
         this.autoUpdater.OnAutoUpdateComplete += new Conversive.AutoUpdater.AutoUpdater.AutoUpdateComplete(this.AutoUpdaterOnAutoUpdateComplete);
         // 
         // MainForm
         // 
         this.AllowDrop = true;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(984, 664);
         this.Controls.Add(this.tabPageCloseBtn);
         this.Controls.Add(this.tabControl);
         this.Controls.Add(this.uploadPanel);
         this.Controls.Add(this._menuStrip);
         this.DoubleBuffered = true;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MainMenuStrip = this._menuStrip;
         this.Name = "MainForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Starting...";
         this.Activated += new System.EventHandler(this.MainForm_Activated);
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
         this.Load += new System.EventHandler(this.MainForm_Load);
         this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
         this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
         this.DragOver += new System.Windows.Forms.DragEventHandler(this.MainForm_DragOver);
         this.DragLeave += new System.EventHandler(this.MainForm_DragLeave);
         this.Resize += new System.EventHandler(this.MainForm_Resize);
         this._menuStrip.ResumeLayout(false);
         this._menuStrip.PerformLayout();
         this.notifyContextMenu.ResumeLayout(false);
         this.uploadPanel.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
         this.tabControl.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.ToolStripMenuItem _fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _fileChangeSettingsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem _fileExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem roomsToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem _fileNotificationsCheckboxMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private Conversive.AutoUpdater.AutoUpdater autoUpdater;
        private System.Windows.Forms.ContextMenuStrip notifyContextMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem CloseBtn;
        private System.Windows.Forms.ToolStripMenuItem onlineSupportForumsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Panel uploadPanel;
        private System.Windows.Forms.Label uploadLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage lobbyTabPage;
        private System.Windows.Forms.Button tabPageCloseBtn;
        private System.Windows.Forms.ToolStripMenuItem donateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeADonationToFlareToolStripMenuItem;
    }
}

