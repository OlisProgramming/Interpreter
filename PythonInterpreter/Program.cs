using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.Variables;
using PythonInterpreter.TokeniserNamespace;
using PythonInterpreter.SyntaxTrees;
using PythonInterpreter.ParserNamespace;
using PythonInterpreter.InterpreterNamespace;

namespace PythonInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string text = System.IO.File.ReadAllText("../../programs/program.txt");

                Tokeniser tokeniser = new Tokeniser(text);
                List<Token> tokens = tokeniser.Tokenise();

                Console.WriteLine("TOKENS:");
                foreach (Token t in tokens)
                    Console.WriteLine(t);

                Parser parser = new Parser(tokens);
                Node expr = parser.Program();

                Console.WriteLine("ABSTRACT SYNTAX TREE:");
                Console.WriteLine(expr);

                Console.WriteLine("INITIALISING INTERPRETER AND RUNNING PROGRAM...");
                Console.WriteLine("-----------------------------------------------\n\n\n");

                Interpreter interpreter = new Interpreter();
                Variable result = interpreter.Visit(expr);
            }
            catch (InterpreterException ex)
            {
                Console.Error.WriteLine(ex);
            }

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
