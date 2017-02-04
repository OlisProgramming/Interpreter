using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter
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
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
