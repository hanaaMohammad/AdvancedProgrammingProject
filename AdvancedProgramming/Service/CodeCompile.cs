using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgramming.Service
{
    public class CodeCompile
    {

        public string Copmile(string path,string language)
        {
            if (language == "c++")
           
                return Execut("g++", path + " -o app.exe");
            else
                return Execut("javac", path);







        }

        private string Execut(string path , string args)
        {
            ConnactiomCmd cmd = new ConnactiomCmd();
            return cmd.call(path, args);

        }
    }







    }
