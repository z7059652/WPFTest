using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFTest.Model;
using System.IO;

namespace WPFTest
{
    public class FileService
    {
        const string filename = "default.ini";
        string path = System.IO.Directory.GetCurrentDirectory();
        string file = Path + "\\" + filename;
        public bool LoadProgram(ref IList<Program> ProList)
        {
            StreamReader sr = new StreamReader(file, Encoding.Default); 
            return true;
        }
        public bool SaveProgram(IList<Program> ProList)
        {
            return true;
        }
    }
}
