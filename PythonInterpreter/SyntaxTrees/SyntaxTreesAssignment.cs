using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.TokeniserNamespace;

namespace PythonInterpreter.SyntaxTrees
{
    /// <summary>
    /// Assigns value of Right to variable Left.
    /// </summary>
    class AssignNode : BinaryOperationNode
    {
        public AssignNode(Token token, Node left, Node right) : base(token, left, right)
        {

        }
    }

    class AssignLetNode : UnaryOperationNode
    {
        public AssignLetNode(Token token, Node assignNode) : base(token, assignNode)
        {

        }
    }
}
