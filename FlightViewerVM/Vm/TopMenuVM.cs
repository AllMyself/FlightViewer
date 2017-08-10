using BinHong.FlightViewerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinHong.FlightViewerVM.Vm
{
    //用于保存工程与加载工程
    public class TopMenuVM
    {
        private Device429 _device429;
        public DeviceForXml _deviceInfo;
        public TopMenuVM(Device429 device429)
        {
            this._device429 = device429;
            _deviceInfo = new DeviceForXml();
        }
        public void SaveSetting()
        {
            _deviceInfo.sendSet = new List<SendSetting>();
            _deviceInfo.LoopSet = new List<LoopTxSetting>();
            _deviceInfo.GatherSet = new List<GatherSetting>();
            _deviceInfo.devMsg = new DeviceMessage();
            //_deviceInfo.FilterSet = new FilterSetting();
            //FilterSetting filterStr = new FilterSetting();
            //filterStr.filter = _device429.filterStr;
            DeviceMessage devMessage = new DeviceMessage();
            devMessage.BoardNo = _device429.BoardNo;
            devMessage.BoardType = _device429.BoardType;
            devMessage.ChannelCount = _device429.ChannelCount;
            devMessage.ChannelType = _device429.ChannelType;
            devMessage.DevID = _device429.DevID;
            devMessage.filter = _device429.filterStr;
            _deviceInfo.devMsg = devMessage;
            foreach (var item in _device429.SendComponents)
            {
                Channe429Send sendChanel = (Channe429Send)item;

                //赋值发送配置信息
                SendSetting sendSet = new SendSetting();
                sendSet.AliasName = sendChanel.AliasName;
                sendSet.BaudRate = sendChanel.BaudRate;
                sendSet.ChannelID = sendChanel.ChannelID;
                sendSet.ChannelType = sendChanel.ChannelType;
                sendSet.Enabled = sendChanel.Enabled;
                sendSet.labelInfos = new List<LabelInfo>();
                for (int i = 0; i <= 377; i++)
                {
                    LabelInfo label = new LabelInfo();
                    SendLabel429 label429 = (SendLabel429)sendChanel.GetSpecificItem(i);
                    if (label429 != null)
                    {
                        label.Data = label429.Data;
                        label.Label = label429.Label;
                        label.Parity = label429.Parity;
                        label.SDI = label429.SDI;
                        label.SymbolState = label429.SymbolState;
                        sendSet.labelInfos.Add(label);
                    }
                }
                _deviceInfo.sendSet.Add(sendSet);//记录发送设置信息
            }
            //赋值接收配置信息
            _deviceInfo.RevSet = new List<ReceiveSetting>();
            //赋值接收配置信息
            foreach (var item in _device429.ReceiveComponents)
            {
                ReceiveSetting RevSet = new ReceiveSetting();
                Channe429Receive RevChanel = (Channe429Receive)item;
                RevSet.AliasName = RevChanel.AliasName;
                RevSet.BaudRate = RevChanel.BaudRate;
                RevSet.ChannelID = RevChanel.ChannelID;
                RevSet.ChannelType = RevChanel.ChannelType;
                RevSet.Enabled = RevChanel.Enabled;
                _deviceInfo.RevSet.Add(RevSet);

                GatherSetting GatherSet = new GatherSetting();
                ChannelGatherParamA429Rx gatherParamA429 = new ChannelGatherParamA429Rx();
                uint ret = ((Channel429DriverRx)(RevChanel.ChannelDriver)).ChannelGatherParam(ref gatherParamA429,
                        ParamOptionA429.BHT_L1_PARAM_OPT_GET);
                GatherSet.chanelID = RevChanel.ChannelID;
                GatherSet.gatherType = gatherParamA429.recv_mode;
                GatherSet.isFilter = gatherParamA429.gather_enable;
                GatherSet.ThresholdCount = gatherParamA429.threshold_count;
                GatherSet.ThresholdTime = gatherParamA429.threshold_time;
                _deviceInfo.GatherSet.Add(GatherSet);

                LoopTxSetting loopTxSetting = new LoopTxSetting();
                loopTxSetting.chanelID = RevChanel.ChannelID;
                loopTxSetting.IsLoop = RevChanel.isLoop;
                _deviceInfo.LoopSet.Add(loopTxSetting);
            }
        }
        public void OpenSetting()
        {

        }
    }
    public class DeviceForXml
    {
        public DeviceMessage devMsg;
        public List<SendSetting> sendSet;
        public List<ReceiveSetting> RevSet;
        public List<LoopTxSetting> LoopSet;
        public List<GatherSetting> GatherSet;
    }
    public class DeviceMessage
    {
        public uint BoardNo { get; set; }
        public BoardType BoardType { get; set; }
        public ChannelType ChannelType { get; set; }
        public uint DevID { get; set; }
        public uint ChannelCount { get; set; }
        public string filter { get; set; }
    }
    public class SendSetting
    {

        public uint ChannelID { get; set; }

        public string AliasName { get; set; }

        public ChannelType ChannelType { get; set; }

        public bool Enabled { get; set; }

        public bool Parity { get; set; }

        public int BaudRate { get; set; }

        public List<LabelInfo> labelInfos { get; set; }
    }
    public class ReceiveSetting
    {
        public uint ChannelID { get; set; }

        public string AliasName { get; set; }

        public ChannelType ChannelType { get; set; }

        public bool Enabled { get; set; }

        public bool Parity { get; set; }

        public int BaudRate { get; set; }
    }
    public class LoopTxSetting
    {
        public uint chanelID { get; set; }
        public bool IsLoop { get; set; }
    }
    public class GatherSetting
    {
        //这里可以从设备读取出来
        public uint chanelID { get; set; }
        public uint isFilter { get; set; }
        public RecvModeA429 gatherType { get; set; }
        public ushort ThresholdCount { get; set; }
        public ushort ThresholdTime { get; set; }
    }
    public class FilterSetting
    {
        public string filter { get; set; }
    }
    public class LabelInfo
    {
        /// <summary>
        /// 标号（1~8）
        /// </summary>
        public int Label { get; set; }

        /// <summary>
        /// SDI(9~10)
        /// </summary>
        public int SDI { get; set; }

        /// <summary>
        /// Data(11~29)
        /// </summary>
        public int Data { get; set; }

        /// <summary>
        /// 符号位
        /// </summary>
        public int SymbolState { get; set; }

        /// <summary>
        /// 奇偶校验位
        /// </summary>
        public int Parity { get; set; }
    }
}
