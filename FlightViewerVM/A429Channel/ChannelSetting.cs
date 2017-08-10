using System.ComponentModel;
using BinHong.FlightViewerCore;
using BinHong.FlightViewerVM.Properties;
using BinHong.Utilities;
using System;

namespace BinHong.FlightViewerVM
{
    public enum BaudRate
    {
        偶校验 = 0,
        奇校验 = 1,
        不校验 = 2
    }
    public class ChannelInfoUiSend : IChannelInfoMsg, INotifyPropertyChanged
    {
        private uint _id;
        private bool _enabled;
        private string _parity;
        private int _baudRate;
        private string _name;
        private bool _loopEnable;//设置是否环回



        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public uint ChannelID
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged("ChannelID");
            }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (value.Equals(_enabled)) return;
                _enabled = value;
                OnPropertyChanged("Enabled");
            }
        }

        public string Parity
        {
            get { return _parity; }
            set
            {
                if (value.Equals(_parity)) return;
                _parity = value;
                OnPropertyChanged("Parity");
            }
        }

        public int BaudRate
        {
            get { return _baudRate; }
            set
            {
                if (value == _baudRate) return;
                _baudRate = value;
                OnPropertyChanged("BaudRate");
            }
        }
        public bool LoopEnable
        {
            get { return _loopEnable; }
            set
            {
                if (value == _loopEnable) return;
                _loopEnable = value;
                OnPropertyChanged("LoopEnble");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public ChannelType ChannelType { get; set; }

    }
    public class ChannelInfoUiReceive : IChannelInfoMsg, INotifyPropertyChanged
    {
        private uint _id;
        private bool _enabled;
        private string _parity;
        private int _baudRate;
        private string _name;
        private bool isFilter;


        private string receiveType;

        private string deepCount;

        private string timeCount;




        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public uint ChannelID
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged("ChannelID");
            }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (value.Equals(_enabled)) return;
                _enabled = value;
                OnPropertyChanged("Enabled");
            }
        }

        public string Parity
        {
            get { return _parity; }
            set
            {
                if (value.Equals(_parity)) return;
                _parity = value;
                OnPropertyChanged("Parity");
            }
        }

        public int BaudRate
        {
            get { return _baudRate; }
            set
            {
                if (value == _baudRate) return;
                _baudRate = value;
                OnPropertyChanged("BaudRate");
            }
        }
        public bool IsFilter
        {
            get { return isFilter; }
            set
            {
                if (value == isFilter) return;
                isFilter = value;
                OnPropertyChanged("IsFilter");
            }
        }
        public string ReceiveType
        {
            get { return receiveType; }
            set
            {
                if (value == receiveType) return;
                receiveType = value;
                OnPropertyChanged("ReceiveType");
            }
        }
        public string DeepCount
        {
            get { return deepCount; }
            set
            {
                if (value == deepCount) return;
                deepCount = value;
                OnPropertyChanged("DeepCount");
            }
        }
        public string TimeCount
        {
            get { return timeCount; }
            set
            {
                if (value == timeCount) return;
                timeCount = value;
                OnPropertyChanged("TimeCount");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public ChannelType ChannelType { get; set; }

    }

    /// <summary>
    /// 通道设置接收
    /// </summary>
    public class ChannelReceiveSettingVm
    {
        public BindingList<ChannelInfoUiReceive> Channels
        {
            get { return _channels; }
        }

        private readonly BindingList<ChannelInfoUiReceive> _channels = new BindingList<ChannelInfoUiReceive>();

        public void UpdataDevice(Device429 device429)
        {
            for (int i = 0; i < 16; i++)
            {
                Channe429Receive channel = (Channe429Receive)device429.GetSpecificItem(i);
                ChannelInfoUiReceive channelInfoUi = _channels[i];
                channel.AliasName = channelInfoUi.Name;
                if (channelInfoUi.Parity == BaudRate.偶校验.ToString())
                {
                    channel.Parity = 1;
                }
                else if (channelInfoUi.Parity == BaudRate.奇校验.ToString())
                {
                    channel.Parity = 0;
                }
                else
                {
                    channel.Parity = 2;
                }
                channel.Enabled = channelInfoUi.Enabled;
                channel.BaudRate = channelInfoUi.BaudRate;
                channel.isFilter = channelInfoUi.IsFilter;
                channel.receiveType = channelInfoUi.ReceiveType;
                channel.deepCount = channelInfoUi.DeepCount;
                channel.timeCount = channelInfoUi.TimeCount;

                ChannelParamA429 paramA429 = new ChannelParamA429()
                {
                    work_mode = ChannelWorkModeA429.A429ChannelWorkModeNABLE
                };
                if (channel.Parity == 0)
                {
                    paramA429.par = ParityA429.BHT_L1_A429_PARITY_ODD;
                }
                else if (channel.Parity == 1)
                {
                    paramA429.par = ParityA429.BHT_L1_A429_PARITY_EVEN;
                }
                else
                {
                    paramA429.par = ParityA429.BHT_L1_A429_PARITY_NONE;
                }
                if (channelInfoUi.BaudRate == 12500)
                {
                    paramA429.baud = BaudA429.BHT_L1_A429_BAUD_12_5K;
                }
                else if (channelInfoUi.BaudRate == 50000)
                {
                    paramA429.baud = BaudA429.BHT_L1_A429_BAUD_50K;
                }
                else if (channelInfoUi.BaudRate == 100000)
                {
                    paramA429.baud = BaudA429.BHT_L1_A429_BAUD_100K;
                }
                uint ret = ((Channel429DriverRx)(channel.ChannelDriver)).ChannelParamRx(ref paramA429,
               ParamOptionA429.BHT_L1_PARAM_OPT_SET);
                if (ret != 0)
                {
                    RunningLog.Record(string.Format("return value is {0} when invoke ChannelParamRx", ret));
                }
                SetGatherParam(channel);
            }
        }
        private void SetGatherParam(Channe429Receive channelInfoUi)
        {
            ChannelGatherParamA429Rx gatherParamA429 = new ChannelGatherParamA429Rx();
            if (channelInfoUi.isFilter == true)//过滤
            {
                gatherParamA429.gather_enable = 0;
            }
            else//不过滤
            {
                gatherParamA429.gather_enable = 1;
            }
            if (channelInfoUi.receiveType == "队列")
            {
                gatherParamA429.recv_mode = RecvModeA429.BHT_L1_A429_RECV_MODE_LIST;
            }
            else
            {
                gatherParamA429.recv_mode = RecvModeA429.BHT_L1_A429_RECV_MODE_SAMPLE;
            }
            if (!string.IsNullOrEmpty(channelInfoUi.deepCount))
            {
                gatherParamA429.threshold_count = (ushort)Convert.ToInt32(channelInfoUi.deepCount);
            }
            if (!string.IsNullOrEmpty(channelInfoUi.timeCount))
            {
                gatherParamA429.threshold_time = (ushort)Convert.ToInt32(channelInfoUi.timeCount);
            }
            uint ret = ((Channel429DriverRx)(channelInfoUi.ChannelDriver)).ChannelGatherParam(ref gatherParamA429,
                ParamOptionA429.BHT_L1_PARAM_OPT_SET);
            if (ret != 0)
            {
                RunningLog.Record(string.Format("return value is {0} when invoke ChannelGatherParam", ret));
            }
        }
        public void Initialize(Device429 device429)//初始化通道
        {
            for (int i = 0; i < 16; i++)
            {
                Channe429Receive ch = (Channe429Receive)device429.GetSpecificItem(i);//遍历dic，获取信息
                var channel = new ChannelInfoUiReceive();
                channel.Name = "ReceiveChannel" + (ch.ChannelID + 1).ToString();
                channel.ChannelID = ch.ChannelID;
                if (ch.Parity == 1)
                {
                    channel.Parity = BaudRate.偶校验.ToString();
                }
                else if (ch.Parity == 0)
                {
                    channel.Parity = BaudRate.奇校验.ToString();
                }
                else
                {
                    channel.Parity = BaudRate.不校验.ToString();
                }
                channel.Enabled = ch.Enabled;
                channel.BaudRate = ch.BaudRate;
                channel.IsFilter = ch.isFilter;
                channel.ReceiveType = ch.receiveType;
                channel.DeepCount = ch.deepCount;
                channel.TimeCount = ch.timeCount;
                _channels.Add(channel);
                SetGatherParam(ch);
            }
        }
    }
    /// <summary>
    /// 发送通道设置
    /// </summary>
    public class ChannelSendSettingVm
    {
        public BindingList<ChannelInfoUiSend> Channels
        {
            get { return _channels; }
        }

        private readonly BindingList<ChannelInfoUiSend> _channels = new BindingList<ChannelInfoUiSend>();

        public void UpdataDevice(Device429 device429)
        {
            for (int i = 0; i < 16; i++)
            {
                Channe429Send channel = (Channe429Send)device429.GetSpecificItem(i + 16);//注意这里
                ChannelInfoUiSend channelInfoUi = _channels[i];
                channel.AliasName = channelInfoUi.Name;
                if (channelInfoUi.Parity == BaudRate.偶校验.ToString())
                {
                    channel.Parity = 1;
                }
                else if (channelInfoUi.Parity == BaudRate.奇校验.ToString())
                {
                    channel.Parity = 0;
                }
                else
                {
                    channel.Parity = 2;
                }
                channel.BaudRate = channelInfoUi.BaudRate;
                channel.LoopEnable = channelInfoUi.LoopEnable;
                channel.Enabled = channelInfoUi.Enabled;
                ChannelParamA429 paramA429 = new ChannelParamA429()
                {
                    work_mode = ChannelWorkModeA429.A429ChannelWorkModeNABLE
                };
                if (channel.Parity == 0)
                {
                    paramA429.par = ParityA429.BHT_L1_A429_PARITY_ODD;
                }
                else if (channel.Parity == 1)
                {
                    paramA429.par = ParityA429.BHT_L1_A429_PARITY_EVEN;
                }
                else
                {
                    paramA429.par = ParityA429.BHT_L1_A429_PARITY_NONE;
                }
                if (channelInfoUi.BaudRate == 12500)
                {
                    paramA429.baud = BaudA429.BHT_L1_A429_BAUD_12_5K;
                }
                else if (channelInfoUi.BaudRate == 50000)
                {
                    paramA429.baud = BaudA429.BHT_L1_A429_BAUD_50K;
                }
                else if (channelInfoUi.BaudRate == 100000)
                {
                    paramA429.baud = BaudA429.BHT_L1_A429_BAUD_100K;
                }
                uint ret = ((Channel429DriverTx)(channel.ChannelDriver)).ChannelParamTx(ref paramA429, ParamOptionA429.BHT_L1_PARAM_OPT_SET);
                if (ret != 0)
                {
                    RunningLog.Record(string.Format("return value is {0} when invoke ChannelParamRx", ret));
                }
                if (channel.LoopEnable)
                {
                    Channel429DriverTx driverTx = new Channel429DriverTx(device429.DevID, channel.ChannelID);
                    ret = driverTx.ChannelLoopTx(AbleStatusA429.BHT_L1_OPT_ENABLE);
                }
                else
                {
                    Channel429DriverTx driverTx = new Channel429DriverTx(device429.DevID, channel.ChannelID);
                    ret = driverTx.ChannelLoopTx(AbleStatusA429.BHT_L1_OPT_DISABLE);
                }
            }

        }

        public void Initialize(Device429 device429)
        {
            for (int i = 0; i < 16; i++)
            {
                Channe429Send ch = (Channe429Send)device429.GetSpecificItem(i + 16);
                var channel = new ChannelInfoUiSend();
                channel.Name = "SendChannel" + (ch.ChannelID + 1).ToString();
                channel.ChannelID = ch.ChannelID;
                if (ch.Parity == 1)
                {
                    channel.Parity = BaudRate.偶校验.ToString();
                }
                else if (ch.Parity == 0)
                {
                    channel.Parity = BaudRate.奇校验.ToString();
                }
                else
                {
                    channel.Parity = BaudRate.不校验.ToString();
                }
                channel.Enabled = ch.Enabled;
                channel.BaudRate = ch.BaudRate;
                channel.LoopEnable = ch.LoopEnable;
                _channels.Add(channel);
            }
        }
    }
}
