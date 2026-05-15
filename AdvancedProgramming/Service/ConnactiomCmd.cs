using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace AdvancedProgramming.Service
{
    public class ConnactiomCmd
    {
        ProcessStartInfo processInfo ;






        public string call(string fileName, string arguments)
        {
           procesInfo = new ProcessStartInfo();
            procesInfo.FileName = fileName;
            procesInfo.Arguments = arguments;
            procesInfo.UseShellExecute = false;
            procesInfo.RedirectStandardOutput = true;
            procesInfo.RedirectStandardError = true;
            procesInfo.CreateNoWindow = true;

            Process p = Process.Start(procesInfo);
            string error = p.StandardError.ReadToEnd();
            p.WaitForExit();
            return error;




        }










        
        ProcessStartInfo procesInfo = new ProcessStartInfo();



















    }
}
