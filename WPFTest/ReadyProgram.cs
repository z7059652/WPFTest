﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFTest.Model;
namespace WPFTest
{
    public class ReadyProgram:AProgram
    {
        public ReadyProgram()
        {
            file = new ReadyFileService("default.ini");
        }
    }
}
