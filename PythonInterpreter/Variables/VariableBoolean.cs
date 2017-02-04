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
            throw new NotImplementedException();
        }

        public override Variable SubImpl(Variable other)
        {
            throw new NotImplementedException();
        }

        public override Variable MulImpl(Variable other)
        {
            throw new NotImplementedException();
        }

        public override Variable DivImpl(Variable other)
        {
            throw new NotImplementedException();
        }

        public override Variable CastImpl(string typeToCast)
        {
            if (typeToCast == "number")
                return new VariableNumber(Value ? 1 : 0);

            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Value ? "true" : "false";
        }

        public override Variable UnaryPlusImpl(Variable other)
        {
            throw new NotImplementedException();
        }

        public override Variable UnaryMinusImpl(Variable other)
        {
            throw new NotImplementedException();
        }

        public override Variable LessThanImpl(Variable other)
        {
            throw new NotImplementedException();
        }

        public override Variable GreaterThanImpl(Variable other)
        {
            throw new NotImplementedException();
        }

        public override Variable LessThanOrEqualImpl(Variable other)
        {
            throw new NotImplementedException();
        }

        public override Variable GreaterThanOrEqualImpl(Variable other)
        {
            throw new NotImplementedException();
        }

        public override Variable EqualImpl(Variable other)
        {
            return new VariableBoolean(Value == (other.Cast("boolean") as VariableBoolean).Value);
        }

        public override Variable NotEqualImpl(Variable other)
        {
            return new VariableBoolean(Value != (other.Cast("boolean") as VariableBoolean).Value);
        }
    }
}
