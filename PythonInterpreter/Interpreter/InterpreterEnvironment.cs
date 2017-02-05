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
        public InterpreterEnvironment parent { get; set; }

        public InterpreterEnvironment(InterpreterEnvironment parent)
        {
            variables = new Dictionary<string, Variable>();
            this.parent = parent;
        }

        public virtual bool NameExists(string name, Frame frame)
        {
            return variables.ContainsKey(name) || parent.NameExists(name, frame);
        }

        public virtual bool NameFree(string name, Frame frame)
        {
            return !NameExists(name, frame);
        }

        public virtual void SetVariable(string name, Variable value, Frame frame)
        {
            if (variables.ContainsKey(name))
                variables[name] = value;
            else
                parent.SetVariable(name, value, frame);
        }

        public virtual void LetVariable(string name, Variable value, Frame frame)
        {
            if (variables.ContainsKey(name))
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_VARIABLE_EXISTS,
                    frame,
                    name);
            variables[name] = value;
        }

        public virtual Variable GetVariable(string name, Frame frame)
        {
            try
            {
                return (variables.ContainsKey(name)) ? variables[name] : parent.GetVariable(name, frame);
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
