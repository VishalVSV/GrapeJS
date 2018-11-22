using System;
using System.Collections.Generic;
using System.IO;

namespace GrapeJS
{
    public static class Extensions
    {
        public static string Last(this string[] array)
        {
            return array[array.Length - 1];
        }
    }

    public static class Compiler
    {
        private static List<GrapeFunction> Functions = new List<GrapeFunction>();
        private static Dictionary<string, string> Templates = new Dictionary<string, string>();

        static Compiler()
        {
            string[] FunctionFiles = Directory.GetFiles("./Definitions/Functions");

            for (int i = 0; i < FunctionFiles.Length; i++)
            {
                if (FunctionFiles[i].Split('.').Last() == "func")
                    Functions.Add(new GrapeFunction(FunctionFiles[i]));
            }

            Templates.Add("Default", "<html><body><script>{%OutputCode%}</script></body></html>");
            string[] TemplatesFiles = Directory.GetFiles("./Definitions/Templates");
            for (int i = 0; i < TemplatesFiles.Length; i++)
            {
                using (StreamReader sr = new StreamReader(TemplatesFiles[i]))
                {
                    while (!sr.EndOfStream)
                    {
                        string li = sr.ReadLine();
                        if (li.Split(' ')[0] == "N")
                        {
                            string template = sr.ReadToEnd();
                            Templates.Add(li.Substring(1).Trim(),template);
                        }
                    }
                }
            }
        }

        public static bool Compile(string fileName, string destFile, string Template = "Default")
        {
            string OutputCode = "";

            using (StreamReader sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    for (int i = 0; i < Functions.Count; i++)
                    {
                        if (line.Length - Functions[i].FunctionName.Length > 0)
                        {
                            string h = line.Substring(0, Functions[i].FunctionName.Length);
                            if (h == Functions[0].FunctionName)
                            {
                                string toS = line.Substring(line.IndexOf('(') + 1, line.LastIndexOf(')') - line.IndexOf('(') - 1);
                                string[] pars = toS.Split(',');

                                OutputCode += Functions[i].ToJS(pars);
                            }
                        }
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(destFile))
            {
                sw.Write(Templates[Template].Replace("{%OutputCode%}",OutputCode));
            }

            return true;
        }
    }
}
