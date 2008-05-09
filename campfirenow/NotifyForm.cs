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
        MainForm _mainForm;
        Boolean _opening = true;

        public NotifyForm(string title, string person, string message, MainForm mainForm)
        {
            InitializeComponent();

            TitleLabel.Text = title;
            PersonLabel.Text = person;
            MessageLabel.Text = message;
            _mainForm = mainForm;
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
            if (_opening)
            {
                this.Opacity += 0.05;
                if (this.Opacity > 0.8)
                {
                    timer.Interval = _mainForm.Account.User.NotifyWindowDelay;
                    _opening = false;
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
            _mainForm.ShowFormHideIcon();
            this.Close();
        }
    }
}