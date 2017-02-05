using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.TokeniserNamespace;

namespace PythonInterpreter
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
        }

        public InterpreterExceptionType Error { get; set; }
        public Token TokenAt { get; set; }  // For debugging line/col of error.
        public string[] Details { get; set; }

        public InterpreterException(InterpreterExceptionType error, Token token_at, params string[] details)
        {
            Error = error;
            TokenAt = token_at;
            Details = details;
        }

        public override string ToString()
        {
            IEnumerable<string> lines = System.IO.File.ReadLines(TokenAt.FileName);
            string line = lines.Skip(TokenAt.Line - 1).Take(1).First();

            string msg = $"\n\n-----\n\nError caught at line {TokenAt.Line}, column {TokenAt.Column} of file {TokenAt.FileName}! "
             + $"Erroneous line is:\n{line}\n" + "^".PadLeft(TokenAt.Column + 1) + "\n"
             + $"Error code {((int)Error).ToString("D3")} ({Error}):\n";

            switch (Error)
            {
                case InterpreterExceptionType.TOKENISER_FILE_EMPTY:
                    msg += "The file was empty.";
                    break;

                case InterpreterExceptionType.TOKENISER_UNRECOGNISED_TOKEN:
                    msg += $"The token '{TokenAt.Value}' was unrecognised.";
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

                default:
                    msg += "No defined error message";
                    break;
            }

            msg += "\n\n-----\n\n";

            return msg;
        }
    }
}
