using System;
using BinHong.Utilities;
using System.ComponentModel;
using System.Collections.Generic;

namespace BinHong.FlightViewerCore
{

    public abstract class AbstractChannel429 : AbstractChannel, IChannelInfo, IAlias, IGetItem<AbstractLabel>, IAddDelete<AbstractLabel>
    {
        public string AliasName { get; set; }

        public ChannelType ChannelType { get; set; }

        public bool Enabled { get; set; }

        public int Parity { get; set; }

        public int BaudRate { get; set; }

        public abstract AbstractLabel GetSpecificItem(int index);
        public abstract AbstractLabel GetSpecificItem(string name);
        public abstract void Add(AbstractLabel t);
        public abstract void Delete(AbstractLabel t);
        public abstract void Remove(int index);

        public bool isSend = false;//是否矢能
    }

    /// <summary>
    /// 429接收通道
    /// </summary>
    public class Channe429Receive : AbstractChannel429, IFilter, ISummary, IReceive
    {
        public FilterModule FilterModule { get; private set; }
        public SummaryModule SummaryModule { get; private set; }
        public DataProcessModule DataProcessModule { get; private set; }
        public RxpA429 rxpA429Result;
        public int count = 0;
        public int errCount = 0;
        public bool isFilter { get; set; }
        public string receiveType = "";
        public string deepCount = "";
        public string timeCount = "";
        public uint DeviceCount = 0;
        public uint errDeviceCount = 0;
        public bool isLoop = false;

        public Channe429Receive(uint id)
        {
            if (id > 15)
            {
                throw new Exception("接收通道的ID只能在[0,15]的范围内");
            }
            ChannelID = id;
            NamePrefix = "ReceiveChannel";
            ChannelType = ChannelType.Receive;
        }

        public override void BuildModule()
        {
            uint devID = ((IDeviceInfo)Owner).DevID;
            ChannelDriver = new Channel429DriverRx(devID, ChannelID);

            object baudRateValue = App.Instance.ConfigManager.GetParameter(this.Path + "_BaudRate");
            if (baudRateValue != null)
            {
                BaudRate = (int)baudRateValue;
            }
            rxpA429Result = new RxpA429()
            {
                timestamp = 0,
                data = 0
            };
            DataProcessModule = new DataProcessModule(this);
            SummaryModule = new SummaryModule(this);
            FilterModule = new FilterModule(this);
            GetGatherParam();
        }
        private void GetGatherParam()
        {
            ChannelGatherParamA429Rx gatherParamA429 = new ChannelGatherParamA429Rx();
            uint ret = ((Channel429DriverRx)(ChannelDriver)).ChannelGatherParam(ref gatherParamA429,
     ParamOptionA429.BHT_L1_PARAM_OPT_GET);
            if (ret != 0)
            {
                RunningLog.Record(string.Format("return value is {0} when invoke ChannelGatherParam", ret));
            }
            if (gatherParamA429.gather_enable == 0)//过滤
            {
                isFilter = true;
            }
            else
            {
                isFilter = false;
            }
            if (gatherParamA429.recv_mode == RecvModeA429.BHT_L1_A429_RECV_MODE_LIST)
            {
                receiveType = "队列";
            }
            else
            {
                receiveType = "采样";
            }
            if (!string.IsNullOrEmpty(gatherParamA429.threshold_count.ToString()))
            {
                deepCount = gatherParamA429.threshold_count.ToString();
            }
            if (!string.IsNullOrEmpty(gatherParamA429.threshold_time.ToString()))
            {
                timeCount = gatherParamA429.threshold_time.ToString();
            }
        }
        protected override IChildContainer<AbstractComponent> ChildComponents
        {
            get { return _componentDic; }
        }
        private readonly ComponentDic<AbstractLabel> _componentDic = new ComponentDic<AbstractLabel>();

        public override IGetItemByIndex GetItem(int index)
        {
            return GetSpecificItem(index);
        }

        public override IGetItemByName GetItem(string name)
        {
            return GetSpecificItem(name);
        }

        public override AbstractLabel GetSpecificItem(int index)
        {
            return _componentDic.GetSpecificItem(index);
        }

        public override AbstractLabel GetSpecificItem(string name)
        {
            return _componentDic.GetSpecificItem(name);
        }

        public override void Add(AbstractLabel t)
        {
            _componentDic.Add(t);
        }

        public override void Delete(AbstractLabel t)
        {
            _componentDic.Delete(t);
        }

