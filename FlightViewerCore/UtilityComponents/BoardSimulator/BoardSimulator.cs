//********************************************************
//建立日期:2015.11.13
//作者:litao
//內容说明:　定义了板卡模拟器。希望做到，不要板卡，我们也可以调试程序。

//修改日期：
//作者:
//內容说明:
//********************************************************

using System.Collections.Generic;
using System.IO;
using BinHong.Utilities;

namespace BinHong.FlightViewerCore
{
    /// <summary>
    /// 板卡模拟器
    /// </summary>
    public class BoardSimulator
    {
        /// <summary>
        /// 仿真数据的存放Path
        /// </summary>
        private string _dataPath;

        /// <summary>
        /// 模拟数据的列表
        /// </summary>
        private readonly Dictionary<string, List<SimulateData>> _dataDictionary = new Dictionary<string, List<SimulateData>>();
        
        /// <summary>
        /// 接收列表
        /// </summary>
        private readonly List<SimulateData> _receiveList=new List<SimulateData>();

        /// <summary>
        /// 是否处于仿真状态
        /// </summary>
        public bool IsSimulateState { get; private set; }

        /// <summary>
        /// 是否可以采集数据
        /// </summary>
        public bool CanCollectData { get; private set; }

        /// <summary>
        /// 弹出模拟请求
        /// 就是根据要发送的命名Key,从字典中取出要发送的数据，放到发送列表中。
        /// </summary>
        public void PopSimulatedRequest(string sendKey)
        {
            //仿真状态
            if (IsSimulateState)
            {
                List<SimulateData> dataList;
                _dataDictionary.TryGetValue(sendKey, out dataList);
                if (dataList == null)
                {
                    return;
                }
                _receiveList.AddRange(dataList);
            }
        }

        /// <summary>
        /// 收集模拟请求
        /// </summary>
        public void CollectSimulateRequest(string sendKey)
        {
            if (IsSimulateState
                && CanCollectData)
            {
                if (!_dataDictionary.ContainsKey(sendKey))
                {
                    _dataDictionary.Add(sendKey, new List<SimulateData>());
                }
            }
        }

        /// <summary>
        /// 弹出模拟数据
        /// </summary>
        public SimulateData PopSimulatedData()
        {
            if (IsSimulateState)
            {
                if (_receiveList.Count!=0)
                {
                    SimulateData curData = _receiveList[0];
                    _receiveList.RemoveAt(0);
                    return curData;
                }
            }
            return null;
        }

        /// <summary>
        /// 收集模拟数据
        /// </summary>
        public void CollectSimulateData(string receiveKey, byte[] bytes)
        {
            if (IsSimulateState
                && CanCollectData)
            {
                _dataDictionary[receiveKey].Add(new SimulateData(bytes));
            }
        }

        /// <summary>
        /// 初始化仿真器。
        /// 主要工作为：
        /// 1、获取仿真器要产生的（也是要获取的）仿真数据的位置。
        /// 2、把仿真数据放到内存里面的DataDictionary
        /// </summary>
        public void Initialize(string dir)
        {
            IsSimulateState = (bool)App.Instance.Parameter.GetParaData(ParameterNames.IsSimulateBoardState).Value;
            CanCollectData = (bool)App.Instance.Parameter.GetParaData(ParameterNames.CanCollectBoardData).Value;

            if (!IsSimulateState)
            {
                return;
            }

            //1、获取仿真器要产生的（也是要获取的）仿真数据的位置。
            _dataPath = dir+"\\Config\\SimulateData.bin";

            //2、把仿真数据放到内存里面的DataDictionary
            if (File.Exists(_dataPath)
                && IsSimulateState)
            {
                string[] rows=File.ReadAllLines(_dataPath);
                foreach (var row in rows)
                {
                    string[] keyValue=row.Split('~');
                    if (keyValue.Length==2)
                    {
                        string key = keyValue[0];
                        string[] strArray = key.Split('+');
                        string ip = strArray[0];

                        byte[] bytes = UtilityConvertor.ReversibleStringByteToArray(keyValue[1]);
                        if (!_dataDictionary.ContainsKey(key))
                        {
                            _dataDictionary.Add(key, new List<SimulateData>());
                        }
                        _dataDictionary[key].Add(new SimulateData(bytes,ip));
                    }
                }
            }
        }
        
        /// <summary>
        /// 把保存的数据保存到指定位置。
        /// </summary>
        public void Save()
        {
            if (IsSimulateState
                && CanCollectData)
            {
                List<string> outPutList = new List<string>();

                foreach (var keyValue in _dataDictionary)
                {
                    string key = keyValue.Key;
                    foreach (SimulateData data in keyValue.Value)
                    {
                        outPutList.Add(key + "~" + UtilityConvertor.ReversibleByteArrayToString(data.Bytes));
                    }
                }

                File.WriteAllLines(_dataPath, outPutList);
            }
        }
    }

    /// <summary>
    /// 模拟数据
    /// </summary>
    public class SimulateData
    {
        /// <summary>
        /// 设备的Ip
        /// </summary>
        public string DeviceIp { get; private set; }

        /// <summary>
        /// 模拟数据。
        /// </summary>
        public byte[] Bytes;
        
        public SimulateData(byte[] bytes,string ip)
        {
            Bytes = bytes;
            DeviceIp = ip;
        }

        public SimulateData(byte[] bytes)
        {
            Bytes = bytes;
        }
    }
}
