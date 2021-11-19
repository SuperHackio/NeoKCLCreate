using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;

namespace NeoKCLCreate
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Prevents floats being written to the .dae with commas instead of periods on European systems.
            CultureInfo.CurrentCulture = new CultureInfo("", false);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
