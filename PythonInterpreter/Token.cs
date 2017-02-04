﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter
{
    class Token
    {
        public enum TokenType
        {
            NUMBER,
            ADD, SUB, MUL, DIV,
            LPARENTH, RPARENTH,
            ASSIGN, SEMICOLON,
            OUT,
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
