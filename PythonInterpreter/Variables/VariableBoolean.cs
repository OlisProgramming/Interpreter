using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.Exceptions;
using PythonInterpreter.InterpreterNamespace;

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

        public override Variable AddImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable SubImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable MulImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable DivImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable CastImpl(string typeToCast, Frame frame)
        {
            if (typeToCast == "number")
                return new VariableNumber(Value ? 1 : 0);

            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Value ? "true" : "false";
        }

        public override Variable UnaryPlusImpl(Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable UnaryMinusImpl(Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable LessThanImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable GreaterThanImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable LessThanOrEqualImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable GreaterThanOrEqualImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable EqualImpl(Variable other, Frame frame)
        {
            return new VariableBoolean(Value == (other.Cast("boolean", frame) as VariableBoolean).Value);
        }

        public override Variable NotEqualImpl(Variable other, Frame frame)
        {
            return new VariableBoolean(Value != (other.Cast("boolean", frame) as VariableBoolean).Value);
        }

        public override Variable CallImpl(Interpreter interpreter, Frame frame)
        {
            throw new NotImplementedException();
        }
    }
}
