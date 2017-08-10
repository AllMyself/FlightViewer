using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using BinHong.FlightViewerCore;
using BinHong.Utilities;
using UiControls;


namespace BinHong.FlightViewerVM
{
    public class NewDeviceUi
    {
        public readonly StatusStripMsgShow MsgShow = new StatusStripMsgShow();

        /// <summary>
        /// 可以绑定到界面List的设备信息列表
        /// </summary>
        public BindingList<DeviceUiInfo> DeviceUiInfos
        {
            get
            {
                return _deviceUiInfos;
            }
        }

        private readonly BindingList<DeviceUiInfo> _deviceUiInfos=new BindingList<DeviceUiInfo>();

        /// <summary>
        /// 检测所有设备
        /// </summary>
        public void CheckAllDevice()
        {
            MsgShow.ShowWarning("正在检测设备。。。");
            MainThreadDelayAction.Run(3000, MsgShow.Clear);
            List<IDeviceInfo> deviceIdList = App.Instance.FlightBusManager.CheckDevice();
#if Test
             deviceIdList=new List<IDeviceInfo>()
             {
                 new DeviceInfo(){BoardNo = 1,BoardType = BoardType.A429},
             new DeviceInfo(){BoardNo = 1,BoardType = BoardType.A1553}
             };
#endif

            int i = 0;
            foreach (IDeviceInfo item in deviceIdList)
            {
                DeviceUiInfo info=new DeviceUiInfo();
                info.BoardNo = item.BoardNo;
                info.BoardType = item.BoardType;
                info.DevID = item.DevID;
                info.Name = "localDevice_"+i++;
                info.IsSelected = true;
                _deviceUiInfos.Add(info);
            }
            MsgShow.ShowWarning("设备检测完毕。。。");
            MainThreadDelayAction.Run(3000, MsgShow.Clear);
        }

        public void AddDevice(DeviceUiInfo deviceUiInfo)
        {
            DeviceUiInfos.Add(deviceUiInfo);
        }
      

        public void ClearAllDevice()
        {
            
        }

        public void NewDevice()
        {
            
        }

        public void Login()
        {
            foreach (var deviceUiInfo in _deviceUiInfos)
            {
                if (deviceUiInfo.IsSelected)
                {
                    App.Instance.FlightBusManager.Login(deviceUiInfo);
                    VmManager.MainWindowVm.Update(deviceUiInfo);
                }
            }
        }

        public void DelDevice(DeviceUiInfo info)
        {
            DeviceUiInfos.Remove(info);
        }

        public void LoadConfig(string fileName)
        {
            if (File.Exists(fileName))
            {
                string content = File.ReadAllText(fileName);
                BindingList<DeviceUiInfo> deviceUiInfos =
                    SimpleSerializer.Deserialize(DeviceUiInfos.GetType(), content) as BindingList<DeviceUiInfo>;
                if (deviceUiInfos != null)
                {
                    DeviceUiInfos.Clear();
                    foreach (var deviceUiInfo in deviceUiInfos)
                    {
                        DeviceUiInfos.Add(deviceUiInfo);
                    }
                }
            }
        }

        public void SaveConfig(string fileName)
        {
            string content = SimpleSerializer.Serialize(DeviceUiInfos);
            File.WriteAllText(fileName, content);
        }
    }
}
