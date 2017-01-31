using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter
{
    class NodeVisitor
    {
        public double Visit(Node node)
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
            throw new Exception($"Error while visiting node {node} of type {node.GetType().Name}. There was no Visit method for this node.");
        }

        private double VisitAddNode(AddNode node)
        {
            return Visit(node.Left) + Visit(node.Right);
        }

        private double VisitSubNode(SubNode node)
        {
            return Visit(node.Left) - Visit(node.Right);
        }

        private double VisitMulNode(MulNode node)
        {
            return Visit(node.Left) * Visit(node.Right);
        }

        private double VisitDivNode(DivNode node)
        {
            return Visit(node.Left) / Visit(node.Right);
        }

        private double VisitNumberNode(NumberNode node)
        {
            return node.Value;
        }

        private double VisitUnaryPlusNode(UnaryPlusNode node)
        {
            return Visit(node.Child);
        }

        private double VisitUnaryMinusNode(UnaryMinusNode node)
        {
            return -Visit(node.Child);
        }
    }
}
