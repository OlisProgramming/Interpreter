using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.Exceptions;
using PythonInterpreter.InterpreterNamespace;

namespace PythonInterpreter.Variables
{
    class VariableNumber : Variable
    {
        internal double Value { get; set; }

        public VariableNumber() : this(0.0) { }

        public VariableNumber(double value) : base("number")
        {
            Value = value;
        }

        public override Variable AddImpl(Variable other, Frame frame)
        {
            VariableNumber otherDouble = other.Cast("number", frame) as VariableNumber;
            return new VariableNumber(Value + otherDouble.Value);
        }

        public override Variable SubImpl(Variable other, Frame frame)
        {
            VariableNumber otherDouble = other.Cast("number", frame) as VariableNumber;
            return new VariableNumber(Value - otherDouble.Value);
        }

        public override Variable MulImpl(Variable other, Frame frame)
        {
            VariableNumber otherDouble = other.Cast("number", frame) as VariableNumber;
            return new VariableNumber(Value * otherDouble.Value);
        }

        public override Variable DivImpl(Variable other, Frame frame)
        {
            VariableNumber otherDouble = other.Cast("number", frame) as VariableNumber;
            return new VariableNumber(Value / otherDouble.Value);
        }

        public override Variable CastImpl(string typeToCast, Frame frame)
        {
            if (typeToCast == "boolean") return new VariableBoolean(Value != 0);

            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override Variable UnaryPlusImpl(Frame frame)
        {
            return this;
        }

        public override Variable UnaryMinusImpl(Frame frame)
        {
            return new VariableNumber(-Value);
        }

        public override Variable LessThanImpl(Variable other, Frame frame)
        {
            return new VariableBoolean(Value < (other.Cast("number", frame) as VariableNumber).Value);
        }

        public override Variable GreaterThanImpl(Variable other, Frame frame)
        {
            return new VariableBoolean(Value > (other.Cast("number", frame) as VariableNumber).Value);
        }

        public override Variable LessThanOrEqualImpl(Variable other, Frame frame)
        {
            return new VariableBoolean(Value <= (other.Cast("number", frame) as VariableNumber).Value);
        }

        public override Variable GreaterThanOrEqualImpl(Variable other, Frame frame)
        {
            return new VariableBoolean(Value >= (other.Cast("number", frame) as VariableNumber).Value);
        }

        public override Variable EqualImpl(Variable other, Frame frame)
        {
            return new VariableBoolean(Value == (other.Cast("number", frame) as VariableNumber).Value);
        }

        public override Variable NotEqualImpl(Variable other, Frame frame)
        {
            return new VariableBoolean(Value != (other.Cast("number", frame) as VariableNumber).Value);
        }

        public override Variable CallImpl(Interpreter interpreter, Frame frame)
        {
            throw new NotImplementedException();
        }
    }
}
