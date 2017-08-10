using System;
using System.Threading;

namespace BinHong.Utilities
{
    public class DelayAction
    {
        public event Action<Action> BeginInvoke;

        /// <summary>
        /// 延时调用某些行为。
        /// </summary>
        /// <param name="millisecond">延迟的事件ms</param>
        /// <param name="act">要执行的行为</param>
        public void Run(int millisecond, Action act)
        {
            DelayInfo delayInfo = new DelayInfo(act, millisecond);
            Thread thread = new Thread(OnThreadProcess);
            thread.IsBackground = true;
            thread.Start(delayInfo);
        }

        private void OnThreadProcess(object obj)
        {
            DelayInfo delayInfo = (DelayInfo) obj;
            Thread.Sleep(delayInfo.Millisecond);
            if (BeginInvoke != null)
            {
                BeginInvoke(delayInfo.Act);
            }
            else
            {
                delayInfo.Act();
            }
        }

        private struct DelayInfo
        {
            public readonly Action Act;

            public readonly int Millisecond;

            public DelayInfo(Action act, int millisecond)
                : this()
            {
                this.Act = act;
                this.Millisecond = millisecond;
            }
        }
    }

    /// <summary>
    /// 主线程延迟调用行为
    /// </summary>
    public static class MainThreadDelayAction
    {
        private static readonly DelayAction DelayAction=new DelayAction();

        private static bool _isInitialized = false;

        public static void Initialize(Action<Action> action)
        {
            if (_isInitialized)
            {
                return;
            }
            DelayAction.BeginInvoke += action;
            _isInitialized = true;
        }

        public static void Run(int millisecond, Action act)
        {
            if (!_isInitialized)
            {
                return;
            }
            DelayAction.Run(millisecond, act);
        }
    }

    /// <summary>
    /// 后台线程延迟调用行为
    /// </summary>
    public static class BackThreadDelayAction
    {
        private static readonly DelayAction DelayAction = new DelayAction();

        public static void Run(int millisecond, Action act)
        {
            DelayAction.Run(millisecond, act);
        }
    }
}