        public override void Remove(int index)
        {
            _componentDic.Delete(index);
        }
        public bool IsFileSaveAllow { get; set; }
        public void Receive()
        {
            if (Enabled == false || isSend == false)
            {
                return;
            }
            rxpA429Result.data = 0;
            Channel429DriverRx driverRx = (Channel429DriverRx)ChannelDriver;
            RxpA429 rxpA429 = new RxpA429();
            uint maxRxp = 1;
            int rxpNum;
            WaitStatusA429 opt = WaitStatusA429.BHT_L1_OPT_NOWAIT;
            uint ret = driverRx.ChannelRecvRx(out rxpA429, maxRxp, out rxpNum, opt);//这里设置为存储一个数据，那么可以操作的data只有一个
            if (ret != 0)
            {
                errCount++;
                RunningLog.Record(string.Format("return value is {0} when receive data", ret));
            }
            else if (rxpNum > 0 && ret == 0)
            {
                count++;
                rxpA429Result = rxpA429;//这里将out的数据保存到类中
                FileHelper.WriteLogForReceive(Convert.ToString(rxpA429.data, 2));
            }
            MibDataA429 mibDataA429;
            ret = driverRx.ChannelMibGetRx(out mibDataA429);
            if (ret != 0)
            {
                RunningLog.Record(string.Format("return value is {0} when ChannelMibGetRx data", ret));
            }
            DeviceCount = mibDataA429.cnt;
            errDeviceCount = mibDataA429.err_cnt;
            if (IsFileSaveAllow)//如果允许保存文件，那么就执行这一步操作
            {
                DataProcessModule.Save(rxpA429, rxpNum);
            }

        }
        //找数据
        public List<string> DataAnalysis(string data, string sdi, string ssm, int pageIndex, out int pageCount)
        {
            int pageCount1 = 0;
            List<string> list = DataProcessModule.GetData(data, sdi, ssm, pageIndex, out pageCount1);
            pageCount = pageCount1;
            return list;
        }
        public override void Dispose()
        {
            DataProcessModule.Dispose();
            SummaryModule.Dispose();
            FilterModule.Dispose();
            base.Dispose();
        }
    }

    /// <summary>
    /// 429发送通道
    /// </summary>
    public class Channe429Send : AbstractChannel429, ISend, IIsSelected
    {
        public Channe429Send(uint id)
        {
            if (id > 15)
            {
                throw new Exception("发送通道的ID只能在[0,15]的范围内");
            }
            ChannelID = id;
            NamePrefix = "SendChannel";
            ChannelType = ChannelType.Send;
        }

        public int labelCount = 0;

        public int errCount = 0;

        public uint DeviceCount = 0;

        public uint errDeviceCount = 0;

        public SendLabel429 currentLabel = null;

        public bool isAutoIncrement = false;//是否自增

        public bool LoopEnable { get; set; }

        public override void BuildModule()
        {
            uint devID = ((IDeviceInfo)Owner).DevID;
            Channel429DriverTx driverTx = new Channel429DriverTx(devID, ChannelID);
            ChannelDriver = driverTx;

            object baudRateValue = App.Instance.ConfigManager.GetParameter(this.Path + "_BaudRate");
            if (baudRateValue != null)
            {
                BaudRate = (int)baudRateValue;
            }

            uint ret = driverTx.ChannelLoopTx(AbleStatusA429.BHT_L1_OPT_ENABLE);
            LoopEnable = true;
            if (ret != 0)
            {
                RunningLog.Record(string.Format("return value is {0} when invoke ChannelLoopTx", ret));
            }
        }

        protected override IChildContainer<AbstractComponent> ChildComponents
        {
            get { return _componentDic; }
        }
        private readonly ComponentDic<AbstractLabel> _componentDic = new ComponentDic<AbstractLabel>();

        public override IGetItemByIndex GetItem(int index)
        {
            return GetSpecificItem(index);
        }

        public override IGetItemByName GetItem(string name)
        {
            return GetSpecificItem(name);
        }

        public override AbstractLabel GetSpecificItem(int index)
        {
            return _componentDic.GetSpecificItem(index);
        }

        public override AbstractLabel GetSpecificItem(string name)
        {
            return _componentDic.GetSpecificItem(name);
        }

        public override void Add(AbstractLabel t)
        {
            _componentDic.Add(t);
        }

        public override void Delete(AbstractLabel t)
        {
            _componentDic.Delete(t);
        }

        public override void Remove(int index)
        {
            _componentDic.Delete(index);
        }

        public void Send()
        {
            if (Enabled == false || isSend == false)
            {
                return;
            }

            foreach (var item in ChildComponents)
            {
                SendLabel429 label429 = item as SendLabel429;
                if (!label429.IsSelected)
                {
                    return;
                }
                if (label429 != null)
                {
                    currentLabel = label429;
                    Channel429DriverTx driverTx = (Channel429DriverTx)ChannelDriver;
                    uint ret = 0;
                    if (!label429.isAutoIncrement)
                    {
                        ret = driverTx.ChannelSendTx((uint)label429.ActualValue, SendOptA429.BHT_L1_A429_OPT_RANDOM_SEND);
                    }
                    else
                    {
                        label429.ActualValue += 1;
                        ret = driverTx.ChannelSendTx((uint)label429.ActualValue, SendOptA429.BHT_L1_A429_OPT_RANDOM_SEND);
                    }
                    if (ret != 0)
                    {
                        RunningLog.Record(string.Format("return value is {0} when invoke ChannelSendTx", ret));
                        errCount++;
                    }
                    else
                    {
                        labelCount++;
                        FileHelper.WriteLogForSend(Convert.ToString(label429.ActualValue, 2));
                    }
                    MibDataA429 mibDataA429;
                    ret = driverTx.ChannelMibGetTx(out mibDataA429);

                    if (ret != 0)
                    {
                        RunningLog.Record(string.Format("return value is {0} when invoke ChannelMibGetTx", ret));
                        errCount++;
                    }
                    DeviceCount = mibDataA429.cnt;
                    errDeviceCount = mibDataA429.err_cnt;
                }
            }

        }
        public bool IsSelected { get; set; }

    }
}
