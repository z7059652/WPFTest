using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFTest.Model;

namespace WPFTest
{
    public class LocalProgram:AProgram
    {
        public override IList<Program> LoadProgram()
        {
            return this.ProList;             
        }
        public override bool SaveProgram()
        {
            return true;
        }
        public LocalProgram()
        {
            
        }
    }
}
