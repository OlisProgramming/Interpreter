using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PythonInterpreter.InterpreterNamespace;
using PythonInterpreter.SyntaxTrees;
using PythonInterpreter.Exceptions;

namespace PythonInterpreter.Variables
{
    class VariableCallable : Variable
    {
        private StatementListNode procedure;
        public StatementListNode Procedure { get { return procedure; } }

        public VariableCallable(StatementListNode procedure) : base("callable")
        {
            this.procedure = procedure;
        }

        public override Variable AddImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable CallImpl(Interpreter interpreter, Frame frame)
        {
            return interpreter.Visit(Procedure, frame);
        }

        public override Variable CastImpl(string typeToCast, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable DivImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable EqualImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable GreaterThanImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable GreaterThanOrEqualImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable LessThanImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable LessThanOrEqualImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable MulImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable NotEqualImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable SubImpl(Variable other, Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable UnaryMinusImpl(Frame frame)
        {
            throw new NotImplementedException();
        }

        public override Variable UnaryPlusImpl(Frame frame)
        {
            throw new NotImplementedException();
        }
    }
}
