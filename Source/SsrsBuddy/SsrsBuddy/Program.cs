using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SsrsBuddy
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Reploy.Form1());
            //the ssrsbuddy splash screen has been discarded, go straight to deployment tool
        }
    }
}