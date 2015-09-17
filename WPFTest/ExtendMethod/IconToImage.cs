using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Interop;

namespace WPFTest.ExtendMethod
{
    public static class IconToImage
    {
        public static string FileDirectory = System.IO.Directory.GetCurrentDirectory()+"\\ICON";
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };
        class Win32
        {
            public const uint SHGFI_ICON = 0x100;
            public const uint SHGFI_LARGEICON = 0x0; // 'Large icon
            public const uint SHGFI_SMALLICON = 0x1; // 'Small icon
            [DllImport("shell32.dll")]
            public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
            [DllImport("shell32.dll")]
            public static extern uint ExtractIconEx(string lpszFile, int nIconIndex, int[] phiconLarge, int[] phiconSmall, uint nIcons);
        }
        public static string SaveToFile(this Image image,string name)
        {
            try 
            {
                if (Directory.Exists(FileDirectory) == false)
                {
                    Directory.CreateDirectory(FileDirectory);
                }
                if (!File.Exists(FileDirectory + "\\" + name))
                {
                    image.Save(FileDirectory + "\\" + name);
                }
                return FileDirectory + "\\" + name;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public static string ToImage(this string path,string name)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            path = path.Trim();
            if(path.StartsWith("\""))
            {
                path = path.Substring(1,path.Length - 2);
            }
            try
            {
                Win32.SHGetFileInfo(path, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
                Icon myIcon = Icon.FromHandle(shinfo.hIcon);
                Image myImage = Image.FromHbitmap(myIcon.ToBitmap().GetHbitmap());
                return myImage.SaveToFile(name + ".png");
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public static ImageSource ToImageSource(this string path)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            Win32.SHGetFileInfo(path, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
            Icon myIcon = Icon.FromHandle(shinfo.hIcon);
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(myIcon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            return imageSource; 
        }
    }
}
