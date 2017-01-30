using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter
{
    partial class Interpreter
    {
        private List<Token> tokens;
        private int index = 0;

        public Interpreter(List<Token> tokens)
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
                Error();
            }
        }

        public void Error()
        {
            throw new Exception("Error interpreting tokens");
        }
    }
}
