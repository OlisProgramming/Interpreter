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

        public bool NameExists(string name)
        {
            return variables.ContainsKey(name) || variablesBuiltin.ContainsKey(name);
        }

        public bool NameFree(string name)
        {
            return !NameExists(name);
        }

        public void SetVariable(string name, Variable value)
        {
            if (variablesBuiltin.ContainsKey(name))
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_CANNOT_SET_READONLY_VARIABLE,
                    new TokeniserNamespace.Token(TokeniserNamespace.Token.TokenType.EOF, ""),
                    name);
            variables[name] = value;
        }

        public Variable GetVariable(string name)
        {
            return (variablesBuiltin.ContainsKey(name))? variablesBuiltin[name] : variables[name];
        }
    }
}
