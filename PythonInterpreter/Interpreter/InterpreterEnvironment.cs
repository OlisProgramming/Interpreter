using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.Variables;

namespace PythonInterpreter.InterpreterNamespace
{
    class InterpreterEnvironment
    {
        private Dictionary<string, Variable> variables;

        public InterpreterEnvironment()
        {
            variables = new Dictionary<string, Variable>();
        }

        public bool NameExists(string name)
        {
            return variables.ContainsKey(name);
        }

        public bool NameFree(string name)
        {
            return !NameExists(name);
        }

        public void SetVariable(string name, Variable value)
        {
            variables[name] = value;
        }

        public Variable GetVariable(string name)
        {
            return variables[name];
        }
    }
}
