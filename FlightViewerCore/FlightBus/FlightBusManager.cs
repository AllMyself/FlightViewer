using System.Collections.Generic;
using BinHong.Utilities;

namespace BinHong.FlightViewerCore
{
    public class FlightBusManager : AbstractComponent, IBuildModule, ICheckDevice, ILogin, IGetBus
    {
       public readonly Bus429 Bus429 = new Bus429();
       public readonly Bus1553 Bus1553 = new Bus1553();


        //检测板卡信息
        public List<IDeviceInfo> CheckDevice()
        {
            List<IDeviceInfo> list = new List<IDeviceInfo>();
            DeviceIDManager deviceIDManager = new DeviceIDManager();
            List<uint> deviceIDList = deviceIDManager.GetProperAllDeviceID();
            //遍历所有可能的devID，看哪些是在线的
            for (int i = 0; i < deviceIDList.Count; i++)
            {
                uint devID = deviceIDList[i];
                //使用429也可以检测处1553的设备，所以此处就用429的就可以了。
                uint ret = Bus429.DriverOperate.DeviceProbe(devID);
                if (ret != 0)
                {
                    RunningLog.Record(string.Format("return value is {0} when invoke DeviceProbe", ret));
                }
                if (ret == 0)
                {
                    DeviceInfo deviceInfo = new DeviceInfo();
                    uint backplaneType;
                    uint boardNumber;
                    BoardType boardType;
                    DeviceIDManager.AnalysisDevID(devID, out backplaneType, out boardType, out boardNumber);
                    deviceInfo.BoardNo = boardNumber;
                    deviceInfo.BoardType = boardType;
                    deviceInfo.DevID = devID;
                    list.Add(deviceInfo);
                }
            }
            return list;
        }
        //登陆设备，初始化设备，并且将设备的信息注册到组件，接收发送通道在这一步已经初始化了
        public void Login(IDeviceInfo info)
        {
            Bus429.Login(info);
            Bus1553.Login(info);
        }
        //获取板卡dic的操作
        protected override IChildContainer<AbstractComponent> ChildComponents
        {
            get
            {
                return _busDic;
            }
        }
        //根据index获取注册板卡操作
        public override IGetItemByIndex GetItem(int index)
        {
            if (index == 0)
            {
                return Bus429;
            }
            if (index ==1)
            {
                return Bus1553;
            }
            return null;
        }
        //根据名字获取注册板卡操作
        public override IGetItemByName GetItem(string name)
        {
            if (name == BoardType.A429.ToString())
            {
                return Bus429;
            }
            if (name == BoardType.A1553.ToString())
            {
                return Bus1553;
            }
            return null;
        }

        private ComponentDic<AbstractComponent> _busDic;
        //这个不要想得太高端，只不过是将板卡信息建立为dic，便于管理，与前端的使用而已
        public void BuildModule()
        {
            _busDic = new ComponentDic<AbstractComponent>();
            _busDic.Add(Bus429);
            _busDic.Add(Bus1553);
        }
        //根据name获取板卡的操作方式
        public AbstractBus GetBus(string name)
        {
            return (AbstractBus)_busDic.GetSpecificItem(name);
        }
    }
}
