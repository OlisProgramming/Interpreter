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

                Console.WriteLine("TOKENS:");
                foreach (Token t in tokens)
                    Console.WriteLine(t);

                Parser parser = new Parser(tokens);
                Node expr = parser.ArithmeticExpression();

                Console.WriteLine("ABSTRACT SYNTAX TREE:");
                Console.WriteLine(expr);

                Interpreter interpreter = new Interpreter();
                double result = interpreter.Visit(expr);

                Console.WriteLine("VALUE OF TREE:");
                Console.WriteLine(result);
            }
        }
    }
}
