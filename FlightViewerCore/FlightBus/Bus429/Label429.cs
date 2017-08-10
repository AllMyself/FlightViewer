using BinHong.Utilities;
using System;
namespace BinHong.FlightViewerCore
{
    public abstract class Label429 : AbstractLabel, ILabel429Info, IIsSelected
    {
        public Label429(string name)
        {
            Name = name;
        }
        public Label429()
        {
        }

        protected override IChildContainer<AbstractComponent> ChildComponents
        {
            get { return _componentDic; }
        }
        private readonly ComponentDic<AbstractComponent> _componentDic = new ComponentDic<AbstractComponent>();

        public override IGetItemByIndex GetItem(int index)
        {
            return _componentDic.GetSpecificItem(index);
        }

        public override IGetItemByName GetItem(string name)
        {
            return _componentDic.GetSpecificItem(name);
        }

        public int ActualValue { get; set; }

        public int Label
        {
            get { return (byte)(ActualValue & 0xff); }
            set { ActualValue = ((ActualValue & (~0xff)) | (value & 0xff)); }
        }

        public int SDI
        {
            get { return (byte)((ActualValue >> 8) & 0x3); }
            set { ActualValue = ((ActualValue & (~(0x3 << 8))) | ((value & 0x3) << 8)); }
        }

        public int Data
        {
            get { return (byte)((ActualValue >> 10) & 0x7FFFF); }
            set { ActualValue = ((ActualValue & (~(0x7FFFF << 10))) | ((value & 0x7FFFF) << 10)); }
        }

        public int SymbolState
        {
            get { return (byte)((ActualValue >> 29) & 0x3); }
            set { ActualValue = ((ActualValue & (~(0x3 << 29))) | ((value & 0x3) << 29)); }
        }

        public int Parity
        {
            get { return (byte)((ActualValue >> 31) & 0x1); }
            set { ActualValue = ((ActualValue & (~(0x1 << 31))) | (value & 0x1) << 31); }
        }
        public bool IsSelected { get; set; }
        public bool isAutoIncrement { get; set; }
    }

    public class ReceiveLabel429 : Label429
    {
        public ReceiveLabel429(string name)
            : base(name)
        {
        }

        public ReceiveLabel429()
        {
        }

    }

    public class SendLabel429 : Label429
    {
        public SendLabel429(string name)
            : base(name)
        {

        }

        /// <summary>
        /// 发送间隔
        /// </summary>
        public int Interval { get; set; }
        public int cycleInterval { get; set; }
        //软件上面的周期发送
        public void Send()
        {
            if (Owner == null)
            {
                return;
            }
            Channe429Send send = (Channe429Send)Owner;
            Channel429DriverTx driverTx = (Channel429DriverTx)send.ChannelDriver;
            uint ret = 0;
            if (!isAutoIncrement)
            {
                ret = driverTx.ChannelSendTx((uint)ActualValue, SendOptA429.BHT_L1_A429_OPT_RANDOM_SEND);
            }
            else
            {
                ActualValue += 1;
                ret = driverTx.ChannelSendTx((uint)ActualValue, SendOptA429.BHT_L1_A429_OPT_RANDOM_SEND);
            }
            if (ret != 0)
            {
                RunningLog.Record(string.Format("return value is {0} when invoke ChannelSendTx", ret));
                send.errCount++;
            }
            else
            {
                send.labelCount++;
                FileHelper.WriteLogForSend(Convert.ToString(ActualValue, 2));
            }
            MibDataA429 mibDataA429;
            ret = driverTx.ChannelMibGetTx(out mibDataA429);

            if (ret != 0)
            {
                RunningLog.Record(string.Format("return value is {0} when invoke ChannelMibGetTx", ret));
                send.errCount++;
            }
            send.DeviceCount = mibDataA429.cnt;
            send.errDeviceCount = mibDataA429.err_cnt;
        }
    }
}
