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

        public double ArithmeticExpressionMulDiv()
        {
            double result = ArithmeticUnit();

            while (
                tokens[index].Type == Token.TokenType.MUL ||
                tokens[index].Type == Token.TokenType.DIV
                )
            {
                switch (tokens[index].Type)
                {
                    case Token.TokenType.MUL:
                        Eat(Token.TokenType.MUL);
                        result *= ArithmeticUnit();
                        
                        break;

                    case Token.TokenType.DIV:
                        Eat(Token.TokenType.DIV);
                        result /= ArithmeticUnit();

                        break;
                }
            }

            return result;
        }

        public double ArithmeticExpressionPlusMinus()
        {
            double result = ArithmeticExpressionMulDiv();

            while (
                tokens[index].Type == Token.TokenType.PLUS ||
                tokens[index].Type == Token.TokenType.MINUS
                )
            {
                switch (tokens[index].Type)
                {
                    case Token.TokenType.PLUS:
                        Eat(Token.TokenType.PLUS);
                        result += ArithmeticExpressionMulDiv();

                        break;

                    case Token.TokenType.MINUS:
                        Eat(Token.TokenType.MINUS);
                        result -= ArithmeticExpressionMulDiv();

                        break;
                }
            }

            return result;
        }

        public double ArithmeticExpression()
        {
            double result = ArithmeticExpressionPlusMinus();

            return result;
        }

        public double ArithmeticUnit()
        {
            double result;
            if (tokens[index].Type == Token.TokenType.LPARENTH)
            {
                Eat(Token.TokenType.LPARENTH);
                result = ArithmeticExpression();
                Eat(Token.TokenType.RPARENTH);
            }
            else
            {
                result = Number();
            }
            return result;
        }

        public double Number()
        {
            bool signIsPos = true;

            if (tokens[index].Type == Token.TokenType.PLUS)
            {
                signIsPos = true;
                index++;
            }
            else if (tokens[index].Type == Token.TokenType.MINUS)
            {
                signIsPos = false;
                index++;
            }

            Token integer = tokens[index];
            Eat(Token.TokenType.INTEGER);
            return (signIsPos? 1 : -1) * Convert.ToDouble(integer.Value);
        }
    }
}
