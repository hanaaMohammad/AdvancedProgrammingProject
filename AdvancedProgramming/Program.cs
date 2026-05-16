using System;
using System.Windows.Forms;
using AdvancedProgramming.Forms;

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

            var appContext = new ApplicationContext();
            AppForm.AppContext = appContext;
            appContext.MainForm = new StartupForm();
            Application.Run(appContext);
        }
    }
}
