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

                if (p.ExitCode == 0)
                    return "";
                else
                    return (output + error).Trim();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("cannot find the file"))
                    return "ERROR: Compiler '" + fileName + "' not found. Use Java or install the compiler.";
                return "ERROR: " + ex.Message;
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
                string error = p.StandardError.ReadToEnd();
                p.WaitForExit();

                string result = output;
                if (!string.IsNullOrEmpty(error))
                    result += "\n" + error.Trim();
                return result;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("cannot find the file"))
                    return "RUNTIME_ERROR: Executable '" + fileName + "' not found. Compilation may have failed.";
                return "RUNTIME_ERROR: " + ex.Message;
            }
        }
    }
}
