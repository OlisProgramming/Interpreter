using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter
{
    partial class Parser
    {
        private List<Token> tokens;
        private int index = 0;
        private InterpreterEnvironment env;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public void Eat(Token.TokenType type)
        {
            if (tokens[index].Type == type)
            {
                index++;
            }
            else
            {
                Error($"Token type {type} was expected, but got {tokens[index].Type} instead.", tokens[index]);
            }
        }

        public void Error(string message, Token token_at)
        {
            Console.WriteLine("Error while interpreting.\n" + message);
            Console.In.ReadLine();
            Environment.Exit(1);
        }
    }
}
