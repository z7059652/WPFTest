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
        const string fileHead = "# this is a file of configuration for app manger,please don't change it, every line begin with # is comment....";
        const string filename = "default.ini";
        string path = System.IO.Directory.GetCurrentDirectory();
        string file = null;
        public FileService()
        {
            file = path + "\\" + filename;
        }
        public bool LoadProgram(ref IList<Program> ProList)
        {
            if (!File.Exists(file))
                return true;
            StreamReader sr = new StreamReader(file, Encoding.Default); 
            return true;
        }
        public bool SaveProgram(IList<Program> ProList)
        {
            FileStream fs = new FileStream(file,FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            return true;
        }
    }
}
