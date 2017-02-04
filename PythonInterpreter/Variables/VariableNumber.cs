using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override Variable AddImpl(Variable other)
        {
            VariableNumber otherDouble = other.Cast("number") as VariableNumber;
            return new VariableNumber(Value + otherDouble.Value);
        }

        public override Variable SubImpl(Variable other)
        {
            VariableNumber otherDouble = other.Cast("number") as VariableNumber;
            return new VariableNumber(Value - otherDouble.Value);
        }

        public override Variable MulImpl(Variable other)
        {
            VariableNumber otherDouble = other.Cast("number") as VariableNumber;
            return new VariableNumber(Value * otherDouble.Value);
        }

        public override Variable DivImpl(Variable other)
        {
            VariableNumber otherDouble = other.Cast("number") as VariableNumber;
            return new VariableNumber(Value / otherDouble.Value);
        }

        public override Variable CastImpl(string typeToCast)
        {
            if (typeToCast == "boolean") return new VariableBoolean(Value != 0);

            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
