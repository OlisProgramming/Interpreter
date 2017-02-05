using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.Variables;
using PythonInterpreter.Exceptions;

namespace PythonInterpreter.InterpreterNamespace
{
    class InterpreterEnvironmentRoot : InterpreterEnvironment
    {
        private Dictionary<string, Variable> variables;

        public InterpreterEnvironmentRoot() : base(null)
        {
            variables = new Dictionary<string, Variable>();
            variables.Add("true", new VariableBoolean(true));
            variables.Add("false", new VariableBoolean(false));
        }

        public override bool NameExists(string name, Frame frame)
        {
            return variables.ContainsKey(name);
        }

        public override bool NameFree(string name, Frame frame)
        {
            return !NameExists(name, frame);
        }

        public override void SetVariable(string name, Variable value, Frame frame)
        {
            if (variables.ContainsKey(name))
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_CANNOT_SET_READONLY_VARIABLE,
                    frame,
                    name);
            }
            else
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_VARIABLE_DOES_NOT_EXIST,
                    frame,
                    name);
            }
        }

        public override Variable GetVariable(string name, Frame frame)
        {
            try
            {
                return variables[name];
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
