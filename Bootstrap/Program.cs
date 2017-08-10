//********************************************************
//建立日期:2015.11.1
//作者:litao
//內容说明:　应用程序启动

//修改日期：
//作者:
//內容说明:
//********************************************************

using System;
using System.Threading;
using System.Windows.Forms;
using BinHong.FlightViewerCore;
using BinHong.Utilities;
using CommonInterface;

namespace BinHong.Bootstrap
{
    class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //检测.net版本。
            if (!VersionCheck.Check4FromRegistry())
            {
                MessageBox.Show(".net版本过低。本程序需要运行在.net4.0或.net4.0以后的版本！", "提示");
                return;
            }
            App.Instance.ApplicationDirectory = Environment.CurrentDirectory + "\\";

            //同时只能运行一个实例。
            if (SingletonApp.IsMutexExsited)
            {
                SingletonApp.ShowRunningInstance();
                return;
            }
            //指定运行日志要保存的目录
            RunningLog.Start(App.Instance.ApplicationDirectory+"\\history\\");

            //判断当前登录用户是否为管理员
            if (WindowsAuthority.IsAdministrator)
            {
                //设置应用程序退出和异常的事件
                Application.ApplicationExit += App.Instance.RaiseApplicationExist;
                Application.ThreadExit += App.Instance.RaiseMainThreadExist;
                Application.ThreadException += (o, e) => MessageBox.Show(e.Exception.ToString());

                try
                {
                    //运行应用程序内核
                    App.Instance.BuildModule();
                    App.Instance.Initialize();
                    App.Instance.RaiseStarting();
                    
                    //启动Ui界面
                    IInvoke invoker = ObjectGenerator.Create("FlightViewerUI.dll", "BinHong.FlightViewerUI.UiInvoker") as IInvoke;
                    invoker.Invoke();
                }
                catch (Exception e)
                {
                    string msg = e.Message;
#if DEBUG
                    msg = e.Message + e.StackTrace;
#endif
                    RunningLog.Record(LogType.System, LogLevel.Error, msg);
                    Thread.Sleep(200);
                    MessageBox.Show(msg);
                }
                finally
                {
                    SingletonApp.ReleaseMutex();
                }
            }
            else
            {
                WindowsAuthority.RunAsAdministrator(Application.ExecutablePath);
            }
        }
    }
}

