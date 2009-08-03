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
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMessageNotificationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.changeSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lobbyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filesTranscriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.membersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.roomsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineSupportForumsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.CloseBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.waitingTimer = new System.Windows.Forms.Timer(this.components);
            this.loadingCover = new System.Windows.Forms.PictureBox();
            this.uploadPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.uploadLabel = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.lobbyTabPage = new System.Windows.Forms.TabPage();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.tabPageCloseBtn = new System.Windows.Forms.Button();
            this.autoUpdater = new Conversive.AutoUpdater.AutoUpdater();
            this.menuStrip1.SuspendLayout();
            this.notifyContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingCover)).BeginInit();
            this.uploadPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl.SuspendLayout();
            this.lobbyTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowDrop = true;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.lobbyToolStripMenuItem,
            this.roomsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMessageNotificationToolStripMenuItem,
            this.toolStripSeparator2,
            this.changeSettingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.settingsToolStripMenuItem.Text = "&File";
            // 
            // showMessageNotificationToolStripMenuItem
            // 
            this.showMessageNotificationToolStripMenuItem.Checked = true;
            this.showMessageNotificationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showMessageNotificationToolStripMenuItem.Name = "showMessageNotificationToolStripMenuItem";
            this.showMessageNotificationToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.showMessageNotificationToolStripMenuItem.Text = "Show Message Notification";
            this.showMessageNotificationToolStripMenuItem.Click += new System.EventHandler(this.showMessageNotificationToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(215, 6);
            // 
            // changeSettingsToolStripMenuItem
            // 
            this.changeSettingsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("changeSettingsToolStripMenuItem.Image")));
            this.changeSettingsToolStripMenuItem.Name = "changeSettingsToolStripMenuItem";
            this.changeSettingsToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.changeSettingsToolStripMenuItem.Text = "Change &Settings...";
            this.changeSettingsToolStripMenuItem.Click += new System.EventHandler(this.changeSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(215, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // lobbyToolStripMenuItem
            // 
            this.lobbyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesTranscriptsToolStripMenuItem,
            this.membersToolStripMenuItem,
            this.settingsToolStripMenuItem1});
            this.lobbyToolStripMenuItem.Name = "lobbyToolStripMenuItem";
            this.lobbyToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.lobbyToolStripMenuItem.Text = "&Lobby";
            // 
            // filesTranscriptsToolStripMenuItem
            // 
            this.filesTranscriptsToolStripMenuItem.Name = "filesTranscriptsToolStripMenuItem";
            this.filesTranscriptsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.filesTranscriptsToolStripMenuItem.Text = "Files && Transcripts";
            this.filesTranscriptsToolStripMenuItem.Click += new System.EventHandler(this.filesTranscriptsToolStripMenuItem_Click);
            // 
            // membersToolStripMenuItem
            // 
            this.membersToolStripMenuItem.Name = "membersToolStripMenuItem";
            this.membersToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.membersToolStripMenuItem.Text = "Members";
            this.membersToolStripMenuItem.Click += new System.EventHandler(this.membersToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem1
            // 
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.settingsToolStripMenuItem1.Text = "Settings";
            this.settingsToolStripMenuItem1.Click += new System.EventHandler(this.settingsToolStripMenuItem1_Click);
            // 
            // roomsToolStripMenuItem
            // 
            this.roomsToolStripMenuItem.Name = "roomsToolStripMenuItem";
            this.roomsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.roomsToolStripMenuItem.Text = "&Rooms";
            this.roomsToolStripMenuItem.Visible = false;
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
            this.notifyContextMenu.Size = new System.Drawing.Size(132, 54);
            // 
            // OpenBtn
            // 
            this.OpenBtn.Image = ((System.Drawing.Image)(resources.GetObject("OpenBtn.Image")));
            this.OpenBtn.Name = "OpenBtn";
            this.OpenBtn.Size = new System.Drawing.Size(131, 22);
            this.OpenBtn.Text = "Show Flare";
            this.OpenBtn.Click += new System.EventHandler(this.OpenBtn_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(128, 6);
            // 
            // CloseBtn
            // 
            this.CloseBtn.Image = ((System.Drawing.Image)(resources.GetObject("CloseBtn.Image")));
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(131, 22);
            this.CloseBtn.Text = "Exit";
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // waitingTimer
            // 
            this.waitingTimer.Interval = 1000;
            this.waitingTimer.Tick += new System.EventHandler(this.waitingTimer_Tick);
            // 
            // loadingCover
            // 
            this.loadingCover.BackColor = System.Drawing.Color.White;
            this.loadingCover.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadingCover.Image = global::Flare.Properties.Resources.indicator2;
            this.loadingCover.Location = new System.Drawing.Point(0, 24);
            this.loadingCover.Name = "loadingCover";
            this.loadingCover.Size = new System.Drawing.Size(984, 640);
            this.loadingCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loadingCover.TabIndex = 3;
            this.loadingCover.TabStop = false;
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
            this.lobbyTabPage.Controls.Add(this.webBrowser);
            this.lobbyTabPage.Location = new System.Drawing.Point(4, 26);
            this.lobbyTabPage.Name = "lobbyTabPage";
            this.lobbyTabPage.Size = new System.Drawing.Size(976, 610);
            this.lobbyTabPage.TabIndex = 0;
            this.lobbyTabPage.Text = " Lobby ";
            this.lobbyTabPage.UseVisualStyleBackColor = true;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(976, 610);
            this.webBrowser.TabIndex = 1;
            this.webBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser_Navigating);
            this.webBrowser.NewWindow += new System.ComponentModel.CancelEventHandler(this.WebBrowserNewWindow);
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
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
            this.autoUpdater.ConfigUrl = "http://mattbrindley.com/campfirewin/updates.xml";
            this.autoUpdater.DownloadForm = null;
            this.autoUpdater.LoginUserName = null;
            this.autoUpdater.LoginUserPass = null;
            this.autoUpdater.ProxyUrl = null;
            this.autoUpdater.RestartForm = null;
            this.autoUpdater.OnAutoUpdateComplete += new Conversive.AutoUpdater.AutoUpdater.AutoUpdateComplete(this.AutoUpdaterOnAutoUpdateComplete);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 664);
            this.Controls.Add(this.loadingCover);
            this.Controls.Add(this.tabPageCloseBtn);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.uploadPanel);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Starting...";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragLeave += new System.EventHandler(this.MainForm_DragLeave);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.MainForm_DragOver);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.notifyContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loadingCover)).EndInit();
            this.uploadPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.lobbyTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem roomsToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem showMessageNotificationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private Conversive.AutoUpdater.AutoUpdater autoUpdater;
        private System.Windows.Forms.Timer waitingTimer;
        private System.Windows.Forms.ContextMenuStrip notifyContextMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem CloseBtn;
        private System.Windows.Forms.ToolStripMenuItem onlineSupportForumsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.PictureBox loadingCover;
        private System.Windows.Forms.Panel uploadPanel;
        private System.Windows.Forms.Label uploadLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage lobbyTabPage;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Button tabPageCloseBtn;
        private System.Windows.Forms.ToolStripMenuItem lobbyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filesTranscriptsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem membersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
    }
}

