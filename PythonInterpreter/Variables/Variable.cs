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
        public string TypeName { get; }

        public Variable(string typeName)
        {
            this.typeName = typeName;
        }

        public Variable Add(Variable other)
        {
            Variable this_result = AddImpl(other);  // Should return null if code path is unavailable.
            Variable result = this_result ?? other.AddImpl(this);

            return result;
        }
        public static Variable operator +(Variable self, Variable other)
        {
            return self.Add(other);
        }
        public abstract Variable AddImpl(Variable other);

        public Variable Sub(Variable other)
        {
            Variable this_result = SubImpl(other);
            Variable result = this_result ?? other.SubImpl(this);

            return result;
        }
        public static Variable operator -(Variable self, Variable other)
        {
            return self.Sub(other);
        }
        public abstract Variable SubImpl(Variable other);

        public Variable Mul(Variable other)
        {
            Variable this_result = MulImpl(other);
            Variable result = this_result ?? other.MulImpl(this);

            return result;
        }
        public static Variable operator *(Variable self, Variable other)
        {
            return self.Mul(other);
        }
        public abstract Variable MulImpl(Variable other);

        public Variable Div(Variable other)
        {
            Variable this_result = DivImpl(other);
            Variable result = this_result ?? other.DivImpl(this);

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
            if (typeName != typeToCast)
                return CastImpl(typeToCast);
            return this;
        }
        public abstract Variable CastImpl(string typeToCast);
    }
}
