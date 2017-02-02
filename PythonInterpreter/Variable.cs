using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter
{
    abstract class Variable
    {
        public Variable Add(Variable other)
        {
            return AddImpl(other);
        }

        public abstract Variable AddImpl(Variable other);
    }
}
