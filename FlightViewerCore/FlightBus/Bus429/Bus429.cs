using BinHong.Utilities;

namespace BinHong.FlightViewerCore
{
    public class Bus429 : AbstractBus,IGetItem<Device429>
    {
        public Bus429()
        {
            BoardType = BoardType.A429;
            Name = BoardType.ToString();//此处以BoardType来作为Name，标识唯一性。
            DriverOperate=new Device429Operator();
            Path="BinHong_" + this.Name;
        }

        protected override IChildContainer<AbstractComponent> ChildComponents
        {
            get { return _componentDic; }
        }

        private readonly ComponentDic<Device429> _componentDic = new ComponentDic<Device429>();

        public Device429 GetSpecificItem(int index)
        {
            return _componentDic.GetSpecificItem(index);
        }

        public Device429 GetSpecificItem(string name)
        {
            return _componentDic.GetSpecificItem(name);
        }

        public override void Login(IDeviceInfo info)
        {
            if (BoardType == info.BoardType)
            {
                foreach (var component in ChildComponents)
                {
                    IDeviceInfo device = (IDeviceInfo)component;
                    if (device.BoardNo == info.BoardNo
                        && device.BoardType == info.BoardType
                        && device.ChannelCount == info.ChannelCount
                        && device.ChannelType == info.ChannelType)
                    {
                        return;
                    }
                }
                //建立设备前初始化
                uint ret=((Device429Operator) DriverOperate).DefaultInit(info.DevID);
                if (ret != 0)
                {
                    RunningLog.Record(string.Format("return value is {0} when invoke ChannelSendTx", ret));
                }
                //建立设备
                Device429 dev = new Device429();
                dev.InitializeParameter(info);
                Add(dev);

                dev.BuildModule();

                dev.ReceiveModule.Start();
                dev.SendModule.Start();
            }
        }

        public override void Logout(string name)
        {
            Device429 device429=GetSpecificItem(name);
            device429.ReceiveModule.Stop();
            device429.SendModule.Stop();
            DriverOperate.DeviceRemove(device429.DevID);
        }

        public override IGetItemByIndex GetItem(int index)
        {
            return GetSpecificItem(index);
        }

        public override IGetItemByName GetItem(string name)
        {
            return GetSpecificItem(name);
        }
    }
}
