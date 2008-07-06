using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Flare
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(args));
            }
            catch (System.Exception err)
            {
                FlareException.ShowFriendly(err);
            }
        }
    }
}