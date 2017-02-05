using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.Variables;
using PythonInterpreter.SyntaxTrees;
using PythonInterpreter.Exceptions;

namespace PythonInterpreter.InterpreterNamespace
{
    partial class Interpreter
    {
        private Variable VisitAssignNode(AssignNode node, Frame frame)
        {
            Variable v = Visit(node.Right, frame.Next(node.Right.Token));
            env.SetVariable((node.Left as IdentifierNode).Value, v, frame);
            return v;
        }

        private Variable VisitAssignLetNode(AssignNode node, Frame frame)
        {
            Variable v = Visit(node.Right, frame.Next(node.Right.Token));
            env.LetVariable((node.Left as IdentifierNode).Value, v, frame);
            return v;
        }
    }
}
