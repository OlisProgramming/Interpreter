﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter
{
    class InterpreterException : Exception
    {
        public enum InterpreterExceptionType
        {
            NONE,

            TOKENISER_FILE_EMPTY,
            TOKENISER_UNRECOGNISED_TOKEN,

            PARSER_EXPECTED_DIFFERENT_TOKEN
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
            string msg = $"\n\n-----\n\nError caught! Error code {((int)Error).ToString("D3")} ({Error}):\n";

            switch (Error)
            {
                case InterpreterExceptionType.TOKENISER_FILE_EMPTY:
                    msg += "The file was empty.";
                    break;

                case InterpreterExceptionType.TOKENISER_UNRECOGNISED_TOKEN:
                    msg += $"The token '{TokenAt.Value}' was unrecognised.";
                    break;

                case InterpreterExceptionType.PARSER_EXPECTED_DIFFERENT_TOKEN:
                    msg += $"Parser expected a token of type {Details[0]} but received a token of {Details[1]}.";
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