using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter
{
    class Interpreter
    {
        private InterpreterEnvironment env;

        public Interpreter()
        {
            env = new InterpreterEnvironment();
        }

        public Variable Visit(Node node)
        {
            if (node is AddNode)
                return VisitAddNode(node as AddNode);
            if (node is SubNode)
                return VisitSubNode(node as SubNode);
            if (node is MulNode)
                return VisitMulNode(node as MulNode);
            if (node is DivNode)
                return VisitDivNode(node as DivNode);

            if (node is NumberNode)
                return VisitNumberNode(node as NumberNode);

            if (node is UnaryPlusNode)
                return VisitUnaryPlusNode(node as UnaryPlusNode);
            if (node is UnaryMinusNode)
                return VisitUnaryMinusNode(node as UnaryMinusNode);

            if (node is AssignNode)
                return VisitAssignNode(node as AssignNode);

            throw new Exception($"Error while visiting node {node} of type {node.GetType().Name}. There was no Visit method for this node.");
        }

        private Variable VisitAddNode(AddNode node)
        {
            return Visit(node.Left) + Visit(node.Right);
        }

        private Variable VisitSubNode(SubNode node)
        {
            return Visit(node.Left) - Visit(node.Right);
        }

        private Variable VisitMulNode(MulNode node)
        {
            return Visit(node.Left) * Visit(node.Right);
        }

        private Variable VisitDivNode(DivNode node)
        {
            return Visit(node.Left) / Visit(node.Right);
        }

        private Variable VisitNumberNode(NumberNode node)
        {
            return node.Value;
        }

        private Variable VisitUnaryPlusNode(UnaryPlusNode node)
        {
            return Visit(node.Child);
        }

        private Variable VisitUnaryMinusNode(UnaryMinusNode node)
        {
            return Visit(node.Child) * new VariableDouble(-1.0);  // TODO unary minus
        }

        private Variable VisitAssignNode(AssignNode node)
        {
            Variable v = Visit(node.Right);
            env.SetVariable((node.Left as IdentifierNode).Value, v);
            return v;
        }
    }
}
