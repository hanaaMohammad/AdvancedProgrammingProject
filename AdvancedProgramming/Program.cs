using System;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                DatabaseManager.InitializeDatabase();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name + ": " + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
            }
        }
    }
}
