﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.SyntaxTrees;
using PythonInterpreter.TokeniserNamespace;

namespace PythonInterpreter.ParserNamespace
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

            if (tokens[index].Type == Token.TokenType.LESS_THAN)
            {
                Eat(Token.TokenType.LESS_THAN);
                result = new LessThanNode(tokens[index - 1], result, ArithmeticExpression());
            }
            else if (tokens[index].Type == Token.TokenType.GREATER_THAN)
            {
                Eat(Token.TokenType.GREATER_THAN);
                result = new GreaterThanNode(tokens[index - 1], result, ArithmeticExpression());
            }
            else if (tokens[index].Type == Token.TokenType.LESS_OR_EQUAL)
            {
                Eat(Token.TokenType.LESS_OR_EQUAL);
                result = new LessThanOrEqualNode(tokens[index - 1], result, ArithmeticExpression());
            }
            else if (tokens[index].Type == Token.TokenType.GREATER_OR_EQUAL)
            {
                Eat(Token.TokenType.GREATER_OR_EQUAL);
                result = new GreaterThanOrEqualNode(tokens[index - 1], result, ArithmeticExpression());
            }
            else if (tokens[index].Type == Token.TokenType.EQUAL)
            {
                Eat(Token.TokenType.EQUAL);
                result = new EqualNode(tokens[index - 1], result, ArithmeticExpression());
            }
            else if (tokens[index].Type == Token.TokenType.NOT_EQUAL)
            {
                Eat(Token.TokenType.NOT_EQUAL);
                result = new NotEqualNode(tokens[index - 1], result, ArithmeticExpression());
            }

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
            else if (tokens[index].Type == Token.TokenType.IDENTIFIER)
            {
                node = Identifier();
            }
            else
            {
                node = Number();
            }

            while (tokens[index].Type == Token.TokenType.CAST)
            {
                Eat(Token.TokenType.CAST);
                string id = tokens[index].Value;
                Eat(Token.TokenType.IDENTIFIER);
                node = new CastNode(tokens[index-1], node, id);
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

            Eat(Token.TokenType.NUMBER);

            NumberNode node = new NumberNode(tk);

            return node;
        }
    }
}
