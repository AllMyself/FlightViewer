using System;
using System.Collections.Generic;
using System.Timers;

namespace BinHong.Utilities
{
    /// <summary>
    /// 自定义定时器
    /// </summary>
    public class UtilityTimer:IDisposable
    {
        /// <summary>
        /// 定时器的名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 定时器
        /// </summary>
        private readonly Timer _timer = new Timer();

        /// <summary>
        /// elapsed字典
        /// </summary>
        private readonly Dictionary<string, ElapsedEventHandler> _elapsedDic = new Dictionary<string, ElapsedEventHandler>();

        /// <summary>
        /// 响应事件
        /// </summary>
        private event ElapsedEventHandler Elapsed;

        /// <summary>
        /// 是否正处于Elapsed的过程中
        /// </summary>
        private bool _isInElapsed = false;

        /// <summary>
        /// 保证单线运行的锁。
        /// </summary>
        private readonly object _singleLocker = new object();

        private void OnElapsed(object o, ElapsedEventArgs e)
        {
            lock (_singleLocker)
            {
                //如果Elapsed正在执行，不能进入。
                if (_isInElapsed == true)
                {
                    return;
                }
                //如果Elapsed已经执行完，可以设置_isInElapsed状态为true，进入Elapsed
                _isInElapsed = true;
            }

            try
            {
                if (Elapsed != null)
                {
                    Elapsed(o, e);
                }
            }
            catch (Exception ex)
            {
                UtilityTrace.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }

            //如果Elapsed已经执行完，设置_isInElapsed为false
            lock (_singleLocker)
            {
                _isInElapsed = false;
            }
        }

        public UtilityTimer(string name, int interval)
        {
            Name = name;
            _timer.Interval = interval;
            _timer.Elapsed += OnElapsed;
        }

        /// <summary>
        /// 添加Elapsed
        /// </summary>
        /// <param name="elapsedName"></param>
        /// <param name="elapsed"></param>
        public void AddElapsedEventHandler(string elapsedName, ElapsedEventHandler elapsed)
        {
            if (!_elapsedDic.ContainsKey(elapsedName))
            {
                _elapsedDic.Add(elapsedName, elapsed);
                Elapsed += elapsed;
            }
            if (_elapsedDic.Count==1)
            {
                _timer.Start();
            }
        }

        /// <summary>
        /// 根据ElapsedName移除某个Elapsed
        /// </summary>
        /// <param name="elapsedName"></param>
        public void RemoveElapsedEventHandler(string elapsedName)
        {
            if (_elapsedDic.ContainsKey(elapsedName))
            {
                ElapsedEventHandler elapsed = _elapsedDic[elapsedName];
                Elapsed -= elapsed;
                _elapsedDic.Remove(elapsedName);
            }
            if (_elapsedDic.Count == 0)
            {
                _timer.Stop();
            }
        }

        /// <summary>
        /// 定时器Dispose
        /// </summary>
        public void Dispose()
        {
            if (_timer.Enabled)
            {
                _timer.Close();
            }
            _timer.Dispose();
        }
    }

    /// <summary>
    /// 定时器
    /// </summary>
    public static class BhTimerManager
    {
        public static void Dispose()
        {
            Timer200.Dispose();
            Timer1000.Dispose();
            Timer50.Dispose();
        }

        /// <summary>
        /// 50毫秒定时器
        /// </summary>
        public static UtilityTimer Timer50 = new UtilityTimer("interval50", 50);

        /// <summary>
        /// 200毫秒定时器
        /// </summary>
        public static UtilityTimer Timer200 = new UtilityTimer("interval200", 200);

        /// <summary>
        /// 1000毫秒定时器
        /// </summary>
        public static UtilityTimer Timer1000 = new UtilityTimer("interval1000", 1000);
    }
}
