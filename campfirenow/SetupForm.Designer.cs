namespace campfirenow
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
            this.label1 = new System.Windows.Forms.Label();
            this.accountName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.useSSL = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Account Name: ";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // accountName
            // 
            this.accountName.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.accountName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountName.Location = new System.Drawing.Point(92, 12);
            this.accountName.Name = "accountName";
            this.accountName.Size = new System.Drawing.Size(95, 20);
            this.accountName.TabIndex = 0;
            this.accountName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SetupForm_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(185, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = ".campfirenow.com";
            this.label2.UseCompatibleTextRendering = true;
            // 
            // usernameBox
            // 
            this.usernameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameBox.Location = new System.Drawing.Point(92, 44);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(189, 20);
            this.usernameBox.TabIndex = 1;
            this.usernameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SetupForm_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Email:";
            this.label3.UseCompatibleTextRendering = true;
            // 
            // passwordBox
            // 
            this.passwordBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordBox.Location = new System.Drawing.Point(92, 76);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(189, 20);
            this.passwordBox.TabIndex = 2;
            this.passwordBox.UseSystemPasswordChar = true;
            this.passwordBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SetupForm_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Password:";
            this.label4.UseCompatibleTextRendering = true;
            // 
            // okBtn
            // 
            this.okBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okBtn.Location = new System.Drawing.Point(12, 140);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 3;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            this.okBtn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SetupForm_KeyDown);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelBtn.Location = new System.Drawing.Point(206, 140);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 4;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            this.cancelBtn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SetupForm_KeyDown);
            // 
            // useSSL
            // 
            this.useSSL.AutoSize = true;
            this.useSSL.Location = new System.Drawing.Point(92, 111);
            this.useSSL.Name = "useSSL";
            this.useSSL.Size = new System.Drawing.Size(153, 17);
            this.useSSL.TabIndex = 6;
            this.useSSL.Text = "Yes, my account uses SSL";
            this.useSSL.UseVisualStyleBackColor = true;
            this.useSSL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SetupForm_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Use SSL?:";
            this.label5.UseCompatibleTextRendering = true;
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 175);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.useSSL);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.usernameBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.accountName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setting Up...";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SetupForm_KeyDown);
            this.Load += new System.EventHandler(this.SetupForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox accountName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.CheckBox useSSL;
        private System.Windows.Forms.Label label5;
    }
}