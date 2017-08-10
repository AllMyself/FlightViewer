namespace BinHong.Utilities
{
    /// <summary>
    /// 运行日志
    /// </summary>
    public class RunningLog
    {
        /// <summary>
        /// 日志文件
        /// </summary>
        public static LogFile LogFile
        {
            get
            {
                if (_logFile == null)
                {
                    //默认日志放到一个的文件夹里面。要切换其他文件夹请使用Start设置
                    Start(@"history\");
                }
                return _logFile;
            }
            private set { _logFile = value; }
        }
        
        private static LogFile _logFile;

        /// <summary>
        /// 日志功能启动
        /// </summary>
        public static void Start(string dir)
        {
            LogFile = new LogFile(dir);
            SaveFileThread.AddFileToSaveThread(LogFile);
            SaveFileThread.Start();
        }

        /// <summary>
        /// 记录一条日志
        /// </summary>
        private static void CreateItem(LogType type, LogLevel level, string text)
        {
            LogFile.CreateItem(type, level, text);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="text"></param>
        public static void Record(string text)
        {
            Record(LogType.System, LogLevel.Information, text);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        public static void Record(LogLevel level, string text)
        {
            Record(LogType.System, level, text);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        public static void Record(LogType type, string text)
        {
            Record(type, LogLevel.Information, text);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        public static void Record(LogType type, LogLevel level, string text)
        {
            CreateItem(type, level, text);
        }
    }
}
