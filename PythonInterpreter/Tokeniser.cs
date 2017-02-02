using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter
{
    partial class Tokeniser
    {
        string Text { get; set; }
        int TextPos { get; set; }
        Token CurrentToken { get; set; }
        char CurrentChar { get; set; }

        public Tokeniser(string text)
        {
            Text = text;
            if (text.Length == 0)
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.TOKENISER_FILE_EMPTY,
                    null);
            CurrentChar = Text[0];
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
                else if (char.IsLetter(CurrentChar))
                {
                    return new Token(Token.TokenType.IDENTIFIER, GetNextIdentifierValue());
                }
                switch (CurrentChar)
                {
                    case '+':
                        Advance();
                        return new Token(Token.TokenType.ADD, "+");

                    case '-':
                        Advance();
                        return new Token(Token.TokenType.SUB, "-");

                    case '*':
                        Advance();
                        return new Token(Token.TokenType.MUL, "*");

                    case '/':
                        Advance();
                        return new Token(Token.TokenType.DIV, "/");

                    case '(':
                        Advance();
                        return new Token(Token.TokenType.LPARENTH, "(");

                    case ')':
                        Advance();
                        return new Token(Token.TokenType.RPARENTH, ")");

                    case '=':
                        Advance();
                        return new Token(Token.TokenType.ASSIGN, "=");

                    default:
                        throw new InterpreterException(
                            InterpreterException.InterpreterExceptionType.TOKENISER_UNRECOGNISED_TOKEN,
                            new Token(Token.TokenType.EOF, new string(CurrentChar, 1)));
                }
            }

            return new Token(Token.TokenType.EOF, "");
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
