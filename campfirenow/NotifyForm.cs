using System;
using System.Windows.Forms;

namespace Flare
{
    public partial class NotifyForm : Form
    {
        private MainForm _mainForm;
        private Boolean _opening = true;

        public NotifyForm(string title, string person, string message, MainForm mainForm)
        {
            InitializeComponent();

            TitleLabel.Text = title;
            PersonLabel.Text = person;
            MessageLabel.Text = message;
            _mainForm = mainForm;
            Opacity = 0.0;
        }

        private void NotifyForm_Load(object sender, EventArgs e)
        {
            // Position it:
            Left = Screen.PrimaryScreen.WorkingArea.Width - Width;
            Top = Screen.PrimaryScreen.WorkingArea.Height - Height;
            timer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (_opening)
            {
                Opacity += 0.05;
                if (Opacity > 0.8)
                {
                    timer.Interval = _mainForm.Account.User.NotifyWindowDelay;
                    _opening = false;
                }
            }
            else
            {
                timer.Interval = 50;
                Opacity -= 0.05;
                if (Opacity <= 0.5)
                {
                    Close();
                }
            }
        }

        private void NotifyForm_MouseClick(object sender, MouseEventArgs e)
        {
            _mainForm.ShowFormHideIcon();
            Close();
        }
    }
}