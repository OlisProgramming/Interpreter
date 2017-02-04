using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter.Variables
{
    class VariableDouble : Variable
    {
        internal double Value { get; set; }

        public VariableDouble() : this(0.0) { }

        public VariableDouble(double value) : base("double")
        {
            Value = value;
        }

        public override Variable AddImpl(Variable other)
        {
            VariableDouble otherDouble = other as VariableDouble;
            if (otherDouble == null)
            {
                //throw new InterpreterException()
            }
            return new VariableDouble(Value + otherDouble.Value);
        }

        public override Variable SubImpl(Variable other)
        {
            VariableDouble otherDouble = other as VariableDouble;
            if (otherDouble == null)
            {
                //throw new InterpreterException()
            }
            return new VariableDouble(Value - otherDouble.Value);
        }

        public override Variable MulImpl(Variable other)
        {
            VariableDouble otherDouble = other as VariableDouble;
            if (otherDouble == null)
            {
                //throw new InterpreterException()
            }
            return new VariableDouble(Value * otherDouble.Value);
        }

        public override Variable DivImpl(Variable other)
        {
            VariableDouble otherDouble = other as VariableDouble;
            if (otherDouble == null)
            {
                //throw new InterpreterException()
            }
            return new VariableDouble(Value / otherDouble.Value);
        }

        public override Variable CastImpl(string typeToCast)
        {
            if (typeToCast == "boolean") return new VariableBoolean(Value != 0);

            throw new InterpreterException(
                InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                $"Cast({typeToCast})", TypeName);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
