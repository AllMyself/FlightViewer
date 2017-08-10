namespace BinHong.FlightViewerCore
{
    public static class StaticMethods
    {
        //界面以boardType，boardNo，channelType，channelCount来标识唯一性。
        //core中bus，device等也应该包含这个几个属性，用它来标识唯一性
        public static AbstractDevice GetSeletecedDevice(string boardName,string deviceName)
        {
            //获取当前选择的Bus

            AbstractBus bus = App.Instance.FlightBusManager.GetBus(boardName);
           
            //获取当前选择Device
            AbstractDevice selectedDevice = null;
            if (bus != null)
            {
                selectedDevice = (AbstractDevice)bus.GetItem(deviceName);
            }

            return selectedDevice;
        }
    }
}
