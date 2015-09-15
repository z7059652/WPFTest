using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFTest.Model;

namespace WPFTest
{
    public abstract class AProgram
    {
        public IList<Program> ProList = new List<Program>();
        public abstract IList<Program> LoadProgram();
        public abstract bool SaveProgram();

    }
}
