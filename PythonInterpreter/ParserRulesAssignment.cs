using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter
{
    partial class Parser
    {
        public Node AssignmentExpression()
        {
            Node node = Identifier();

            Eat(Token.TokenType.ASSIGN);

            Node arith = ArithmeticExpression();

            return new AssignNode(tokens[index], node, arith);
        }

        public Node Identifier()
        {
            Token tk = tokens[index];
            Eat(Token.TokenType.IDENTIFIER);
            return new IdentifierNode(tk);
        }
    }
}
