using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.Variables;
using PythonInterpreter.TokeniserNamespace;

namespace PythonInterpreter.SyntaxTrees
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

    class TernaryOperationNode : Node
    {
        public Node Left { get; set; }
        public Node Mid { get; set; }
        public Node Right { get; set; }

        public TernaryOperationNode(Token token, Node left, Node mid, Node right) : base(token)
        {
            Left = left;
            Mid = mid;
            Right = right;
        }

        public override string ToString()
        {
            return Token.Value + "{" + Left.ToString() + ", " + Mid.ToString() + ", " + Right.ToString() + "}";
        }
    }

    class NumberNode : Leaf
    {
        public Variable Value { get; set; }

        public NumberNode(Token token) : base(token)
        {
            Value = new VariableNumber(Convert.ToDouble(token.Value));
        }

        public override string ToString()
        {
            return Token.Value + "{" + Value.ToString() + "}";
        }
    }

    class IdentifierNode : Leaf
    {
        public string Value { get; set; }

        public IdentifierNode(Token token) : base(token)
        {
            Value = token.Value;
        }

        public override string ToString()
        {
            return Token.Value + "{" + Value + "}";
        }
    }
}
