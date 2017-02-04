using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter.Variables
{
    class VariableBoolean : Variable
    {
        internal bool Value { get; set; }

        public VariableBoolean() : this(false) { }

        public VariableBoolean(bool value) : base("boolean")
        {
            Value = value;
        }

        public override Variable AddImpl(Variable other)
        {
            throw new InterpreterException(
                InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                "Add", TypeName);
        }

        public override Variable SubImpl(Variable other)
        {
            throw new InterpreterException(
                InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                "Sub", TypeName);
        }

        public override Variable MulImpl(Variable other)
        {
            throw new InterpreterException(
                InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                "Mul", TypeName);
        }

        public override Variable DivImpl(Variable other)
        {
            throw new InterpreterException(
                InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                "Div", TypeName);
        }

        public override Variable CastImpl(string typeToCast)
        {
            if (typeToCast == "number")
                return new VariableNumber(Value ? 1 : 0);

            throw new InterpreterException(
                InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                "Cast", TypeName);
        }

        public override string ToString()
        {
            return Value ? "true" : "false";
        }
    }
}
