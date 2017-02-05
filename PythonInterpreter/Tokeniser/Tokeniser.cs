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
                    new Frame(1, 1, fileName, null));
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
                            new Frame(Line, Column, FileName, null));
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
                    return new Token(Token.TokenType.NUMBER, GetNextNumericValue(), new Frame(Line, Column, FileName, null));
                }
                else if (char.IsLetter(CurrentChar))
                {
                    string id = GetNextIdentifierValue();
                    if (id == "if")
                        return new Token(Token.TokenType.IF, "if", new Frame(Line, Column, FileName, null));
                    else if (id == "else")
                        return new Token(Token.TokenType.ELSE, "else", new Frame(Line, Column, FileName, null));
                    else if (id == "let")
                        return new Token(Token.TokenType.LET, "let", new Frame(Line, Column, FileName, null));
                    else
                        return new Token(Token.TokenType.IDENTIFIER, id, new Frame(Line, Column, FileName, null));
                }
                switch (CurrentChar)
                {
                    case '+':
                        Advance();
                        return new Token(Token.TokenType.ADD, "+", new Frame(Line, Column, FileName, null));

                    case '-':
                        Advance();
                        if (CurrentChar == '>')
                        {
                            Advance();
                            return new Token(Token.TokenType.CAST, "->", new Frame(Line, Column, FileName, null));
                        }
                        return new Token(Token.TokenType.SUB, "-", new Frame(Line, Column, FileName, null));

                    case '*':
                        Advance();
                        return new Token(Token.TokenType.MUL, "*", new Frame(Line, Column, FileName, null));

                    case '/':
                        Advance();
                        return new Token(Token.TokenType.DIV, "/", new Frame(Line, Column, FileName, null));

                    case '(':
                        Advance();
                        return new Token(Token.TokenType.LPARENTH, "(", new Frame(Line, Column, FileName, null));

                    case ')':
                        Advance();
                        return new Token(Token.TokenType.RPARENTH, ")", new Frame(Line, Column, FileName, null));

                    case '{':
                        Advance();
                        return new Token(Token.TokenType.LBRACE, "{", new Frame(Line, Column, FileName, null));

                    case '}':
                        Advance();
                        return new Token(Token.TokenType.RBRACE, "}", new Frame(Line, Column, FileName, null));

                    case '=':
                        Advance();
                        if (CurrentChar == '=')
                        {
                            Advance();
                            return new Token(Token.TokenType.EQUAL, "==", new Frame(Line, Column, FileName, null));
                        }
                        return new Token(Token.TokenType.ASSIGN, "=", new Frame(Line, Column, FileName, null));

                    case '!':
                        Advance();
                        if (CurrentChar == '=')
                        {
                            Advance();
                            return new Token(Token.TokenType.NOT_EQUAL, "!=", new Frame(Line, Column, FileName, null));
                        }
                        throw new InterpreterException(
                            InterpreterException.InterpreterExceptionType.TOKENISER_UNRECOGNISED_TOKEN,
                            new Frame(Line, Column, FileName, null));

                    case ';':
                        Advance();
                        return new Token(Token.TokenType.SEMICOLON, ";", new Frame(Line, Column, FileName, null));

                    case ':':
                        Advance();
                        return new Token(Token.TokenType.OUT, ":", new Frame(Line, Column, FileName, null));

                    case '<':
                        Advance();
                        if (CurrentChar == '=')
                        {
                            Advance();
                            return new Token(Token.TokenType.LESS_OR_EQUAL, "<=", new Frame(Line, Column, FileName, null));
                        }
                        return new Token(Token.TokenType.LESS_THAN, "<", new Frame(Line, Column, FileName, null));

                    case '>':
                        Advance();
                        if (CurrentChar == '=')
                        {
                            Advance();
                            return new Token(Token.TokenType.GREATER_OR_EQUAL, ">=", new Frame(Line, Column, FileName, null));
                        }
                        return new Token(Token.TokenType.GREATER_THAN, ">", new Frame(Line, Column, FileName, null));

                    case '#':
                        do Advance(); while (CurrentChar != '\n');
                        break;

                    default:
                        throw new InterpreterException(
                            InterpreterException.InterpreterExceptionType.TOKENISER_UNRECOGNISED_TOKEN,
                            new Frame(Line, Column, FileName, null),
                            new string(CurrentChar, 1));
                }
            }

            return new Token(Token.TokenType.EOF, "", new Frame(Line, Column, FileName, null));
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
