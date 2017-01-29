using System;
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

        public string GetNextInteger()
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
                    return new Token(Token.TokenType.INTEGER, GetNextInteger());
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

        public void Eat(Token.TokenType type)
        {
            if (CurrentToken.Type == type)
            {
                CurrentToken = GetNextToken();
            }
            else
            {
                Error();
            }
        }

        public void Expr()
        {
            CurrentToken = GetNextToken();

            Token left = CurrentToken;
            Eat(Token.TokenType.INTEGER);

            Token op = CurrentToken;
            if (op.Type == Token.TokenType.PLUS)
                Eat(Token.TokenType.PLUS);
            else
                Eat(Token.TokenType.MINUS);

            Token right = CurrentToken;
            Eat(Token.TokenType.INTEGER);

            int resulta, resultb;
            if (!int.TryParse(left.Value, out resulta))
            {
                Error();
            }
            if (!int.TryParse(right.Value, out resultb))
            {
                Error();
            }
            Console.WriteLine(
                op.Type == Token.TokenType.PLUS ?
                resulta + resultb :
                resulta - resultb);
        }
    }
}
