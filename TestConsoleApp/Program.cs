using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestConsoleApp
{
    class Program
    {
        static readonly int OUTPUT_LENGTH = 1024;
        static void Main(string[] args)
        {
            for (int i = 0; i < OUTPUT_LENGTH; ++i)
                Console.WriteLine(i + "This is an output.");

            for (int i = 0; i < OUTPUT_LENGTH; ++i)
            {
                Console.Error.WriteLine(i + "This is an error.");
                Thread.Sleep(10);
            }
        }
    }
}
