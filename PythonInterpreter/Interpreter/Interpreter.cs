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
    class Interpreter
    {
        private InterpreterEnvironment env;

        public Interpreter()
        {
            env = new InterpreterEnvironment();
        }

        public Variable Visit(Node node, Frame frame)
        {
            if (node is AddNode)
                return VisitAddNode(node as AddNode, frame);
            if (node is SubNode)
                return VisitSubNode(node as SubNode, frame);
            if (node is MulNode)
                return VisitMulNode(node as MulNode, frame);
            if (node is DivNode)
                return VisitDivNode(node as DivNode, frame);

            if (node is NumberNode)
                return VisitNumberNode(node as NumberNode, frame);
            if (node is IdentifierNode)
                return VisitIdentifierNode(node as IdentifierNode, frame);

            if (node is UnaryPlusNode)
                return VisitUnaryPlusNode(node as UnaryPlusNode, frame);
            if (node is UnaryMinusNode)
                return VisitUnaryMinusNode(node as UnaryMinusNode, frame);

            if (node is LessThanNode)
                return VisitLessThanNode(node as LessThanNode, frame);
            if (node is GreaterThanNode)
                return VisitGreaterThanNode(node as GreaterThanNode, frame);
            if (node is LessThanOrEqualNode)
                return VisitLessThanOrEqualNode(node as LessThanOrEqualNode, frame);
            if (node is GreaterThanOrEqualNode)
                return VisitGreaterThanOrEqualNode(node as GreaterThanOrEqualNode, frame);
            if (node is EqualNode)
                return VisitEqualNode(node as EqualNode, frame);
            if (node is NotEqualNode)
                return VisitNotEqualNode(node as NotEqualNode, frame);

            if (node is AssignNode)
                return VisitAssignNode(node as AssignNode, frame);
            if (node is PrintNode)
                return VisitPrintNode(node as PrintNode, frame);
            if (node is CastNode)
                return VisitCastNode(node as CastNode, frame);

            if (node is IfNode)
                return VisitIfNode(node as IfNode, frame);

            if (node is StatementListNode)
                return VisitStatementListNode(node as StatementListNode, frame);

            throw new InterpreterException(
                InterpreterException.InterpreterExceptionType.INTERPRETER_NO_VISIT_METHOD,
                frame,
                node.ToString(), node.GetType().Name);
        }

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

        private Variable VisitNumberNode(NumberNode node, Frame frame)
        {
            return node.Value;
        }

        private Variable VisitIdentifierNode(IdentifierNode node, Frame frame)
        {
            return env.GetVariable(node.Value, frame);
        }

        private Variable VisitUnaryPlusNode(UnaryPlusNode node, Frame frame)
        {
            return Visit(node.Child, frame.Next(node.Child.Token)).UnaryPlus(frame);
        }

        private Variable VisitUnaryMinusNode(UnaryMinusNode node, Frame frame)
        {
            return Visit(node.Child, frame.Next(node.Child.Token)).UnaryMinus(frame);  // TODO unary minus
        }

        private Variable VisitAssignNode(AssignNode node, Frame frame)
        {
            Variable v = Visit(node.Right, frame.Next(node.Right.Token));
            env.SetVariable((node.Left as IdentifierNode).Value, v, frame);
            return v;
        }

        private Variable VisitPrintNode(PrintNode node, Frame frame)
        {
            Variable v = Visit(node.Child, frame.Next(node.Child.Token));
            Console.WriteLine(v);
            return v;
        }

        private Variable VisitStatementListNode(StatementListNode node, Frame frame)
        {
            foreach (Node statement in node.Statements)
            {
                Variable val = Visit(statement, frame.Next(statement.Token));
            }

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
