using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.Exceptions;

namespace PythonInterpreter.TokeniserNamespace
{
    partial class Tokeniser
    {
        string Text { get; set; }
        int TextPos { get; set; }
        int Line { get; set; }
        int Column { get; set; }
        Token CurrentToken { get; set; }
        char CurrentChar { get; set; }
        string FileName { get; set; }

        public Tokeniser(string text, string fileName)
        {
            Text = text;
            if (text.Length == 0)
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.TOKENISER_FILE_EMPTY,
                    null);
            CurrentChar = Text[0];
            Line = 1;
            Column = 0;
            FileName = fileName;
        }

        public void Advance()
        {
            TextPos++;
            Column++;
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
                if (CurrentChar == '\n')
                {
                    Line++;
                    Column = 0;
                }
                Advance();
            }
        }

        public string GetNextIdentifierValue()
        {
            string result = "";
            while (CurrentChar != '\0' && char.IsLetterOrDigit(CurrentChar))
            {
                result += CurrentChar;
                Advance();
            }
            return result;
        }

        public string GetNextNumericValue()
        {
            string result = "";
            bool decimalPointUsed = false;
            while (CurrentChar != '\0' && (char.IsDigit(CurrentChar) || CurrentChar == '.'))
            {
                result += CurrentChar;
                if (CurrentChar == '.')
                {
                    if (decimalPointUsed)
                        throw new InterpreterException(
                            InterpreterException.InterpreterExceptionType.TOKENISER_TOO_MANY_DECIMAL_POINTS,
                            new Token(Token.TokenType.EOF, "", Line, Column, FileName));
                    decimalPointUsed = true;
                }
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
                    return new Token(Token.TokenType.NUMBER, GetNextNumericValue(), Line, Column, FileName);
                }
                else if (char.IsLetter(CurrentChar))
                {
                    string id = GetNextIdentifierValue();
                    if (id == "if")
                        return new Token(Token.TokenType.IF, "if", Line, Column, FileName);
                    else if (id == "else")
                        return new Token(Token.TokenType.ELSE, "else", Line, Column, FileName);
                    else
                        return new Token(Token.TokenType.IDENTIFIER, id, Line, Column, FileName);
                }
                switch (CurrentChar)
                {
                    case '+':
                        Advance();
                        return new Token(Token.TokenType.ADD, "+", Line, Column, FileName);

                    case '-':
                        Advance();
                        if (CurrentChar == '>')
                        {
                            Advance();
                            return new Token(Token.TokenType.CAST, "->", Line, Column, FileName);
                        }
                        return new Token(Token.TokenType.SUB, "-", Line, Column, FileName);

                    case '*':
                        Advance();
                        return new Token(Token.TokenType.MUL, "*", Line, Column, FileName);

                    case '/':
                        Advance();
                        return new Token(Token.TokenType.DIV, "/", Line, Column, FileName);

                    case '(':
                        Advance();
                        return new Token(Token.TokenType.LPARENTH, "(", Line, Column, FileName);

                    case ')':
                        Advance();
                        return new Token(Token.TokenType.RPARENTH, ")", Line, Column, FileName);

                    case '{':
                        Advance();
                        return new Token(Token.TokenType.LBRACE, "{", Line, Column, FileName);

                    case '}':
                        Advance();
                        return new Token(Token.TokenType.RBRACE, "}", Line, Column, FileName);

                    case '=':
                        Advance();
                        if (CurrentChar == '=')
                        {
                            Advance();
                            return new Token(Token.TokenType.EQUAL, "==", Line, Column, FileName);
                        }
                        return new Token(Token.TokenType.ASSIGN, "=", Line, Column, FileName);

                    case '!':
                        Advance();
                        if (CurrentChar == '=')
                        {
                            Advance();
                            return new Token(Token.TokenType.NOT_EQUAL, "!=", Line, Column, FileName);
                        }
                        throw new InterpreterException(
                            InterpreterException.InterpreterExceptionType.TOKENISER_UNRECOGNISED_TOKEN,
                            new Token(Token.TokenType.EOF, new string(CurrentChar, 1), Line, Column, FileName));

                    case ';':
                        Advance();
                        return new Token(Token.TokenType.SEMICOLON, ";", Line, Column, FileName);

                    case ':':
                        Advance();
                        return new Token(Token.TokenType.OUT, ":", Line, Column, FileName);

                    case '<':
                        Advance();
                        if (CurrentChar == '=')
                        {
                            Advance();
                            return new Token(Token.TokenType.LESS_OR_EQUAL, "<=", Line, Column, FileName);
                        }
                        return new Token(Token.TokenType.LESS_THAN, "<", Line, Column, FileName);

                    case '>':
                        Advance();
                        if (CurrentChar == '=')
                        {
                            Advance();
                            return new Token(Token.TokenType.GREATER_OR_EQUAL, ">=", Line, Column, FileName);
                        }
                        return new Token(Token.TokenType.GREATER_THAN, ">", Line, Column, FileName);


                    default:
                        throw new InterpreterException(
                            InterpreterException.InterpreterExceptionType.TOKENISER_UNRECOGNISED_TOKEN,
                            new Token(Token.TokenType.EOF, new string(CurrentChar, 1), Line, Column, FileName));
                }
            }

            return new Token(Token.TokenType.EOF, "", Line, Column, FileName);
        }

        public List<Token> Tokenise()
        {
            List<Token> tokens = new List<Token>();

            while ((CurrentToken = GetNextToken()).Type != Token.TokenType.EOF)
            {
                tokens.Add(CurrentToken);
            }
            tokens.Add(CurrentToken);

            return tokens;
        }
    }
}
