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

            var context = new ApplicationContext();
            AppForm.AppContext = context;
            context.MainForm = new StartupForm { AnimateOnShow = false };
            Application.Run(context);
        }
    }
}
