using System;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DatabaseManager.InitializeDatabase();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
