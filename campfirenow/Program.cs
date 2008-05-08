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
                // Show generic error message
                MessageBox.Show("Flare has misunderstood a response from Campfire and needs to close. If you would like to help make this beta software better, post the error message below on the CampfireNow support forum at mattbrindley.com/support.\n\nThanks for your patience.\n\nCampfireWin Exception Details:\nProduct Version: " + Application.ProductVersion + "\n" + err.Message + "\n\n");
            }
        }
    }
}