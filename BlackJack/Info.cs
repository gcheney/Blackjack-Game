using System;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;


namespace BlackJack
{
    /// <summary>
    /// Opens a new web browser to display information
    /// related to the game of Blackjack
    /// </summary>
    public partial class Info : Form
    {
        /// <summary>
        /// Initializes a new Info form
        /// </summary>
        public Info()
        {
            InitializeComponent();
            var defaultBrowserPath = GetDefaultBrowserPath();
            var url = "http://www.pagat.com/banking/blackjack.html";
            try
            {
                // launch users default browser with website url
                Process.Start(defaultBrowserPath, url);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private static string GetDefaultBrowserPath()
        {
            var key = @"htmlfile\shell\open\command";
            RegistryKey registryKey 
                = Registry.ClassesRoot.OpenSubKey(key, false);
            // get default browser path
            return ((string)registryKey.GetValue(null, null)).Split('"')[1];
        }
    }
}
