using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Windows;

namespace WPFTest
{
    public class LanuchProcess
    {
        const int MAXIMUM_ALLOWED = 0x02000000;
        private int dwSessionID;
        public LanuchProcess()
        {
            dwSessionID = -1;
            getSessionId();
        }
        private void getSessionId()
        {
            Process[] processes = Process.GetProcessesByName("explorer");
            foreach(Process p in processes)
            {
                dwSessionID = p.SessionId;
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public Int32 dwProcessID;
            public Int32 dwThreadID;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public Int32 Length;
            public IntPtr lpSecurityDescriptor;
            public bool bInheritHandle;
        }

        public enum SECURITY_IMPERSONATION_LEVEL
        {
            SecurityAnonymous,
            SecurityIdentification,
            SecurityImpersonation,
            SecurityDelegation
        }

        public enum TOKEN_TYPE
        {
            TokenPrimary = 1,
            TokenImpersonation
        }

        public const int GENERIC_ALL_ACCESS = 0x10000000;

        [DllImport("kernel32.dll", SetLastError = true,
            CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("advapi32.dll", SetLastError = true,
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CreateProcessAsUser(
            IntPtr hToken,
            string lpApplicationName,
            string lpCommandLine,
            ref SECURITY_ATTRIBUTES lpProcessAttributes,
            ref SECURITY_ATTRIBUTES lpThreadAttributes,
            bool bInheritHandle,
            Int32 dwCreationFlags,
            IntPtr lpEnvrionment,
            string lpCurrentDirectory,
            ref STARTUPINFO lpStartupInfo,
            ref PROCESS_INFORMATION lpProcessInformation);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool DuplicateTokenEx(
            IntPtr hExistingToken,
            Int32 dwDesiredAccess,
            ref SECURITY_ATTRIBUTES lpThreadAttributes,
            Int32 ImpersonationLevel,
            Int32 dwTokenType,
            ref IntPtr phNewToken);

        [DllImport("wtsapi32.dll", SetLastError = true)]
        public static extern bool WTSQueryUserToken(
            Int32 sessionId,
            out IntPtr Token);

        [DllImport("userenv.dll", SetLastError = true)]
        static extern bool CreateEnvironmentBlock(
            out IntPtr lpEnvironment,
            IntPtr hToken,
            bool bInherit);
        public bool Start(string app, string path)
        {
            bool result;
            try
            {
                IntPtr hToken = WindowsIdentity.GetCurrent().Token;
                IntPtr hDupedToken = IntPtr.Zero;

                PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
                SECURITY_ATTRIBUTES sa = new SECURITY_ATTRIBUTES();
                sa.Length = Marshal.SizeOf(sa);

                STARTUPINFO si = new STARTUPINFO();
                si.cb = Marshal.SizeOf(si);

                result = DuplicateTokenEx(hToken, (0x02000000L), null, 2, 1,ref hToken);

//                result = WTSQueryUserToken(dwSessionID, out hToken);
                if(!result)
                {
                    int error = Marshal.GetLastWin32Error();
                    string message = String.Format("WTSQueryUserToken Error: {0}", error);
                    throw new Exception(message);
                }

                IntPtr lpEnvironment = IntPtr.Zero;
                result = CreateEnvironmentBlock(out lpEnvironment, hDupedToken, false);
                if (!result)
                {
                    throw new Exception("CreateEnvironmentBlock failed");
                }

                result = CreateProcessAsUser(
                         hDupedToken,
                         app,
                         String.Empty,
                         ref sa, ref sa,
                         false, 0, IntPtr.Zero,
                         path, ref si, ref pi);

                if (!result)
                {
                    int error = Marshal.GetLastWin32Error();
                    string message = String.Format("CreateProcessAsUser Error: {0}", error);
                    throw new Exception(message);
                }

                if (pi.hProcess != IntPtr.Zero)
                    CloseHandle(pi.hProcess);
                if (pi.hThread != IntPtr.Zero)
                    CloseHandle(pi.hThread);
                if (hDupedToken != IntPtr.Zero)
                    CloseHandle(hDupedToken);
            
                return true;

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            
        }
    }
}
