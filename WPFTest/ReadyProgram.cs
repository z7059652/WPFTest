using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFTest.Model;
namespace WPFTest
{
    public class ReadyProgram:AProgram
    {
        private FileService file = new FileService();
        public override IList<Program> LoadProgram()
        {
            file.LoadProgram(ref this.ProList);
            return this.ProList;
        }
        public override bool SaveProgram()
        {
            return file.SaveProgram(this.ProList);
        }
    }
}
