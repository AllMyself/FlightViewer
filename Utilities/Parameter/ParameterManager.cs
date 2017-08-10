//********************************************************
//建立日期:2015.11.1
//作者:litao
//內容说明:　ParameterManager表示本应用程序中的参数管理。

//修改日期：
//作者:
//內容说明:
//********************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BinHong.Utilities
{
    public class ParameterManager
    {
        /// <summary>
        /// 参数文件路径
        /// </summary>
        private string _parameterFilePath;

        /// <summary>
        /// 是否已经初始化
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// 参数字典
        /// </summary>
        private readonly Dictionary<string,ParameterData> _paramDic=new Dictionary<string, ParameterData>();
        
        /// <summary>
        /// 注册参数
        /// </summary>
        public void RegisterParameter<T>(string paramPath, T defaultValue, string name,string description,
            Func<object, bool> paramVerify, OnsetTime onsetTime)
        {
            if (!_paramDic.ContainsKey(paramPath))
            {
                ParameterData paramData= new ParameterData();
                paramData.Path = paramPath;
                paramData.ValueType = defaultValue.GetType();
                paramData.Value = defaultValue;
                paramData.Name = name;
                paramData.Description = description;
                paramData.OnsetTime = onsetTime;

                _paramDic.Add(paramPath, paramData);
            }
            ParameterData data = _paramDic[paramPath];
            data.Verify = paramVerify;
        }

        /// <summary>
        /// 注册参数
        /// </summary>
        public void RegisterParameter<T>(string paramPath, T defaultValue, string name,
            Func<object, bool> paramVerify, OnsetTime onsetTime)
        {
            if (!_paramDic.ContainsKey(paramPath))
            {
                ParameterData paramData = new ParameterData();
                paramData.Path = paramPath;
                paramData.ValueType = defaultValue.GetType();
                paramData.Value = defaultValue;
                paramData.Name = name;
                paramData.OnsetTime = onsetTime;

                _paramDic.Add(paramPath, paramData);
            }
            ParameterData data = _paramDic[paramPath];
            data.Verify = paramVerify;
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="paramPath"></param>
        /// <returns></returns>
        public ParameterData GetParaData(string paramPath)
        {
            if (!_paramDic.ContainsKey(paramPath))
            {
                return null;
            }
            return _paramDic[paramPath];
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="paramPath"></param>
        /// <param name="value"></param>
        public void SetParamData(string paramPath,Object value)
        {
            if (!_paramDic.ContainsKey(paramPath))
            {
                return;
            }
            ParameterData paraData=_paramDic[paramPath];
            paraData.Value = value;
        }

        /// <summary>
        /// 初始化参数文件的路径。并且装载参数
        /// </summary>
        /// <param name="dir"></param>
        public void Initialize(string dir)
        {
            ParameterData.AfterModify += OnAfterModify;
            ParameterData.ConvertFailed += OnConvertFailed;

            //注册参数
            RegisterParameter();

            //装载参数
            LoadParameter(dir);

            IsInitialized = true;
        }

        /// <summary>
        /// 注册参数
        /// </summary>
        private void RegisterParameter()
        {
            this.RegisterParameter<bool>(ParameterNames.IsSimulateBoardState, false,
                "是否处于板卡模拟状态", null, OnsetTime.Now);
            this.RegisterParameter<bool>(ParameterNames.CanCollectBoardData, false,
                "是否收集板卡数据", null, OnsetTime.Now);
            this.RegisterParameter<string>(ParameterNames.SelectedLanguage, "UnKnown",
                "选择的语言", null, OnsetTime.Reboot);

            this.RegisterParameter<int>(ParameterNames.PrefetchBufferLenPath, 10,
                "UDP 接收缓存大小(KB)", newValue =>
                {
                    int value = (int)newValue;
                    int min = 10;
                    int max = 1000;
                    if (value < min
                        || value > max)
                    {
                        ShowVerifyFailedMessage(value, min.ToString(), max.ToString());
                        return false;
                    }
                    return true;
                }, OnsetTime.Reboot);


            this.RegisterParameter<int>(ParameterNames.LogFileMaxSize, 100,
                "日志文件的分割空间(MB)", newValue =>
                {
                    int value = (int)newValue;
                    int min = 10;
                    int max = 1000;
                    if (value < min
                        || value > max)
                    {
                        ShowVerifyFailedMessage(value, min.ToString(), max.ToString());
                        return false;
                    }
                    return true;
                }, OnsetTime.Now);

            this.RegisterParameter<int>(ParameterNames.LogSamMaxSize, 500,
                "日志文件的最大空间(MB)", newValue =>
                {
                    int value = (int)newValue;
                    int min = 500;
                    int max = 10000;
                    if (value < min
                        || value > max)
                    {
                        ShowVerifyFailedMessage(value, min.ToString(), max.ToString());
                        return false;
                    }
                    return true;
                }, OnsetTime.Now);

            this.RegisterParameter<bool>(ParameterNames.LoginClearLog, false,
                "登录后自动清空日志信息", null, OnsetTime.Now);


            this.RegisterParameter<int>(ParameterNames.AlarmFileMaxSize, 100,
                "告警文件的最大空间(MB)", newValue =>
                {
                    int value = (int)newValue;
                    int min = 100;
                    int max = 1000;
                    if (value < min
                        || value > max)
                    {
                        ShowVerifyFailedMessage(value, min.ToString(), max.ToString());
                        return false;
                    }
                    return true;
                }, OnsetTime.Now);

            this.RegisterParameter<bool>(ParameterNames.LoginClearAlarm, false,
                "登录后自动清除告警信息", null, OnsetTime.Now);


            this.RegisterParameter<int>(ParameterNames.ConsoleFileMaxSize, 10000,
                "控制台文件的最大空间(MB)", newValue =>
                {
                    int value = (int)newValue;
                    int min = 500;
                    int max = 10000;
                    if (value < min
                        || value > max)
                    {
                        ShowVerifyFailedMessage(value, min.ToString(), max.ToString());
                        return false;
                    }
                    return true;
                }, OnsetTime.Now);

            this.RegisterParameter<int>(ParameterNames.ConsoleBufferMaxSize, 10,
                "控制台buffer的空间(M)", newValue =>
                {
                    int value = (int)newValue;
                    int min = 1;
                    int max = 50;
                    if (value < min
                        || value > max)
                    {
                        ShowVerifyFailedMessage(value, min.ToString(), max.ToString());
                        return false;
                    }
                    return true;
                }, OnsetTime.Now);


            this.RegisterParameter<bool>(ParameterNames.IsPingEnabled, true,
                "是否PING每个CPU", null, OnsetTime.Now);

            this.RegisterParameter<int>(ParameterNames.PingTimeOut, 20,
                "PING后等待的超时值(ms)", newValue =>
                {
                    int value = (int)newValue;

                    int min = 10;
                    int max = 1000;
                    if (value < min
                        || value > max)
                    {
                        ShowVerifyFailedMessage(value, min.ToString(), max.ToString());
                        return false;
                    }
                    if (value >= (int)GetParaData(ParameterNames.PingInterval).Value)
                    {
                        OnVerifyFailedAction("PING的超时值要小于PING的时间间隔");
                        return false;
                    }
                    return true;
                }, OnsetTime.Now);

            this.RegisterParameter<int>(ParameterNames.PingInterval, 500,
                "PING的时间间隔(ms)", newValue =>
                {
                    int value = (int)newValue;
                    int min = 10;
                    int max = 10000;
                    if (value < min
                        || value > max)
                    {
                        ShowVerifyFailedMessage(value, min.ToString(), max.ToString());
                        return false;
                    }
                    if ((int)GetParaData(ParameterNames.PingTimeOut).Value >= value)
                    {
                        OnVerifyFailedAction("ping的时间间隔要大于ping的超时值");
                        return false;
                    }
                    return true;
                }, OnsetTime.Now);


            this.RegisterParameter<int>(ParameterNames.HeartInterval, 3000,
                "心跳的时间间隔(ms)", newValue =>
                {
                    int value = (int)newValue;
                    int min = 500;
                    int max = 5000;
                    if (value < min
                        || value > max)
                    {
                       ShowVerifyFailedMessage(value, min.ToString(), max.ToString());
                        return false;
                    }
                    return true;
                }, OnsetTime.Now);

            this.RegisterParameter<int>(ParameterNames.HeartCheckType, 0,
                "检测类型", newValue =>
                {
                    int value = (int)newValue;
                    int min = 0;
                    int max = 1;
                    if (value < min
                        || value > max)
                    {
                        ShowVerifyFailedMessage(value, min.ToString(), max.ToString());
                        return false;
                    }
                    return true;
                }, OnsetTime.Now);

            this.RegisterParameter<string>(ParameterNames.FtpBoardDirHistory, "",
                "", null, OnsetTime.Now);
            this.RegisterParameter<string>(ParameterNames.FtpPcDirHistory, "",
                "", null, OnsetTime.Now);
        }

        /// <summary>
        /// 装载参数
        /// </summary>
        private void LoadParameter(string dir)
        {
            //读取配置
            dir += "\\Parameter\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            _parameterFilePath = dir + "Parameter.cfg";


            if (!File.Exists(_parameterFilePath))
            {
                return;
            }
            //装载参数。
            string[] rows = File.ReadAllLines(_parameterFilePath);
            foreach (var row in rows)
            {
                ParameterData praData = ParameterData.Parse(row);
                if (praData == null)
                {
                    continue;
                }
                if (_paramDic.ContainsKey(praData.Path))
                {
                    ParameterData regData=_paramDic[praData.Path] ;
                    regData.Name = praData.Name;
                    regData.Description = praData.Description;
                    regData.Value = praData.Value;
                }
            }
        }

        /// <summary>
        /// 保存参数
        /// </summary>
        public void Save()
        {
            if (!IsInitialized)
            {
                return;
            }
            List<string> list=_paramDic.Values.Select(o=>o.ToString()).ToList();
            File.WriteAllLines(_parameterFilePath, list);
        }

        /// <summary>
        /// 验证失败行为
        /// </summary>
        public event Action<string> VerifyFailedAction;

        /// <summary>
        /// 验证失败行为的响应
        /// </summary>
        /// <param name="message"></param>
        public void OnVerifyFailedAction(string message)
        {
            if (VerifyFailedAction != null)
            {
                VerifyFailedAction(message);
            }
        }

        /// <summary>
        /// 显示验证失败信息
        /// </summary>
        public void ShowVerifyFailedMessage(object value, string min, string max)
        {
            string message = string.Format("值{0}不在{1}~{2}范围", value, min, max);
            OnVerifyFailedAction(message);
        }

        /// <summary>
        /// 修改参数后的行为
        /// </summary>
        public Action<ParameterData> AfterModify;

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="paramData"></param>
        public void OnAfterModify(ParameterData paramData)
        {
            if (AfterModify!=null)
            {
                AfterModify(paramData);
            }
            //参数修改之后马上保存
            Save();
        }

        /// <summary>
        /// 参数转换失败行为
        /// </summary>
        public Action<ParameterData,string> ConvertFailed;

        /// <summary>
        /// 参数转换失败响应
        /// </summary>
        /// <param name="paramData"></param>
        public void OnConvertFailed(ParameterData paramData,string value)
        {
            if (ConvertFailed != null)
            {
                ConvertFailed(paramData, value);
            }
        }
        
    }
}
