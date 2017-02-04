using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.TokeniserNamespace;

namespace PythonInterpreter.SyntaxTrees
{
    class UnaryPlusNode : UnaryOperationNode
    {
        public UnaryPlusNode(Token token, Node child) : base(token, child)
        {

        }
    }

    class UnaryMinusNode : UnaryOperationNode
    {
        public UnaryMinusNode(Token token, Node child) : base(token, child)
        {

        }
    }

    class AddNode : BinaryOperationNode
    {
        public AddNode(Token token, Node left, Node right) : base(token, left, right)
        {

        }
    }

    class SubNode : BinaryOperationNode
    {
        public SubNode(Token token, Node left, Node right) : base(token, left, right)
        {

        }
    }

    class MulNode : BinaryOperationNode
    {
        public MulNode(Token token, Node left, Node right) : base(token, left, right)
        {

        }
    }

    class DivNode : BinaryOperationNode
    {
        public DivNode(Token token, Node left, Node right) : base(token, left, right)
        {

        }
    }

    class LessThanNode : BinaryOperationNode
    {
        public LessThanNode(Token token, Node left, Node right) : base(token, left, right)
        {

        }
    }

    class GreaterThanNode : BinaryOperationNode
    {
        public GreaterThanNode(Token token, Node left, Node right) : base(token, left, right)
        {

        }
    }

    class LessThanOrEqualNode : BinaryOperationNode
    {
        public LessThanOrEqualNode(Token token, Node left, Node right) : base(token, left, right)
        {

        }
    }

    class GreaterThanOrEqualNode : BinaryOperationNode
    {
        public GreaterThanOrEqualNode(Token token, Node left, Node right) : base(token, left, right)
        {

        }
    }

    class EqualNode : BinaryOperationNode
    {
        public EqualNode(Token token, Node left, Node right) : base(token, left, right)
        {

        }
    }

    class NotEqualNode : BinaryOperationNode
    {
        public NotEqualNode(Token token, Node left, Node right) : base(token, left, right)
        {

        }
    }
}
