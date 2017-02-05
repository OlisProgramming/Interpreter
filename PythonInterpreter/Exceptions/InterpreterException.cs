using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.TokeniserNamespace;

namespace PythonInterpreter.Exceptions
{
    class InterpreterException : Exception
    {
        public enum InterpreterExceptionType
        {
            NONE,

            TOKENISER_FILE_EMPTY,
            TOKENISER_UNRECOGNISED_TOKEN,
            TOKENISER_TOO_MANY_DECIMAL_POINTS,

            PARSER_EXPECTED_DIFFERENT_TOKEN,

            INTERPRETER_NO_VISIT_METHOD,
            INTERPRETER_INVALID_OPERATION,
            INTERPRETER_CANNOT_SET_READONLY_VARIABLE,
            INTERPRETER_VARIABLE_DOES_NOT_EXIST,
            INTERPRETER_VARIABLE_EXISTS,
        }

        public InterpreterExceptionType Error { get; set; }
        public Frame StackFrame { get; set; }
        public string[] Details { get; set; }

        public InterpreterException(InterpreterExceptionType error, Frame stackFrame, params string[] details)
        {
            Error = error;
            StackFrame = stackFrame;
            Details = details;
        }

        public override string ToString()
        {
            string msg = $"\n\n-----\n\nError caught at line {StackFrame.Line}, column {StackFrame.Column} of file {StackFrame.FileName}!\n"
             + $"Error code {((int)Error).ToString("D3")} ({Error}):\n";

            switch (Error)
            {
                case InterpreterExceptionType.TOKENISER_FILE_EMPTY:
                    msg += "The file was empty.";
                    break;

                case InterpreterExceptionType.TOKENISER_UNRECOGNISED_TOKEN:
                    msg += $"The token '{Details[0]}' was unrecognised.";
                    break;

                case InterpreterExceptionType.TOKENISER_TOO_MANY_DECIMAL_POINTS:
                    msg += "Only one decimal point is permitted in a numeric literal.";
                    break;

                case InterpreterExceptionType.PARSER_EXPECTED_DIFFERENT_TOKEN:
                    msg += $"Parser expected a token of type {Details[0]} but received a token of {Details[1]}.";
                    break;

                case InterpreterExceptionType.INTERPRETER_NO_VISIT_METHOD:
                    msg += $"Error while visiting node {Details[0]} of type {Details[1]}. There was no Visit method for this node.";
                    break;

                case InterpreterExceptionType.INTERPRETER_INVALID_OPERATION:
                    msg += $"Cannot perform operation {Details[0]} on type {Details[1]}.";
                    break;

                case InterpreterExceptionType.INTERPRETER_CANNOT_SET_READONLY_VARIABLE:
                    msg += $"Cannot set the value of the readonly variable {Details[0]}.";
                    break;

                case InterpreterExceptionType.INTERPRETER_VARIABLE_DOES_NOT_EXIST:
                    msg += $"Variable {Details[0]} does not exist.";
                    break;

                case InterpreterExceptionType.INTERPRETER_VARIABLE_EXISTS:
                    msg += $"Variable {Details[0]} exists, therefore it can not be used in a Let expression.";
                    break;

                default:
                    msg += "No defined error message";
                    break;
            }

            msg += "\n\nTraceback (most recent call first):\n";

            Frame f = StackFrame;
            Frame prev = null;
            do
            {
                IEnumerable<string> lines = System.IO.File.ReadLines(f.FileName);
                string line = lines.Skip(f.Line - 1).Take(1).First();

                if ((f.Line != prev?.Line) || (f.Column != prev?.Column) || (f.FileName != prev?.FileName))
                {
                    msg += $"Line {f.Line}, column {f.Column} of file {f.FileName}:\n{line}\n";
                    prev = f;
                }
            } while ((f = f.PreviousFrame) != null);

            return msg + "\n--------\n\n";
        }
    }
}
