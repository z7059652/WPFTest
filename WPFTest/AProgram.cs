using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFTest.Model;

namespace WPFTest
{
    public class AProgram
    {
        public IList<Program> ProList = new List<Program>();
        protected AFileService file;
        public IList<Program> LoadProgram(IList<Program> list = null)
        {
            file.LoadProgram(ref this.ProList);
            return this.ProList;
        }
        public bool SaveProgram()
        {
            return file.SaveProgram(this.ProList);
        }
    }
}
