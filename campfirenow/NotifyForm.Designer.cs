namespace Flare
{
    partial class NotifyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyForm));
            this.TitleLabel = new System.Windows.Forms.Label();
            this.PersonLabel = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.MessageLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.TitleLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.ForeColor = System.Drawing.Color.White;
            this.TitleLabel.Location = new System.Drawing.Point(2, 2);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(361, 24);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "(2) Salted >> Talk";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TitleLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyForm_MouseClick);
            // 
            // PersonLabel
            // 
            this.PersonLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PersonLabel.ForeColor = System.Drawing.Color.White;
            this.PersonLabel.Location = new System.Drawing.Point(5, 28);
            this.PersonLabel.Name = "PersonLabel";
            this.PersonLabel.Size = new System.Drawing.Size(358, 64);
            this.PersonLabel.TabIndex = 1;
            this.PersonLabel.Text = "Paul";
            this.PersonLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyForm_MouseClick);
            // 
            // timer
            // 
            this.timer.Interval = 50;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // MessageLabel
            // 
            this.MessageLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageLabel.ForeColor = System.Drawing.Color.White;
            this.MessageLabel.Location = new System.Drawing.Point(5, 45);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(358, 47);
            this.MessageLabel.TabIndex = 2;
            this.MessageLabel.Text = "Yeah, I agree - it\'s pretty awesome!";
            this.MessageLabel.Click += new System.EventHandler(this.NotifyForm_Load);
            this.MessageLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyForm_MouseClick);
            // 
            // NotifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(366, 101);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.PersonLabel);
            this.Controls.Add(this.TitleLabel);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NotifyForm";
            this.Opacity = 0.4;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "NotifyForm";
            this.TopMost = true;
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyForm_MouseClick);
            this.Load += new System.EventHandler(this.NotifyForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label PersonLabel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label MessageLabel;
    }
}