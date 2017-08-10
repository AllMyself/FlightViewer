using System.Diagnostics;
using System.Security.Principal;

namespace BinHong.Utilities
{
    /// <summary>
    /// windows权限检测，处理工具
    /// </summary>
    public static class WindowsAuthority
    {
        /// <summary>
        /// 是否是管理员
        /// </summary>
        public static bool IsAdministrator
        {
            get
            {
                WindowsIdentity identity =WindowsIdentity.GetCurrent();
                if (identity != null)
                {
                    var principal =new WindowsPrincipal(identity);
                    return principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
                return false;
            }
        }

        /// <summary>
        /// 以管理员身份运行
        /// </summary>
        public static void RunAsAdministrator(string exePath)
        {
            //创建启动对象
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.FileName = exePath;
            //设置启动动作,确保以管理员身份运行
            startInfo.Verb = "runas";
            try
            {
                Process.Start(startInfo);
            }
            catch
            {
            }
        }
    }
}
