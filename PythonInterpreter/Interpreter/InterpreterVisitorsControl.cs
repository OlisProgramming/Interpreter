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
        private Variable VisitPrintNode(PrintNode node, Frame frame)
        {
            Variable v = Visit(node.Child, frame.Next(node.Child.Token));
            Console.WriteLine(v);
            return v;
        }

        private Variable VisitStatementListNode(StatementListNode node, Frame frame)
        {
            env = new InterpreterEnvironment(env);
            foreach (Node statement in node.Statements)
            {
                Variable val = Visit(statement, frame.Next(statement.Token));
            }
            env = env.parent;

            return null;
        }

        private Variable VisitCastNode(CastNode node, Frame frame)
        {
            Variable val = Visit(node.Child, frame.Next(node.Child.Token));
            return val.Cast(node.TypeToCast, frame);
        }

        private Variable VisitIfNode(IfNode node, Frame frame)
        {
            VariableBoolean condition = (Visit(node.Left, frame.Next(node.Left.Token)).Cast("boolean", frame)) as VariableBoolean;
            if (condition.Value)
                Visit(node.Mid, frame.Next(node.Mid.Token));
            else
                Visit(node.Right, frame.Next(node.Right.Token));
            return condition;
        }

        private Variable VisitFunctionNode(FunctionNode node, Frame frame)
        {
            return Visit(node.Child, frame.Next(node.Child.Token)).Call(this, frame.Next(node.Child.Token));
        }
    }
}
