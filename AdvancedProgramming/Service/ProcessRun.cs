using System;
using System.IO;

namespace AdvancedProgramming.Service
{
    public class ProcessRun
    {


        public string Run(string path, string language, string input)
        {
            ConnactiomCmd cmd = new ConnactiomCmd();

            if (language.ToLower() == "c++")
                return cmd.callWithInput(Path.GetFullPath("app.exe"), "", input);
            else
            {
                string className = Path.GetFileNameWithoutExtension(path);
                return cmd.callWithInput("java", className, input);

            }


        }




    }
}
