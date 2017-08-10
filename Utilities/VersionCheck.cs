using System;
using Microsoft.Win32;

namespace BinHong.Utilities
{
    /// <summary>
    /// 版本检测
    /// 关于安装的时候，检测电脑是否安装.net环境的检测方法，详见https://msdn.microsoft.com/zh-cn/library/hh925568(v=vs.110).aspx
    //这里是检测当前正在执行代码的运行时版本的 Version 对象
    /// </summary>
    public static class VersionCheck
    {
        // Checking the version using >= will enable forward compatibility, 
        // however you should always compile your code on newer versions of
        // the framework to ensure your app works the same.
        private static string CheckFor45DotVersion(int releaseKey)
        {
            if (releaseKey >= 393295)
            {
                return "4.6 or later";
            }
            if ((releaseKey >= 379893))
            {
                return "4.5.2 or later";
            }
            if ((releaseKey >= 378675))
            {
                return "4.5.1 or later";
            }
            if ((releaseKey >= 378389))
            {
                return "4.5 or later";
            }
            // This line should never execute. A non-null release key should mean
            // that 4.5 or later is installed.
            return "No 4.5 or later version detected";
        }

        /// <summary>
        /// 检测本地是否安装了.net4.5及以后的版本。
        /// </summary>
        /// <returns></returns>
        public static bool Check45orLaterFromRegistry()
        {
            Console.WriteLine(Environment.Version);
            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                int releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));
                if (CheckFor45DotVersion(releaseKey).StartsWith("No 4.5"))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 检测本地是否安装了.net4.0版本。
        /// </summary>
        /// <returns></returns>
        public static bool Check4FromRegistry()
        {
            string version = Environment.Version.ToString();
            if (version.StartsWith("4"))
            {
                return true;
            }
            return false;
        }
    }
}
