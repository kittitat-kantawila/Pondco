using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pond_s_Shop
{
    static class Program
    {
        private static Form MainWindow;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainWindow = new Login();
            Application.Run(MainWindow);
        }
        public static void closeProgram()
        {
            Application.Exit();
        }
    }
}
