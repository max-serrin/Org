using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            String s = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine(s);
            Console.ReadLine();
        }
    }
}
