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
        private InterpreterEnvironment env;

        public Interpreter()
        {
            env = new InterpreterEnvironmentRoot();
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
            if (node is AssignLetNode)
                return VisitAssignLetNode((node as AssignLetNode).Child as AssignNode, frame);
            if (node is PrintNode)
                return VisitPrintNode(node as PrintNode, frame);
            if (node is CastNode)
                return VisitCastNode(node as CastNode, frame);
            if (node is FunctionNode)
                return VisitFunctionNode(node as FunctionNode, frame);

            if (node is IfNode)
                return VisitIfNode(node as IfNode, frame);

            if (node is StatementListNode)
                return VisitStatementListNode(node as StatementListNode, frame);

            throw new InterpreterException(
                InterpreterException.InterpreterExceptionType.INTERPRETER_NO_VISIT_METHOD,
                frame,
                node.ToString(), node.GetType().Name);
        }

        private Variable VisitNumberNode(NumberNode node, Frame frame)
        {
            return node.Value;
        }

        private Variable VisitIdentifierNode(IdentifierNode node, Frame frame)
        {
            return env.GetVariable(node.Value, frame);
        }
    }
}
