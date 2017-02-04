using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                result = AddImpl(other);
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
                result = AddImpl(other);
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
                result = AddImpl(other);
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
