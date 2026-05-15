using System;
using System.IO;

namespace AdvancedProgramming.Service
{
    public class FileManger
    {

        public string Path(string code , string Languge)
        {
            string path = Languge == "c++" ? "code.cpp" : "code.java";
           File.WriteAllText(path, code);
            return path;


        }










    }
}
