using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.SyntaxTrees;
using PythonInterpreter.TokeniserNamespace;
using PythonInterpreter.Exceptions;

namespace PythonInterpreter.ParserNamespace
{
    partial class Parser
    {
        public Node Program()
        {
            return StatementBlock();
        }

        public Node StatementBlock()
        {
            Eat(Token.TokenType.LBRACE);

            StatementListNode node = new StatementListNode(tokens[index]);

            while (index < tokens.Count - 1)
            {
                Node statement = Statement();
                node.AddStatement(statement);
                if (tokens[index].Type == Token.TokenType.RBRACE)
                {
                    break;
                }
            }

            Eat(Token.TokenType.RBRACE);
            return node;
        }

        public Node Statement()
        {
            Token tk = tokens[index];

            Node node;

            if (tk.Type == Token.TokenType.OUT)
            {
                node = PrintStatement();
                Eat(Token.TokenType.SEMICOLON);
            }
            else if (tk.Type == Token.TokenType.IDENTIFIER)
            {
                node = AssignmentExpression();
                Eat(Token.TokenType.SEMICOLON);
            }
            else if (tk.Type == Token.TokenType.IF)
            {
                node = IfStatement();
            }
            else
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.PARSER_EXPECTED_DIFFERENT_TOKEN,
                    new Token(Token.TokenType.EOF, ""),
                    "IF or OUT or IDENTIFIER", tk.Type.ToString());

            }

            return node;
        }

        public Node PrintStatement()
        {
            Eat(Token.TokenType.OUT);

            return new PrintNode(tokens[index - 1], ArithmeticExpression());
        }
    }
}
