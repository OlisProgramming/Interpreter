using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter
{
    class InterpreterEnvironment
    {
        private Dictionary<string, double> variablesNumeric;

        public InterpreterEnvironment()
        {
            variablesNumeric = new Dictionary<string, double>();
        }

        public bool NameExists(string name)
        {
            return variablesNumeric.ContainsKey(name);
        }

        public bool NameFree(string name)
        {
            return !NameExists(name);
        }

        public void SetVariableNumeric(string name, double value)
        {
            variablesNumeric[name] = value;
        }

        public double GetVariableNumeric(string name)
        {
            return variablesNumeric[name];
        }
    }
}
