using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFTest.Model;

namespace WPFTest
{
    public class HandleProgramService
    {
        private static HandleProgramService _instance = new HandleProgramService();
        public bool MoveTo(IList<Program> src, IList<Program> des, IList<Program> items)
        {
            foreach(Program item in items)
            {
                src.Remove(item);
                des.Add(item);
            }
            return true;
        }
        public void RefreshSelected(ref IList<Program> list, System.Collections.IList SelectItems)
        {
            list.Clear();
            foreach (Program item in SelectItems)
            {
                Program temp = new Program(item);
                list.Add(temp);
            }
        }

        public static HandleProgramService INST
        {
            get
            {
                return _instance;
            }
        }
    }
}
