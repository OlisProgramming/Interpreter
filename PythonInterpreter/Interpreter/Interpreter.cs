﻿using System;
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
        private Frame frame;

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
            if (node is IdentifierNode)
                return VisitIdentifierNode(node as IdentifierNode);

            if (node is UnaryPlusNode)
                return VisitUnaryPlusNode(node as UnaryPlusNode);
            if (node is UnaryMinusNode)
                return VisitUnaryMinusNode(node as UnaryMinusNode);

            if (node is LessThanNode)
                return VisitLessThanNode(node as LessThanNode);
            if (node is GreaterThanNode)
                return VisitGreaterThanNode(node as GreaterThanNode);
            if (node is LessThanOrEqualNode)
                return VisitLessThanOrEqualNode(node as LessThanOrEqualNode);
            if (node is GreaterThanOrEqualNode)
                return VisitGreaterThanOrEqualNode(node as GreaterThanOrEqualNode);
            if (node is EqualNode)
                return VisitEqualNode(node as EqualNode);
            if (node is NotEqualNode)
                return VisitNotEqualNode(node as NotEqualNode);

            if (node is AssignNode)
                return VisitAssignNode(node as AssignNode);
            if (node is PrintNode)
                return VisitPrintNode(node as PrintNode);
            if (node is CastNode)
                return VisitCastNode(node as CastNode);

            if (node is IfNode)
                return VisitIfNode(node as IfNode);

            if (node is StatementListNode)
                return VisitProgramNode(node as StatementListNode);

            throw new InterpreterException(
                InterpreterException.InterpreterExceptionType.INTERPRETER_NO_VISIT_METHOD,
                frame,
                node.ToString(), node.GetType().Name);
        }

        private Variable VisitAddNode(AddNode node)
        {
            return Visit(node.Left).Add(Visit(node.Right), frame);
        }

        private Variable VisitSubNode(SubNode node)
        {
            return Visit(node.Left).Sub(Visit(node.Right), frame);
        }

        private Variable VisitMulNode(MulNode node)
        {
            return Visit(node.Left).Mul(Visit(node.Right), frame);
        }

        private Variable VisitDivNode(DivNode node)
        {
            return Visit(node.Left).Div(Visit(node.Right), frame);
        }

        private Variable VisitNumberNode(NumberNode node)
        {
            return node.Value;
        }

        private Variable VisitIdentifierNode(IdentifierNode node)
        {
            return env.GetVariable(node.Value, frame);
        }

        private Variable VisitUnaryPlusNode(UnaryPlusNode node)
        {
            return Visit(node.Child).UnaryPlus(frame);
        }

        private Variable VisitUnaryMinusNode(UnaryMinusNode node)
        {
            return Visit(node.Child).UnaryMinus(frame);  // TODO unary minus
        }

        private Variable VisitAssignNode(AssignNode node)
        {
            Variable v = Visit(node.Right);
            env.SetVariable((node.Left as IdentifierNode).Value, v, frame);
            return v;
        }

        private Variable VisitPrintNode(PrintNode node)
        {
            Variable v = Visit(node.Child);
            Console.WriteLine(v);
            return v;
        }

        private Variable VisitProgramNode(StatementListNode node)
        {
            foreach (Node statement in node.Statements)
            {
                Variable val = Visit(statement);
            }

            return null;
        }

        private Variable VisitCastNode(CastNode node)
        {
            Variable val = Visit(node.Child);
            return val.Cast(node.TypeToCast, frame);
        }

        private Variable VisitIfNode(IfNode node)
        {
            VariableBoolean condition = (Visit(node.Left).Cast("boolean", frame)) as VariableBoolean;
            if (condition.Value)
                Visit(node.Mid);
            else
                Visit(node.Right);
            return condition;
        }

        private Variable VisitLessThanNode(LessThanNode node)
        {
            return Visit(node.Left).LessThan(Visit(node.Right), frame);
        }

        private Variable VisitGreaterThanNode(GreaterThanNode node)
        {
            return Visit(node.Left).GreaterThan(Visit(node.Right), frame);
        }

        private Variable VisitLessThanOrEqualNode(LessThanOrEqualNode node)
        {
            return Visit(node.Left).LessThanOrEqual(Visit(node.Right), frame);
        }

        private Variable VisitGreaterThanOrEqualNode(GreaterThanOrEqualNode node)
        {
            return Visit(node.Left).GreaterThanOrEqual(Visit(node.Right), frame);
        }

        private Variable VisitEqualNode(EqualNode node)
        {
            return Visit(node.Left).Equal(Visit(node.Right), frame);
        }

        private Variable VisitNotEqualNode(NotEqualNode node)
        {
            return Visit(node.Left).NotEqual(Visit(node.Right), frame);
        }
    }
}
