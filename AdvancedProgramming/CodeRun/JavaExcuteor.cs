using System;
using System.Diagnostics;
using System.IO;

namespace AdvancedProgramming.CodeRun
{
    public class JavaExecutor : CodeExecutor
    {
        public string ExecuteCode(string code, string input)
        {
            try
            {
                string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempDir);
                string sourceFile = Path.Combine(tempDir, "Solution.java");
                File.WriteAllText(sourceFile, code);

                Process compileProcess = new Process();
                compileProcess.StartInfo.FileName = "javac";
                compileProcess.StartInfo.Arguments = $"\"{sourceFile}\" -d \"{tempDir}\"";
                compileProcess.StartInfo.UseShellExecute = false;
                compileProcess.StartInfo.RedirectStandardOutput = true;
                compileProcess.StartInfo.RedirectStandardError = true;
                compileProcess.StartInfo.CreateNoWindow = true;
                compileProcess.Start();
                string compileError = compileProcess.StandardError.ReadToEnd();
                compileProcess.WaitForExit();

                if (compileProcess.ExitCode != 0)
                    return "COMPILATION ERROR:\n" + compileError;

                Process runProcess = new Process();
                runProcess.StartInfo.FileName = "java";
                runProcess.StartInfo.Arguments = $"-cp \"{tempDir}\" Solution";
                runProcess.StartInfo.UseShellExecute = false;
                runProcess.StartInfo.RedirectStandardOutput = true;
                runProcess.StartInfo.RedirectStandardError = true;
                runProcess.StartInfo.RedirectStandardInput = true;
                runProcess.StartInfo.CreateNoWindow = true;
                runProcess.Start();
                if (!string.IsNullOrEmpty(input))
                {
                    runProcess.StandardInput.Write(input);
                }
                runProcess.StandardInput.Close();
                string output = runProcess.StandardOutput.ReadToEnd();
                string error = runProcess.StandardError.ReadToEnd();
                runProcess.WaitForExit();

                string result = output;
                if (!string.IsNullOrEmpty(error))
                    result += "\n" + error.Trim();
                return result;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("cannot find the file"))
                    return "RUNTIME_ERROR: Java compiler/runner not found. Install JDK.";
                return "RUNTIME_ERROR: " + ex.Message;
            }
        }
    }
}
