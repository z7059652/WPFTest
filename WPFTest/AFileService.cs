using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WPFTest.Model;

namespace WPFTest
{
    public class AFileService
    {
        protected const string fileHead = "# this is a file of configuration for app manger,please don't change it, every line begin with # is comment....\r\n##########Name########path";
        protected string file;
        protected string path = System.IO.Directory.GetCurrentDirectory();
        public bool LoadProgram(ref IList<Program> ProList)
        {
            if (!File.Exists(file))
                return true;
            StreamReader sr = new StreamReader(file, Encoding.Default);
            string temp = null;
            while ((temp = sr.ReadLine()) != null)
            {
                if (temp.Trim().StartsWith("#"))
                    continue;
                string[] arrline = temp.Trim().Split("=".ToCharArray());
                if (arrline.Length < 2)
                    continue;
                string name = arrline[0];
                string path = arrline[1];
                ProList.Add(new Program(name, path));
            }
            sr.Close();
            return true;

        }
        public bool SaveProgram(IList<Program> ProList)
        {
            try
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                FileStream fs = new FileStream(file, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(fileHead);
                foreach (Program p in ProList)
                {
                    string temp = p.Name + "=" + p.Path;
                    sw.WriteLine(temp);
                }
                sw.Close();
                fs.Close();
                return true;
            }
            catch (Exception e)
            {
                return true;
            }

        }
    }
}
