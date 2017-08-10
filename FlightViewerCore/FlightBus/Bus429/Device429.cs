using BinHong.Utilities;
using System;
using System.Runtime.InteropServices;

namespace BinHong.FlightViewerCore
{
    public class Device429 : AbstractDevice, IGetItem<AbstractChannel429>, IFilter, ISummary
    {
        public readonly ReceiveModule ReceiveModule;
        public readonly A429SendModule SendModule;
        Device429Operator device429Operator = new Device429Operator();
        public Device429()
        {
            SendModule = new A429SendModule(this);
            ReceiveModule = new ReceiveModule(this);
        }

        public override void BuildModule()
        {
            object baudRateValue = App.Instance.ConfigManager.GetParameter(this.Path + "_BaudRate");
            object enableValue = App.Instance.ConfigManager.GetParameter(this.Path + "_Enable");
            object parityValue = App.Instance.ConfigManager.GetParameter(this.Path + "_Parity");

            for (uint i = 0; i < 16; i++)
            {
                Channe429Receive channel = new Channe429Receive(i);
                channel.Enabled = false;
                if (baudRateValue != null)
                {
                    channel.BaudRate = (int)baudRateValue;
                }
                if (enableValue != null)
                {
                    channel.Enabled = (bool)enableValue;
                }
                if (parityValue != null)
                {
                    channel.Parity = (int)parityValue;
                }
                channel.Initialize();
                Add(channel);
                ReceiveComponents.Add(channel);

                channel.BuildModule();
                //初始化Channel
                ChannelParamA429 paramA429 = new ChannelParamA429()
                {
                    work_mode = ChannelWorkModeA429.A429ChannelWorkModeNABLE,
                    baud = BaudA429.BHT_L1_A429_BAUD_100K,
                    par = ParityA429.BHT_L1_A429_PARITY_ODD
                };
                uint ret = ((Channel429DriverRx)(channel.ChannelDriver)).ChannelParamRx(ref paramA429,
                    ParamOptionA429.BHT_L1_PARAM_OPT_SET);
                if (ret != 0)
                {
                    RunningLog.Record(string.Format("return value is {0} when invoke ChannelParamRx", ret));
                }

                ChannelGatherParamA429Rx gatherParamA429 = new ChannelGatherParamA429Rx()
                {
                    gather_enable = 1,
                    recv_mode = RecvModeA429.BHT_L1_A429_RECV_MODE_SAMPLE,
                    threshold_count = 0,
                    threshold_time = 0
                };
                ret = ((Channel429DriverRx)(channel.ChannelDriver)).ChannelGatherParam(ref gatherParamA429,
                    ParamOptionA429.BHT_L1_PARAM_OPT_SET);
                if (ret != 0)
                {
                    RunningLog.Record(string.Format("return value is {0} when invoke ChannelGatherParam", ret));
                }
            }

            for (uint i = 0; i < 16; i++)
            {
                Channe429Send channel = new Channe429Send(i);
                channel.Enabled = false;
                if (baudRateValue != null)
                {
                    channel.BaudRate = (int)baudRateValue;
                }
                if (enableValue != null)
                {
                    channel.Enabled = (bool)enableValue;
                }
                if (parityValue != null)
                {
                    channel.Parity = (int)parityValue;
                }

                channel.Initialize();
                Add(channel);
                SendComponents.Add(channel);

                channel.BuildModule();

                //初始化Channel
                ChannelParamA429 paramA429 = new ChannelParamA429()
                {
                    work_mode = ChannelWorkModeA429.A429ChannelWorkModeNABLE,
                    baud = BaudA429.BHT_L1_A429_BAUD_100K,
                    par = ParityA429.BHT_L1_A429_PARITY_ODD
                };
                uint ret = ((Channel429DriverTx)(channel.ChannelDriver)).ChannelParamTx(ref paramA429, ParamOptionA429.BHT_L1_PARAM_OPT_SET);
                if (ret != 0)
                {
                    RunningLog.Record(string.Format("return value is {0} when invoke ChannelParamRx", ret));
                }
            }
        }
        //读取设备信息
        public void ReadDev(ushort addr,ref byte byteOut)
        {
            int size = Marshal.SizeOf(typeof(byte));
            byte[] bytes = new byte[size];
            IntPtr data = Marshal.AllocHGlobal(size);
            uint ret = device429Operator.FpgaEepromRead(DevID, addr, ref data);
            if (ret != 0)
            {
                RunningLog.Record(string.Format("return value is {0} when invoke ReadDev", ret));
            }
            else
            {
                byteOut = (byte)Marshal.PtrToStructure(data, typeof(int));
            }
        }
        //向设备写入信息
        public void WriteDev(ushort addr, byte byteOut)
        {
            uint ret = device429Operator.FpgaEepromWrite(DevID,addr,byteOut);
            if (ret != 0)
            {
                RunningLog.Record(string.Format("return value is {0} when invoke WriteDev", ret));
            }
            else
            {
                RunningLog.Record(string.Format("WriteDev Success!"));
            }
        }
        protected override IChildContainer<AbstractComponent> ChildComponents
        {
            get { return _componentDic; }
        }
        private readonly ComponentDic<AbstractChannel429> _componentDic = new ComponentDic<AbstractChannel429>();

        public IChildContainer<AbstractComponent> ReceiveComponents
        {
            get { return _recComponentDic; }
        }
        private readonly ComponentDic<AbstractChannel429> _recComponentDic = new ComponentDic<AbstractChannel429>();

        public IChildContainer<AbstractComponent> SendComponents
        {
            get { return _sendComponentDic; }
        }
        private readonly ComponentDic<AbstractChannel429> _sendComponentDic = new ComponentDic<AbstractChannel429>();

        public override IGetItemByIndex GetItem(int index)
        {
            return GetSpecificItem(index);
        }

        public override IGetItemByName GetItem(string name)
        {
            return GetSpecificItem(name);
        }

        public AbstractChannel429 GetSpecificItem(string name)
        {
            return _componentDic.GetSpecificItem(name);
        }

        public AbstractChannel429 GetSpecificItem(int index)
        {
            return _componentDic.GetSpecificItem(index);
        }
    }
}
