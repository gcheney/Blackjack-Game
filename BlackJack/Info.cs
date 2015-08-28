using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;


namespace BlackJack
{
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
            string defaultBrowserPath = GetDefaultBrowserPath();
            string url = "http://www.pagat.com/banking/blackjack.html";
            try
            {
                // launch default browser with website url
                Process.Start(defaultBrowserPath, url);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private static string GetDefaultBrowserPath()
        {
            string key = @"htmlfile\shell\open\command";
            RegistryKey registryKey =
                Registry.ClassesRoot.OpenSubKey(key, false);
            // get default browser path
            return ((string)registryKey.GetValue(null, null)).Split('"')[1];
        }
    }
}
