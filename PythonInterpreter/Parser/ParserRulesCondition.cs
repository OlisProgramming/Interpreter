using System;
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
        public Node IfStatement()
        {
            Token tk = tokens[index];

            Eat(Token.TokenType.IF);
            Node arith = ArithmeticExpression();

            Node statement = StatementBlock();

            Node alternative;

            if (tokens[index].Type == Token.TokenType.ELSE)
            {
                Eat(Token.TokenType.ELSE);
                alternative = StatementBlock();
            }
            else
            {
                alternative = new StatementListNode(tk);
            }

            return new IfNode(tk, arith, statement, alternative);
        }
    }
}
