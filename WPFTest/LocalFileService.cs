using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFTest
{
    public class LocalFileService:AFileService
    {
        public LocalFileService(string filename)
        {
            file = path + "\\" + filename;
        }
    }
}
