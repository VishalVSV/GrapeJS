using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
namespace GrapeJS
{
    public class GrapeFunction
    {
        private Dictionary<int, string> variables = new Dictionary<int, string>();
        private string JSCode = "";

        public string FunctionName = "";

        public GrapeFunction(string filename)
        {
            int pNum = 0;

            FunctionName = filename.Substring(0, filename.Length - filename.LastIndexOf('.'));

            if (!File.Exists(filename))
                throw new FileNotFoundException();
            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    string l = sr.ReadLine();
                    IList<string> line = l.Split(' ');

                    if (line[0] == "I")
                    {
                        variables.Add(pNum, line[1]);
                        pNum++;
                        continue;
                    }

                    if (line[0] == "JS")
                    {
                        JSCode += l.Substring(line[0].Length).Trim() + Environment.NewLine;
                        continue;
                    }

                    if (line[0] == "N")
                    {
                        FunctionName = l.Substring(line[0].Length).Trim();
                        continue;
                    }
                }
            }
        }

        public string ToJS(params string[] parameters)
        {
            string resJS = "";
            foreach (int id in variables.Keys)
            {
                if (JSCode.Contains(variables[id]))
                {
                    resJS = JSCode.Replace('%'+variables[id], parameters[id]);
                }
            }

            return resJS;
        }
    }
}
