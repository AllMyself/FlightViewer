//********************************************************
//建立日期:2015.11.28
//作者:litao
//內容说明:　ParameterNames定义设置的参数的名称，它是一个静态类，以后这个文件的内容可以通过读取配置文件来获取。

//修改日期：
//作者:
//內容说明:
//********************************************************

namespace BinHong.Utilities
{
    public class ParameterNames
    {
        public const string IsSimulateBoardState = "Board_IsSimulateState";
        public const string CanCollectBoardData = "Board_CanCollectData";

        public const string PrefetchBufferLenPath = "UdpConnect_PrefetchBufferLen";
        public const string LogFileMaxSize = "Log_LogFileMaxSize";
        public const string LogSamMaxSize = "Log_LogSamMaxSize";
        public const string LoginClearLog = "Log_LoginClearLog";
        public const string AlarmFileMaxSize = "Alarm_AlarmFileMaxSize";
        public const string LoginClearAlarm = "Alarm_LoginClearAlarm";
        public const string ConsoleFileMaxSize = "Console_ConsoleFileMaxSize";
        public const string ConsoleBufferMaxSize = "Console_ConsoleBufferMaxSize";
        public const string IsPingEnabled = "Ping_IsPingEnabled";
        public const string PingTimeOut = "Ping_PingTimeOut";
        public const string PingInterval = "Ping_PingInterval";
        public const string HeartInterval = "Heart_HeartInterval";
        public const string HeartCheckType = "Heart_HeartCheckType";
        public const string SelectedLanguage = "Chinese";
        public const string FtpPcDirHistory = "Ftp_PcDirHistory";
        public const string FtpBoardDirHistory = "Ftp_BoardDirHistory";
    }
}
