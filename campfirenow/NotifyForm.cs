using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Flare
{
    public partial class NotifyForm : Form
    {
        MainForm pMainFormRef;

        bool opening = true;

        public NotifyForm(string title, string person, string message, MainForm mainFormRef)
        {
            InitializeComponent();

            TitleLabel.Text = title;
            PersonLabel.Text = person;
            MessageLabel.Text = message;
            pMainFormRef = mainFormRef;
            this.Opacity = 0.0;
        }

        private void NotifyForm_Load(object sender, EventArgs e)
        {
            // Position it:
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            timer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (opening)
            {
                this.Opacity += 0.05;
                if (this.Opacity > 0.8)
                {
                    timer.Interval = 1500;
                    opening = false;
                }
            }
            else
            {
                timer.Interval = 50;
                this.Opacity -= 0.05;
                if (this.Opacity <= 0.5)
                {
                    this.Close();
                }
            }
        }

        private void NotifyForm_MouseClick(object sender, MouseEventArgs e)
        {
            pMainFormRef.ShowFormHideIcon();
            this.Close();
        }
    }
}