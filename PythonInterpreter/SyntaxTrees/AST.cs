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

    //////////////

    /// <summary>
    /// Assigns value of Right to variable Left.
    /// </summary>
    class AssignNode : BinaryOperationNode
    {
        public AssignNode(Token token, Node left, Node right) : base(token, left, right)
        {

        }
    }

    //////////////

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

    //////////////

    class ProgramNode : Node
    {
        private List<Node> statements;
        public List<Node> Statements { get { return statements; } }

        public ProgramNode(Token token) : base(token)
        {
            statements = new List<Node>();
        }

        public void AddStatement(Node statement)
        {
            Statements.Add(statement);
        }

        public override string ToString()
        {
            string val = "{\n";

            foreach (Node statement in Statements)
            {
                val += statement + "\n";
            }

            return val + "}";
        }
    }

    class PrintNode : UnaryOperationNode
    {
        public PrintNode(Token token, Node child) : base(token, child)
        {

        }
    }

    class CastNode : UnaryOperationNode
    {
        public string TypeToCast { get; set; }

        public CastNode(Token token, Node child, string id) : base(token, child)
        {
            TypeToCast = id;
        }
    }
}
