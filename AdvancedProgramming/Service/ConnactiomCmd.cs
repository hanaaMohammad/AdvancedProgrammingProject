using System;
using System.Diagnostics;

namespace AdvancedProgramming.Service
{
    public class ConnactiomCmd
    {



        public string call(string fileName, string arguments)
        {
            try
            {
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = fileName;
                processInfo.Arguments = arguments;
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardOutput = true;
                processInfo.RedirectStandardError = true;
                processInfo.CreateNoWindow = true;

                Process p = Process.Start(processInfo);
                string output = p.StandardOutput.ReadToEnd();
                string error = p.StandardError.ReadToEnd();
                p.WaitForExit();
                return output + error;
            }
            catch (Exception ex)
            {
                return "ERROR:" + ex.Message;
            }




        }




        public string callWithInput(string fileName, string arguments, string input)
        {
            try
            {
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = fileName;
                processInfo.Arguments = arguments;
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardOutput = true;
                processInfo.RedirectStandardError = true;
                processInfo.RedirectStandardInput = true;
                processInfo.CreateNoWindow = true;

                Process p = Process.Start(processInfo);
                p.StandardInput.Write(input);
                p.StandardInput.Close();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                return output;
            }
            catch (Exception ex)
            {
                return "RUNTIME_ERROR:" + ex.Message;
            }




        }





    }
}
