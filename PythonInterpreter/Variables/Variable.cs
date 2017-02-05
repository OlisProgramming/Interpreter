using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.Exceptions;

namespace PythonInterpreter.Variables
{
    abstract class Variable
    {
        private string typeName;
        public string TypeName { get { return typeName; } }

        public Variable(string typeName)
        {
            this.typeName = typeName;
        }

        public Variable Add(Variable other)
        {
            Variable result;
            try
            {
                result = AddImpl(other);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                    "Add", TypeName);
            }

            return result;
        }
        public static Variable operator +(Variable self, Variable other)
        {
            return self.Add(other);
        }
        public abstract Variable AddImpl(Variable other);

        public Variable Sub(Variable other)
        {
            Variable result;
            try
            {
                result = SubImpl(other);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                    "Sub", TypeName);
            }

            return result;
        }
        public static Variable operator -(Variable self, Variable other)
        {
            return self.Sub(other);
        }
        public abstract Variable SubImpl(Variable other);

        public Variable Mul(Variable other)
        {
            Variable result;
            try
            {
                result = MulImpl(other);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                    "Mul", TypeName);
            }

            return result;
        }
        public static Variable operator *(Variable self, Variable other)
        {
            return self.Mul(other);
        }
        public abstract Variable MulImpl(Variable other);

        public Variable Div(Variable other)
        {
            Variable result;
            try
            {
                result = DivImpl(other);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                    "Div", TypeName);
            }

            return result;
        }
        public static Variable operator /(Variable self, Variable other)
        {
            return self.Div(other);
        }
        public abstract Variable DivImpl(Variable other);

        public Variable UnaryPlus(Variable other)
        {
            Variable result;
            try
            {
                result = UnaryPlusImpl(other);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                    "UnaryPlus", TypeName);
            }

            return result;
        }
        public abstract Variable UnaryPlusImpl(Variable other);

        public Variable UnaryMinus(Variable other)
        {
            Variable result;
            try
            {
                result = UnaryMinusImpl(other);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                    "UnaryMinus", TypeName);
            }

            return result;
        }
        public abstract Variable UnaryMinusImpl(Variable other);

        public Variable LessThan(Variable other)
        {
            Variable result;
            try
            {
                result = LessThanImpl(other);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                    "LessThan", TypeName);
            }

            return result;
        }
        public abstract Variable LessThanImpl(Variable other);

        public Variable GreaterThan(Variable other)
        {
            Variable result;
            try
            {
                result = GreaterThanImpl(other);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                    "GreaterThan", TypeName);
            }

            return result;
        }
        public abstract Variable GreaterThanImpl(Variable other);

        public Variable LessThanOrEqual(Variable other)
        {
            Variable result;
            try
            {
                result = LessThanOrEqualImpl(other);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                    "LessThanOrEqual", TypeName);
            }

            return result;
        }
        public abstract Variable LessThanOrEqualImpl(Variable other);

        public Variable GreaterThanOrEqual(Variable other)
        {
            Variable result;
            try
            {
                result = GreaterThanOrEqualImpl(other);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                    "GreaterThanOrEqual", TypeName);
            }

            return result;
        }
        public abstract Variable GreaterThanOrEqualImpl(Variable other);

        public Variable Equal(Variable other)
        {
            Variable result;
            try
            {
                result = EqualImpl(other);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                    "Equal", TypeName);
            }

            return result;
        }
        public abstract Variable EqualImpl(Variable other);

        public Variable NotEqual(Variable other)
        {
            Variable result;
            try
            {
                result = NotEqualImpl(other);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                    "NotEqual", TypeName);
            }

            return result;
        }
        public abstract Variable NotEqualImpl(Variable other);

        ///////////////

        public Variable Cast(string typeToCast)
        {
            try
            {
                if (typeName != typeToCast)
                    return CastImpl(typeToCast);
                return this;
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                    $"Cast({typeToCast})", TypeName);
            }
        }
        public abstract Variable CastImpl(string typeToCast);
    }
}
