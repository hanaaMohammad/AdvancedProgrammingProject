using System;

namespace AdvancedProgramming.Service
{
    public class CodeCompile
    {

        public string Compile(string path, string language)
        {
            if (language.ToLower() == "c++")

                return Execut("g++", path + " -o app.exe");
            else
                return Execut("javac", path);



        }

        private string Execut(string fileName, string args)
        {
            ConnactiomCmd cmd = new ConnactiomCmd();
            return cmd.call(fileName, args);

        }





    }





}
