using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Ambiesoft.gui4cmd
{
    static class Program
    {
        static string app_ = string.Empty;
        static string args_ = string.Empty;

        public static string App { get { return app_; } }
        public static string Args { get { return args_; } }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            app_ = "TestConsoleApp.exe";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            (new FormMain()).ShowDialog();
        }
    }
}
