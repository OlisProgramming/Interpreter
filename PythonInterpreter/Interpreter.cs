﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter
{
    class Interpreter
    {
        string Text { get; set; }
        int TextPos { get; set; }
        Token CurrentToken { get; set; }
        char CurrentChar { get; set; }

        public Interpreter(string text)
        {
            Text = text;
            if (text.Length == 0)
                Error();
            CurrentChar = Text[0];
        }

        public void Error()
        {
            throw new Exception("Error parsing input");
        }

        public void Advance()
        {
            TextPos++;
            if (TextPos > Text.Length - 1)
            {
                CurrentChar = '\0';
            }
            else
            {
                CurrentChar = Text[TextPos];
            }
        }

        public void SkipWhitespace()
        {
            while (CurrentChar != '\0' && char.IsWhiteSpace(CurrentChar))
            {
                Advance();
            }
        }

        public string GetNextIntegerValue()
        {
            string result = "";
            while (CurrentChar != '\0' && char.IsDigit(CurrentChar))
            {
                result += CurrentChar;
                Advance();
            }
            return result;
        }

        public Token GetNextToken()
        {
            while (CurrentChar != '\0')
            {
                if (char.IsWhiteSpace(CurrentChar))
                {
                    SkipWhitespace();
                    continue;
                }
                else if (char.IsDigit(CurrentChar))
                {
                    return new Token(Token.TokenType.INTEGER, GetNextIntegerValue());
                }
                switch (CurrentChar)
                {
                    case '+':
                        Advance();
                        return new Token(Token.TokenType.PLUS, new string(CurrentChar, 1));

                    case '-':
                        Advance();
                        return new Token(Token.TokenType.MINUS, new string(CurrentChar, 1));
                }
            }

            return new Token(Token.TokenType.EOF, "");
        }

        public Token Eat(Token.TokenType type)
        {
            if (CurrentToken.Type == type)
            {
                return CurrentToken = GetNextToken();
            }
            else
            {
                Error();
                return null;
            }
        }

        public void Expr()
        {
            CurrentToken = GetNextToken();

            Token tk = Eat(Token.TokenType.INTEGER);
            int result = Convert.ToInt32(tk.Value);

            while (
                CurrentToken.Type == Token.TokenType.PLUS ||
                CurrentToken.Type == Token.TokenType.MINUS
                )
            {
                switch (CurrentToken.Type)
                {
                    case Token.TokenType.PLUS:
                        Eat(Token.TokenType.PLUS);
                        result += Convert.ToInt32(Eat(Token.TokenType.INTEGER).Value);
                        break;

                    case Token.TokenType.MINUS:
                        Eat(Token.TokenType.MINUS);
                        result -= Convert.ToInt32(Eat(Token.TokenType.INTEGER).Value);
                        break;
                }
            }

            Console.WriteLine(result);
        }
    }
}
