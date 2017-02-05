using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.Variables;
using PythonInterpreter.Exceptions;

namespace PythonInterpreter.InterpreterNamespace
{
    class InterpreterEnvironment
    {
        private Dictionary<string, Variable> variables;
        private Dictionary<string, Variable> variablesBuiltin;

        public InterpreterEnvironment()
        {
            variables = new Dictionary<string, Variable>();
            variablesBuiltin = new Dictionary<string, Variable>();

            variablesBuiltin.Add("true", new VariableBoolean(true));
            variablesBuiltin.Add("false", new VariableBoolean(false));
        }

        public bool NameExists(string name, Frame frame)
        {
            return variables.ContainsKey(name) || variablesBuiltin.ContainsKey(name);
        }

        public bool NameFree(string name, Frame frame)
        {
            return !NameExists(name, frame);
        }

        public void SetVariable(string name, Variable value, Frame frame)
        {
            if (variablesBuiltin.ContainsKey(name))
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_CANNOT_SET_READONLY_VARIABLE,
                    frame,
                    name);
            variables[name] = value;
        }

        public Variable GetVariable(string name, Frame frame)
        {
            try
            {
                return (variablesBuiltin.ContainsKey(name)) ? variablesBuiltin[name] : variables[name];
            }
            catch (KeyNotFoundException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_VARIABLE_DOES_NOT_EXIST,
                    frame,
                    name);
            }
        }
    }
}
