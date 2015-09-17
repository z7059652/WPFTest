using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using WPFTest.ExtendMethod;
using System.Windows.Media;

namespace WPFTest.Model
{
    public class Program
    {
        public Program(bool flag,string name,string path)
        {
            IsSelect = flag;
            sName = name;
            sPath = path;
            icon = path.ToImageSource();
        }

        public Program(Program item)
        {
            // TODO: Complete member initialization
//            this.item = item;
            IsSelect = item.IsSelect;
            sName = string.Copy(item.sName);
            sPath = string.Copy(item.sPath);
            icon = item.Icon;
        }
        bool IsSelect;
        string sName;
        string sPath;
        ImageSource icon;
        private Program item;
        public ImageSource Icon
        {
            set
            {
                icon = value;
            }
            get
            {
                return icon;
            }
        }
        public bool Select
        {
            set
            {
                IsSelect = value;
            }
            get
            {
                return IsSelect;
            }
        }
        public string Name
        {
            set
            {
                sName = value;
            }
            get
            {
                return sName;
            }
        }
        public string Path
        {
            set
            {
                sPath = value;
            }
            get
            {
                return sPath;
            }
        }
        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;
            try
            {
                Program b = (Program)obj;
                return this.Path.Equals(b.Path);
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
