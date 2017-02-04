using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter.TokeniserNamespace
{
    class Token
    {
        public enum TokenType
        {
            NUMBER,
            ADD, SUB, MUL, DIV,
            LPARENTH, RPARENTH,
            LBRACE, RBRACE,
            LESS_THAN, GREATER_THAN,
            LESS_OR_EQUAL, GREATER_OR_EQUAL,
            EQUAL, NOT_EQUAL,
            ASSIGN, SEMICOLON, CAST,
            OUT,
            IF, ELSE,
            IDENTIFIER,
            EOF
        }

        public TokenType Type { get; set; }
        public string Value { get; set; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"Token({Type}, \"{Value}\")";
        }
    }
}
