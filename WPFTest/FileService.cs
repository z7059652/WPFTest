using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFTest.Model;

namespace WPFTest
{
    public class FileService
    {
        const string filename = "default.ini";
        string path = System.IO.Directory.GetCurrentDirectory();
        public bool LoadProgram(ref IList<Program> ProList)
        {
            return true;
        }
        public bool SaveProgram(IList<Program> ProList)
        {
            return true;
        }
    }
}
