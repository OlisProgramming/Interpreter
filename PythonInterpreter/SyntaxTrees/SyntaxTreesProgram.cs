using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.TokeniserNamespace;

namespace PythonInterpreter.SyntaxTrees
{
    class StatementListNode : Node
    {
        private List<Node> statements;
        public List<Node> Statements { get { return statements; } }

        public StatementListNode(Token token) : base(token)
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

    class IfNode : TernaryOperationNode
    {
        public IfNode(Token token, Node condition, Node statement, Node alternative) : base(token, condition, statement, alternative)
        {

        }
    }
}
