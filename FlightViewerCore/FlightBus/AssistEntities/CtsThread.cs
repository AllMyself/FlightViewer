using System;
using System.Threading;
using BinHong.Utilities;

namespace BinHong.FlightViewerCore
{
    /// <summary>
    /// 线程
    /// </summary>
    public class CstThread
    {
        /// <summary>
        /// 线程
        /// </summary>
        private readonly Thread _thread;


        public CstThread()
        {
            _thread = new Thread(OnThread);
            _thread.IsBackground = true;
            _thread.Start();
        }
        /// <summary>
        /// 线程响应
        /// </summary>
        /// <param name="stateInfo"></param>
        private void OnThread(object stateInfo)
        {
            while (true)
            {
                Thread.Sleep(100);
                Action act = ThreadEvent;
                if (act != null)
                {
                    try
                    {
                        act();
                    }
                    catch (Exception ex)
                    {
                        RunningLog.Record(ex.Message + "\n" + ex.StackTrace);
                    }
                }
            }
        }

        /// <summary>
        /// 线程事件
        /// </summary>
        public event Action ThreadEvent;
    }
}
