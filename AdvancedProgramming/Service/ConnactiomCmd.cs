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
        Process p;
      
        public ConnactiomCmd()
        {

            p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            call();  







        }

      


        public void call()
        {
            p.Start();
           

        }










        
        ProcessStartInfo procesInfo = new ProcessStartInfo();



















    }
}
