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
        private Variable VisitAddNode(AddNode node, Frame frame)
        {
            return Visit(node.Left, frame.Next(node.Left.Token)).Add(Visit(node.Right, frame.Next(node.Right.Token)), frame);
        }

        private Variable VisitSubNode(SubNode node, Frame frame)
        {
            return Visit(node.Left, frame.Next(node.Left.Token)).Sub(Visit(node.Right, frame.Next(node.Right.Token)), frame);
        }

        private Variable VisitMulNode(MulNode node, Frame frame)
        {
            return Visit(node.Left, frame.Next(node.Left.Token)).Mul(Visit(node.Right, frame.Next(node.Right.Token)), frame);
        }

        private Variable VisitDivNode(DivNode node, Frame frame)
        {
            return Visit(node.Left, frame.Next(node.Left.Token)).Div(Visit(node.Right, frame.Next(node.Right.Token)), frame);
        }

        private Variable VisitUnaryPlusNode(UnaryPlusNode node, Frame frame)
        {
            return Visit(node.Child, frame.Next(node.Child.Token)).UnaryPlus(frame);
        }

        private Variable VisitUnaryMinusNode(UnaryMinusNode node, Frame frame)
        {
            return Visit(node.Child, frame.Next(node.Child.Token)).UnaryMinus(frame);  // TODO unary minus
        }

        private Variable VisitLessThanNode(LessThanNode node, Frame frame)
        {
            return Visit(node.Left, frame.Next(node.Left.Token)).LessThan(Visit(node.Right, frame.Next(node.Right.Token)), frame);
        }

        private Variable VisitGreaterThanNode(GreaterThanNode node, Frame frame)
        {
            return Visit(node.Left, frame.Next(node.Left.Token)).GreaterThan(Visit(node.Right, frame.Next(node.Right.Token)), frame);
        }

        private Variable VisitLessThanOrEqualNode(LessThanOrEqualNode node, Frame frame)
        {
            return Visit(node.Left, frame.Next(node.Left.Token)).LessThanOrEqual(Visit(node.Right, frame.Next(node.Right.Token)), frame);
        }

        private Variable VisitGreaterThanOrEqualNode(GreaterThanOrEqualNode node, Frame frame)
        {
            return Visit(node.Left, frame.Next(node.Left.Token)).GreaterThanOrEqual(Visit(node.Right, frame.Next(node.Right.Token)), frame);
        }

        private Variable VisitEqualNode(EqualNode node, Frame frame)
        {
            return Visit(node.Left, frame.Next(node.Left.Token)).Equal(Visit(node.Right, frame.Next(node.Right.Token)), frame);
        }

        private Variable VisitNotEqualNode(NotEqualNode node, Frame frame)
        {
            return Visit(node.Left, frame.Next(node.Left.Token)).NotEqual(Visit(node.Right, frame.Next(node.Right.Token)), frame);
        }
    }
}
