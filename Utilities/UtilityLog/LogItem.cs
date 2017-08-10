//********************************************************
//建立日期:2015.11.25
//作者:litao
//內容说明:　主要对一条日志进行定义，定义了日志项，日志级别，日志类型
//修改日期：
//作者:
//內容说明:
//********************************************************

using System;
using System.Diagnostics;

namespace BinHong.Utilities
{
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevel : byte
    {
        /// <summary>
        /// 信息
        /// </summary>
        Information = 0,

        /// <summary>
        /// 警告
        /// </summary>
        Warning = 1,

        /// <summary>
        /// 错误
        /// </summary>
        Error = 2
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType:byte
    {
        /// <summary>
        /// 操作日志
        /// </summary>
        Operate,

        /// <summary>
        /// 过程日志
        /// </summary>
        Process,

        /// <summary>
        /// 系统日志
        /// </summary>
        System,

        /// <summary>
        /// 提示类型
        /// </summary>
        Tip,

        /// <summary>
        /// Debug类型
        /// </summary>
        Debug

    }

    /// <summary>
    /// 日志项（条目）
    /// </summary>
    public class LogItem
    {
        /// <summary>
        /// 日志产生的时间
        /// </summary>
        public string Time { get; internal set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public LogType Type { get; internal set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel Level { get; internal set; }

        /// <summary>
        /// 日志的内容
        /// </summary>
        public string Text { get; internal set; }

        public const char SplitChar = (char) 0;
        private static readonly char[] Split = { SplitChar };

        /// <summary>
        /// 把字符串转化为LogItem对象
        /// </summary>
        /// <param name="logItem"></param>
        /// <returns></returns>
        public static LogItem Parse(string logItem)
        {
            try
            {
                string[] strArray = logItem.Split(Split, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length!=4)
                {
                    return null;
                }
                LogItem item=new LogItem();
                item.Time = strArray[0].Substring(2).Trim();
                item.Type = (LogType)Enum.Parse(typeof(LogType), strArray[1].Trim(), true);
                item.Level = (LogLevel)Enum.Parse(typeof(LogLevel), strArray[2].Trim(':').Trim(), true);
                item.Text = strArray[3].Trim();

                return item;
            }
            catch (Exception exception)
            {
                Debug.Assert(false, "无效日志:" + logItem + "\t" + exception.Message);
                return null;
            }
        }

        /// <summary>
        /// 把对象转化为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("=> {0,-16}{1}{2,-7}{3}{4,-12}:{5}   {6}", this.Time, SplitChar, this.Type, SplitChar, this.Level, SplitChar, this.Text);
        }
    }
}
