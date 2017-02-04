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
        public int Line { get; set; }
        public int Column { get; set; }
        public string FileName { get; set; }

        public Token(TokenType type, string value) : this(type, value, 0, 0, "") { }

        public Token(TokenType type, string value, int line, int column, string fileName)
        {
            Type = type;
            Value = value;
            Line = line;
            Column = column;
            FileName = fileName;
        }

        public override string ToString()
        {
            return $"Token({Type}, \"{Value}\")";
        }
    }
}
