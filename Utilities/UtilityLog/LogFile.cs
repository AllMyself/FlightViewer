//********************************************************
//建立日期:2015.11.25
//作者:litao
//內容说明:　LogFile主要实现的是日志文件的操作，比如装载，保存，定时保存等。

//修改日期：
//作者:
//內容说明:
//********************************************************

using System;
using System.IO;

namespace BinHong.Utilities
{
    /// <summary>
    /// 日志文件
    /// </summary>
    public class LogFile : SavingFile
    {
        /// <summary>
        /// 用于显示的队列。最大只能显示500条日志。
        /// </summary>
        public readonly RingBuffer<LogItem> ShowQueue = new RingBuffer<LogItem>(500);

        /// <summary>
        /// 更新ShowQueque列表
        /// //todo 按照最开始的想法，用它来控制不断的读取本地文件，达到可以读出全部历史日志，但是界面上永远最多只显示
        /// 500条的模式。暂时还没有做。
        /// </summary>
        /// <param name="item"></param>
        private void UpdateShowQueue(LogItem item)
        {
            if (this.ShowQueue.IsFull)
            {
                LogItem item2 = null;
                this.ShowQueue.Read(out item2);
            }
            this.ShowQueue.Write(item);
        }

        public override void AfterSave(object item)
        {
            UpdateShowQueue((LogItem)item);
        }

        /// <summary>
        /// 用日志文件路径初始化LogFile类
        /// </summary>
        /// <param name="dir">日志文件目录</param>
        public LogFile(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
          this.FilePath = dir + "Phoenix-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".his";
        }

        /// <summary>
        /// 创建一个日志记录
        /// </summary>
        public void CreateItem(LogType type, LogLevel level, string text)
        {
            LogItem item = new LogItem
            {
                Level = level,
                Text = text,
                Time = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"),
            };
            lock (this.SyncLocker)
            {
                this.SaveQueue.Write(item);
            }
        }
    }
}
