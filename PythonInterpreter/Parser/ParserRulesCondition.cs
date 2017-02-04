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

            return new IfNode(tk, arith, statement);
        }
    }
}
