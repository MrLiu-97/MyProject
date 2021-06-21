// Decompiled with JetBrains decompiler
// Type: dangdangSDK.Util.WinrarHelper
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: C:\Users\TaoQu\Desktop\dangdangSDK.dll

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace dangdangSDK.Util
{
    public class WinrarHelper
    {
        public static bool RAR(string path, string rarPath, string rarName)
        {
            bool flag = false;
            try
            {
                RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("Applications\\WinRAR.exe\\shell\\open\\command") ?? Registry.ClassesRoot.OpenSubKey("WinRAR\\shell\\open\\command");
                string str1 = registryKey.GetValue("").ToString();
                registryKey.Close();
                string str2 = str1.Substring(1, str1.Length - 7);
                Directory.CreateDirectory(path);
                string str3 = string.Format("a {0} {1} -r -ep", rarName, path);
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = str2;
                processStartInfo.Arguments = str3;
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processStartInfo.WorkingDirectory = rarPath;
                Process process = new Process();
                process.StartInfo = processStartInfo;
                process.Start();
                process.WaitForExit();
                if (process.HasExited)
                    flag = true;
                process.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        public static bool UnRAR(string path, string rarPath, string rarName)
        {
            bool flag = false;
            try
            {
                RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("Applications\\WinRAR.exe\\shell\\open\\command");
                string str1 = registryKey.GetValue("").ToString();
                registryKey.Close();
                string str2 = str1.Substring(1, str1.Length - 7);
                Directory.CreateDirectory(path);
                string str3 = string.Format("x {0} {1} -y", rarName, path);
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = str2;
                processStartInfo.Arguments = str3;
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processStartInfo.WorkingDirectory = rarPath;
                Process process = new Process();
                process.StartInfo = processStartInfo;
                process.Start();
                process.WaitForExit();
                if (process.HasExited)
                    flag = true;
                process.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }
    }
}
