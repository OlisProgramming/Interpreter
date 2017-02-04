using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.TokeniserNamespace;

namespace PythonInterpreter.ParserNamespace
{
    partial class Parser
    {
        private List<Token> tokens;
        private int index = 0;

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
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.PARSER_EXPECTED_DIFFERENT_TOKEN,
                    tokens[index],
                    type.ToString(), tokens[index].Type.ToString());
                //Error($"Token type {type} was expected, but got {tokens[index].Type} instead.", tokens[index]);
            }
        }
    }
}
