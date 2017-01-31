using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter
{
    partial class Parser
    {
        public Node ArithmeticExpressionMulDiv()
        {
            Node node = ArithmeticUnit();

            while (
                tokens[index].Type == Token.TokenType.MUL ||
                tokens[index].Type == Token.TokenType.DIV
                )
            {
                Token tk = tokens[index];
                if (tk.Type == Token.TokenType.MUL)
                {
                    Eat(Token.TokenType.MUL);
                    node = new MulNode(
                        tk,
                        left: node,
                        right: ArithmeticUnit());
                }
                else if (tk.Type == Token.TokenType.DIV)
                {
                    Eat(Token.TokenType.DIV);
                    node = new DivNode(
                        tk,
                        left: node,
                        right: ArithmeticUnit());
                }
            }

            return node;
        }

        public Node ArithmeticExpressionAddSub()
        {
            Node node = ArithmeticExpressionMulDiv();

            while (
                tokens[index].Type == Token.TokenType.ADD ||
                tokens[index].Type == Token.TokenType.SUB
                )
            {
                Token tk = tokens[index];
                if (tk.Type == Token.TokenType.ADD)
                {
                    Eat(Token.TokenType.ADD);
                    node = new AddNode(
                        tk,
                        left: node,
                        right: ArithmeticExpressionMulDiv());
                }
                else if (tk.Type == Token.TokenType.SUB)
                {
                    Eat(Token.TokenType.SUB);
                    node = new SubNode(
                        tk,
                        left: node,
                        right: ArithmeticExpressionMulDiv());
                }
            }

            return node;
        }

        public Node ArithmeticExpression()
        {
            Node result = ArithmeticExpressionAddSub();

            return result;
        }

        public Node ArithmeticUnit()
        {
            Node node;
            if (tokens[index].Type == Token.TokenType.LPARENTH)
            {
                Eat(Token.TokenType.LPARENTH);
                node = ArithmeticExpression();
                Eat(Token.TokenType.RPARENTH);
            }
            else
            {
                node = Number();
            }
            return node;
        }

        public Node Number()
        {
            Token tk = tokens[index];

            if (tokens[index].Type == Token.TokenType.ADD)
            {
                Eat(Token.TokenType.ADD);
                return new UnaryPlusNode(tk, Number());
            }
            else if (tokens[index].Type == Token.TokenType.SUB)
            {
                Eat(Token.TokenType.SUB);
                return new UnaryMinusNode(tk, Number());
            }

            Eat(Token.TokenType.INTEGER);

            NumberNode node = new NumberNode(tk);

            return node;
        }
    }
}
