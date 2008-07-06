using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Flare
{
    [Serializable]
    public class FlareException : Exception
    {
        public static void ShowFriendly(Exception e)
        {
            MessageBox.Show(String.Format("Flare has misunderstood a request from Campfire and needs to close. If you would like to help make this beta software better, post the error message below on the Flare support forum at http://code.google.com/p/flare-browser/issues/list.\n\nThanks for your patience.\n\nFlare Exception Details:\nProduct Version: {0}\n{1}\n\n{2}", Application.ProductVersion, e.Message, e.StackTrace));
        }
    }
}
