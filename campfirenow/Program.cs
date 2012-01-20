using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Flare
{
    internal static class Program
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                bool createdNew = true;
                using (var mutex = new Mutex(true, "Flare", out createdNew))
                {
                    if (createdNew)
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new MainForm(args));
                    }
                    else
                    {
                        Process newInstanceOfFlare = Process.GetCurrentProcess();
                        foreach (Process firstInstanceOfFlare in Process.GetProcessesByName(newInstanceOfFlare.ProcessName))
                        {
                            if (firstInstanceOfFlare.Id != newInstanceOfFlare.Id)
                            {
                                SetForegroundWindow(firstInstanceOfFlare.MainWindowHandle);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                FlareException.ShowFriendly(err);
            }
        }
    }
}