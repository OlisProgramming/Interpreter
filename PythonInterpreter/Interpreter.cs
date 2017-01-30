using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter
{
    class Interpreter
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

        /// <summary>
        /// expression: factor ((PLUS | MINUS) factor)*
        /// </summary>
        public int Expression()
        {
            int result = Factor();

            while (
                tokens[index].Type == Token.TokenType.PLUS ||
                tokens[index].Type == Token.TokenType.MINUS
                )
            {
                switch (tokens[index].Type)
                {
                    case Token.TokenType.PLUS:
                        Eat(Token.TokenType.PLUS);
                        result += Factor();
                        
                        break;

                    case Token.TokenType.MINUS:
                        Eat(Token.TokenType.MINUS);
                        result -= Factor();

                        break;
                }
            }

            return result;
            //Console.WriteLine(result);
        }

        /// <summary>
        /// factor: INTEGER
        /// </summary>
        public int Factor()
        {
            Token integer = tokens[index];
            Eat(Token.TokenType.INTEGER);
            return Convert.ToInt32(integer.Value);
        }
    }
}
