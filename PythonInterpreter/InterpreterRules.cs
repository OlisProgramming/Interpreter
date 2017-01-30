using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter
{
    partial class Interpreter
    {
        /// <summary>
        /// expression: factor ((PLUS | MINUS) factor)*
        /// </summary>
        public void Expression()
        {
            Factor();

            while (
                CurrentToken.Type == Token.TokenType.PLUS ||
                CurrentToken.Type == Token.TokenType.MINUS
                )
            {
                switch (CurrentToken.Type)
                {
                    case Token.TokenType.PLUS:
                        Eat(Token.TokenType.PLUS);
                        Eat(Token.TokenType.INTEGER);
                        
                        break;

                    case Token.TokenType.MINUS:
                        Eat(Token.TokenType.MINUS);
                        Eat(Token.TokenType.INTEGER);

                        break;
                }
            }

            //Console.WriteLine(result);
        }

        /// <summary>
        /// factor: INTEGER
        /// </summary>
        public void Factor()
        {
            Eat(Token.TokenType.INTEGER);
        }
    }
}
