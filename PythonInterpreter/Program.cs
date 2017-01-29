using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string text = Console.In.ReadLine();
                Interpreter interpreter = new Interpreter(text);
                interpreter.Expr();
            }
        }
    }
}
