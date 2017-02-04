﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.SyntaxTrees;
using PythonInterpreter.TokeniserNamespace;

namespace PythonInterpreter.ParserNamespace
{
    partial class Parser
    {
        public Node Program()
        {
            ProgramNode program = new ProgramNode(tokens[index]);

            while (index < tokens.Count - 1)
            {
                program.AddStatement(Statement());
            }

            return program;
        }

        public Node Statement()
        {
            Token tk = tokens[index];

            Node node;

            if (tk.Type == Token.TokenType.OUT)
            {
                node = PrintStatement();
            }
            else if (tk.Type == Token.TokenType.IDENTIFIER)
            {
                node = AssignmentExpression();
            }
            else
            {
                node = ArithmeticExpression();
            }

            Eat(Token.TokenType.SEMICOLON);

            return node;
        }

        public Node PrintStatement()
        {
            Eat(Token.TokenType.OUT);

            return new PrintNode(tokens[index], ArithmeticExpression());
        }
    }
}