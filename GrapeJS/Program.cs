using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GrapeJS
{
    class Program
    {


        static void Main(string[] args)
        {
            bool debug = false;
            while (true)
            {
                if (args[4] == "debug")
                {
                    debug = true;
                }

                if (args.Length < 4)
                    break;

                if (args[0] == "compile")
                {
                    if (!File.Exists(args[1]))
                    {
                        Console.WriteLine("File does not exist");
                        break;
                    }

                    if (args[2] != "to")
                    {
                        Console.WriteLine("Incorrect Syntax: Destination file not specified");
                        break;
                    }

                    string destFile = args[3];
                    string file = args[1];

                    if (debug)
                    {
                        SBenchmark.Start();
                    }

                    string template = "Default";

                    if (args[4] != "debug")
                        template = args[4];

                    if (Compiler.Compile(file, destFile,template))
                    {
                        if (debug)
                        {
                            SBenchmark.End();
                            if (SBenchmark.scores[SBenchmark.scores.Count - 1].TotalMilliseconds < 1000)
                            {
                                Console.WriteLine("Compilation Successful->" + destFile + " Time taken:" + SBenchmark.scores[SBenchmark.scores.Count - 1].TotalMilliseconds.ToString());
                            }
                            else
                            {
                                Console.WriteLine("Compilation Successful->" + destFile + " Time taken:" + SBenchmark.scores[SBenchmark.scores.Count - 1].TotalSeconds.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Compilation Successful->" + destFile);
                        }
                    }

                }
                break;
            }

            Console.ReadKey();
        }
    }
}
