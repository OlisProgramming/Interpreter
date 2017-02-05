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
        public Node AssignmentLetExpression()
        {
            Token tk = tokens[index];
            Eat(Token.TokenType.LET);
            Node node = AssignmentExpression();
            return new AssignLetNode(tk, node);
        }

        public Node AssignmentExpression()
        {
            Node node = Identifier();

            Eat(Token.TokenType.ASSIGN);

            return new AssignNode(tokens[index - 1], node, ArithmeticExpression());
        }

        public Node Identifier()
        {
            Token tk = tokens[index];
            Eat(Token.TokenType.IDENTIFIER);
            return new IdentifierNode(tk);
        }
    }
}
