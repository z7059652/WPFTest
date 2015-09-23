using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Windows;
using WPFTest.Model;

namespace WPFTest
{
    public class LanuchProcess
    {
       public bool Start(string path)
       {
            Process p = System.Diagnostics.Process.Start(path);
            return true;
       }
       public bool Start(IList<Program> prolist)
       {
           foreach(Program p in prolist)
           {
               Start(p.Path);
           }
           return true;
       }
    }
}
