using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter
{
    /// <summary>
    /// An abstract syntax tree, or a node of one.
    /// </summary>
    class Node
    {
        public Token Token { get; set; }

        public Node(Token token)
        {
            Token = token;
        }
    }

    class Leaf : Node
    {
        public Leaf(Token token) : base(token)
        {

        }
    }

    //////////////

    class UnaryOperationNode : Node
    {
        public Node Child { get; set; }

        public UnaryOperationNode(Token token, Node child) : base(token)
        {
            Child = child;
        }

        public override string ToString()
        {
            return Token.Value + "{" + Child.ToString() + "}";
        }
    }

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

    //////////////

    class BinaryOperationNode : Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }

        public BinaryOperationNode(Token token, Node left, Node right) : base(token)
        {
            Left = left;
            Right = right;
        }

        public override string ToString()
        {
            return Token.Value + "{" + Left.ToString() + ", " + Right.ToString() + "}";
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

    class NumberNode : Leaf
    {
        public double Value { get; set; }

        public NumberNode(Token token) : base(token)
        {
            Value = Convert.ToDouble(token.Value);
        }

        public override string ToString()
        {
            return Token.Value + "{" + Value.ToString() + "}";
        }
    }
}
