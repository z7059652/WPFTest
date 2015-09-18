using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFTest.Model;

namespace WPFTest
{
    public class LocalProgram:AProgram
    {
        RegistryKey pregkey = Registry.LocalMachine.OpenSubKey(@"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\App Paths");//获取指定路径下的键           
        public override IList<Program> LoadProgram(IList<Program> list = null)
        {
            LoadProgram();
            foreach (Program p in list)
            {
                if (ProList.Contains(p))
                    ProList.Remove(p);
            }
            return ProList;
        }
        public IList<Program> LoadProgram()
        {

            string displayName = null;
            try
            {
                foreach (string item in pregkey.GetSubKeyNames())
                {
                    string name = item.Substring(0, item.Length - 4);
                    RegistryKey subkey = pregkey.OpenSubKey(item);
                    displayName = subkey.GetValue("") as string;
                    if (displayName != null)
                    {
                        Program temp = new Program(false, name, displayName);
                        if (!ProList.Contains(temp))
                            ProList.Add(temp);
                    }
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                //                continue;
                return ProList;
            }
            return this.ProList;
    

        }
        public override bool SaveProgram()
        {
            return true;
        }
        public LocalProgram()
        {
            ProList = new List<Program>();
        }
    }
}
