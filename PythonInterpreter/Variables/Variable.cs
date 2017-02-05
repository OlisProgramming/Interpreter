using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.Exceptions;

namespace PythonInterpreter.Variables
{
    abstract class Variable
    {
        private string typeName;
        public string TypeName { get { return typeName; } }

        public Variable(string typeName)
        {
            this.typeName = typeName;
        }

        public Variable Add(Variable other, Frame frame)
        {
            Variable result;
            try
            {
                result = AddImpl(other, frame);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    frame,
                    "Add", TypeName);
            }

            return result;
        }

        public abstract Variable AddImpl(Variable other, Frame frame);

        public Variable Sub(Variable other, Frame frame)
        {
            Variable result;
            try
            {
                result = SubImpl(other, frame);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    frame,
                    "Sub", TypeName);
            }

            return result;
        }
        public abstract Variable SubImpl(Variable other, Frame frame);

        public Variable Mul(Variable other, Frame frame)
        {
            Variable result;
            try
            {
                result = MulImpl(other, frame);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    frame,
                    "Mul", TypeName);
            }

            return result;
        }
        public abstract Variable MulImpl(Variable other, Frame frame);

        public Variable Div(Variable other, Frame frame)
        {
            Variable result;
            try
            {
                result = DivImpl(other, frame);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    frame,
                    "Div", TypeName);
            }

            return result;
        }
        public abstract Variable DivImpl(Variable other, Frame frame);

        public Variable UnaryPlus(Frame frame)
        {
            Variable result;
            try
            {
                result = UnaryPlusImpl(frame);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    frame,
                    "UnaryPlus", TypeName);
            }

            return result;
        }
        public abstract Variable UnaryPlusImpl(Frame frame);

        public Variable UnaryMinus(Frame frame)
        {
            Variable result;
            try
            {
                result = UnaryMinusImpl(frame);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    frame,
                    "UnaryMinus", TypeName);
            }

            return result;
        }
        public abstract Variable UnaryMinusImpl(Frame frame);

        public Variable LessThan(Variable other, Frame frame)
        {
            Variable result;
            try
            {
                result = LessThanImpl(other, frame);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    frame,
                    "LessThan", TypeName);
            }

            return result;
        }
        public abstract Variable LessThanImpl(Variable other, Frame frame);

        public Variable GreaterThan(Variable other, Frame frame)
        {
            Variable result;
            try
            {
                result = GreaterThanImpl(other, frame);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    frame,
                    "GreaterThan", TypeName);
            }

            return result;
        }
        public abstract Variable GreaterThanImpl(Variable other, Frame frame);

        public Variable LessThanOrEqual(Variable other, Frame frame)
        {
            Variable result;
            try
            {
                result = LessThanOrEqualImpl(other, frame);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    frame,
                    "LessThanOrEqual", TypeName);
            }

            return result;
        }
        public abstract Variable LessThanOrEqualImpl(Variable other, Frame frame);

        public Variable GreaterThanOrEqual(Variable other, Frame frame)
        {
            Variable result;
            try
            {
                result = GreaterThanOrEqualImpl(other, frame);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    frame,
                    "GreaterThanOrEqual", TypeName);
            }

            return result;
        }
        public abstract Variable GreaterThanOrEqualImpl(Variable other, Frame frame);

        public Variable Equal(Variable other, Frame frame)
        {
            Variable result;
            try
            {
                result = EqualImpl(other, frame);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    frame,
                    "Equal", TypeName);
            }

            return result;
        }
        public abstract Variable EqualImpl(Variable other, Frame frame);

        public Variable NotEqual(Variable other, Frame frame)
        {
            Variable result;
            try
            {
                result = NotEqualImpl(other, frame);
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    frame,
                    "NotEqual", TypeName);
            }

            return result;
        }
        public abstract Variable NotEqualImpl(Variable other, Frame frame);

        ///////////////

        public Variable Cast(string typeToCast, Frame frame)
        {
            try
            {
                if (typeName != typeToCast)
                    return CastImpl(typeToCast, frame);
                return this;
            }
            catch (NotImplementedException)
            {
                throw new InterpreterException(
                    InterpreterException.InterpreterExceptionType.INTERPRETER_INVALID_OPERATION,
                    frame,
                    $"Cast({typeToCast})", TypeName);
            }
        }
        public abstract Variable CastImpl(string typeToCast, Frame frame);
    }
}
