namespace BinHong.FlightViewerCore
{
    public class Bus1553 : AbstractBus, IGetItem<Device1553>
    {
        public Bus1553()
        {
            BoardType = BoardType.A1553;
            Name = BoardType.ToString();//此处以BoardType来作为Name，标识唯一性。
            DriverOperate = new Device1553Operator();
        }

        protected override IChildContainer<AbstractComponent> ChildComponents
        {
            get { return _componentDic; }
        }

        private readonly ComponentDic<Device1553> _componentDic = new ComponentDic<Device1553>();

        public Device1553 GetSpecificItem(int index)
        {
            return _componentDic.GetSpecificItem(index);
        }

        public Device1553 GetSpecificItem(string name)
        {
            return _componentDic.GetSpecificItem(name);
        }

        public override void Login(IDeviceInfo info)
        {
            if (BoardType == info.BoardType)
            {
                foreach (var component in ChildComponents)
                {
                    IDeviceInfo device = (IDeviceInfo) component;
                    if (device.BoardNo == info.BoardNo
                        && device.BoardType == info.BoardType
                        && device.ChannelCount == info.ChannelCount
                        && device.ChannelType == info.ChannelType)
                    {
                        return;
                    }
                }
                Device1553 dev = new Device1553();
                dev.InitializeParameter(info);
                Add(dev);
            }
        }

        public override void Logout(string name)
        {
            
        }

        public override IGetItemByIndex GetItem(int index)
        {
            throw new System.NotImplementedException();
        }

        public override IGetItemByName GetItem(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
