using System;
using BinHong.Utilities;

namespace BinHong.FlightViewerCore
{
    /// <summary>
    /// App对象。它是一个App的单例
    /// </summary>
    public class App
    {
        #region 单例
        private App()
        { }

        public static App Instance
        {
            get { return SingletonInstance; }
        }
        private static readonly App SingletonInstance = new App();

        #endregion

        /// <summary>
        /// 应用程序运行目录
        /// </summary>
        public string ApplicationDirectory { get; set; }

        /// <summary>
        /// 配置目录
        /// </summary>
        public string ConfigDirectory
        {
            get
            {
                if (_configDirectory == null)
                {
                    _configDirectory = ApplicationDirectory + "Config\\";
                    MsgScriptDirectory = ConfigDirectory + "MsgScript\\";
                }
                return _configDirectory;
            }
        }
        private string _configDirectory;

        /// <summary>
        /// 消息脚本目录
        /// </summary>
        public string MsgScriptDirectory { get; private set; }

        /// <summary>
        /// 板卡仿真器
        /// </summary>
        public readonly BoardSimulator BoardSimulator = new BoardSimulator();

        /// <summary>
        /// 参数管理
        /// </summary>
        public readonly ParameterManager Parameter = new ParameterManager();

        /// <summary>
        /// 配置管理，读取config中的信息，初始化程序
        /// </summary>
        public readonly ConfigManager ConfigManager = new ConfigManager();

        /// <summary>
        /// FlightBus管理，主要是管理板卡的一些信息，板卡的检测，登陆，移除等等
        /// </summary>
        public readonly FlightBusManager FlightBusManager = new FlightBusManager();

        /// <summary>
        /// DataLoader
        /// </summary>
        public readonly DataLoader DataLoader=new DataLoader();

        /// <summary>
        /// 程序启动前事件（界面加载前）
        /// </summary>
        public event EventHandler Starting;

        /// <summary>
        /// 程序启动后事件（界面加载后）
        /// </summary>
        public event EventHandler Started;

        /// <summary>
        /// 窗口关闭前事件（界面关闭前）
        /// </summary>
        public event EventHandler Closing;

        /// <summary>
        /// 窗口关闭后事件（界面关闭后）
        /// </summary>
        public event EventHandler Closed;

        /// <summary>
        /// 应用程序退出事件
        /// </summary>
        public event EventHandler ApplicationExist;

        /// <summary>
        /// MainThreadExist事件
        /// </summary>
        public event EventHandler MainThreadExist;

        /// <summary>
        /// 主窗口是否存在
        /// </summary>
        public Func<bool> IsMainFormExisted;

        /// <summary>
        /// 主线程同步调用事件
        /// </summary>
        public event Func<Delegate, object> InvokeEvent;

        /// <summary>
        /// 主线程异步调用事件
        /// </summary>
        public event Func<Delegate, object> BeingInvokeEvent;

        /// <summary>
        /// 主线程同步调用
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public object Invoke(Action method)
        {
            if (InvokeEvent != null
                && IsMainFormExisted != null)
            {
                if (IsMainFormExisted())
                {
                    return InvokeEvent(method);
                }
            }
            return null;
        }

        /// <summary>
        /// 主线程异步调用
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public Object BeginInvoke(Action method)
        {
            if (BeingInvokeEvent != null
                && IsMainFormExisted != null)
            {
                if (IsMainFormExisted())
                {
                    return BeingInvokeEvent(method);
                }
            }
            return null;
        }

        /// <summary>
        /// 引发程序启动前事件
        /// </summary>
        public void RaiseStarting()
        {
            if (Starting != null)
            {
                Starting(this, null);
            }
        }

        /// <summary>
        /// 引发程序启动后事件
        /// </summary>
        public void RaiseStarted()
        {
            if (Started != null)
            {
                Started(this, null);
            }
        }

        /// <summary>
        /// 引发窗口关闭前事件
        /// </summary>
        public void RaiseWindowClosing(object o, EventArgs e)
        {
            if (Closing != null)
            {
                Closing(o, e);
            }
            BhTimerManager.Dispose();
            FlightBusManager.UnInitialize();
            DataLoader.UnInitialize();
        }

        /// <summary>
        /// 引发窗口关闭后事件
        /// </summary>
        public void RaiseWindowClosed(object o, EventArgs e)
        {
            if (Closed != null)
            {
                Closed(o, e);
            }

            BoardSimulator.Save();
            Parameter.Save();
            FlightBusManager.Dispose();
            DataLoader.Dispose();
        }

        /// <summary>
        /// 引发程序退出事件
        /// </summary>
        public void RaiseApplicationExist(object o, EventArgs e)
        {
            if (ApplicationExist != null)
            {
                ApplicationExist(null, null);
            }
        }

        /// <summary>
        /// 引发主线程退出事件
        /// </summary>
        public void RaiseMainThreadExist(object o, EventArgs e)
        {
            if (MainThreadExist != null)
            {
                MainThreadExist(null, null);
            }
        }

        /// <summary>
        /// build对象
        /// todo 我希望能够控制core里面每个对象的生成时机。可能通过自定义属性与反射的方式来做。
        /// </summary>
        public void BuildModule()
        {
            FlightBusManager.BuildModule();
            DataLoader.BuildModule();
        }

        /// <summary>
        /// 初始化对象
        /// todo 我希望能够控制core里面每个对象的初始化时机
        /// </summary>
        public void Initialize()
        {
            Parameter.Initialize(ApplicationDirectory);
            ConfigManager.Initialize(ApplicationDirectory);
            BoardSimulator.Initialize(ApplicationDirectory);
            DataLoader.Initialize();
        }
    }
}
