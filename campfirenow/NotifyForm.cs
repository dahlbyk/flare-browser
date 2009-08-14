using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Flare
{
    public partial class NotifyForm : Form
    {
        private readonly MainForm mainForm;
        private Boolean opening = true;

        private const int WsEXNoactivate = 0x08000000;
        private const int WsEXToolwindow = 0x00000080;
                

        public NotifyForm(string title, string person, string message, MainForm mainForm)
        {
            InitializeComponent();

            TitleLabel.Text = title;
            PersonLabel.Text = person;
            MessageLabel.Text = message;
            this.mainForm = mainForm;
            Opacity = 0.0;
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        private const int SW_SHOWNOACTIVATE = 4;
        private const int HWND_TOPMOST = -1;
        private const uint SWP_NOACTIVATE = 0x0010;

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        static extern bool SetWindowPos(
             int hWnd,           // window handle
             int hWndInsertAfter,    // placement-order handle
             int X,          // horizontal position
             int Y,          // vertical position
             int cx,         // width
             int cy,         // height
             uint uFlags);       // window positioning flags

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public void ShowInactiveTopmost()
        {
            ShowWindow(Handle, SW_SHOWNOACTIVATE);
            SetWindowPos(Handle.ToInt32(), HWND_TOPMOST, Left, Top, Width, Height, SWP_NOACTIVATE);
            NotifyForm_Load(this, new EventArgs());
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
            if (opening)
            {
                Opacity += 0.05;
                if (Opacity > 0.8)
                {
                    timer.Interval = mainForm.Account.User.NotifyWindowDelay;
                    opening = false;
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
            mainForm.ShowFormHideIcon();
            Close();
        }
    }
}