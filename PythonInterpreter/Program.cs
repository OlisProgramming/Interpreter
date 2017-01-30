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
                Tokeniser tokeniser = new Tokeniser(text);
                List<Token> tokens = tokeniser.Tokenise();

                foreach (Token t in tokens)
                    Console.WriteLine(t);

                Interpreter interpreter = new Interpreter(tokens);
                Console.WriteLine(interpreter.Expression());
            }
        }
    }
}
