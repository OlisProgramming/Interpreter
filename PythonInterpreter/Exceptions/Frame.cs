using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonInterpreter.Exceptions
{
    class Frame
    {
        private string fileName;
        public string FileName { get { return fileName; } }
        private int line;
        public int Line { get { return line; } }
        private int column;
        public int Column { get { return column; } }

        private Frame previousFrame;
        public Frame PreviousFrame { get { return previousFrame; } }

        public Frame(int line, int column, string fileName, Frame previousFrame)
        {
            this.line = line;
            this.column = column;
            this.fileName = fileName;
            this.previousFrame = previousFrame;
        }
    }
}
