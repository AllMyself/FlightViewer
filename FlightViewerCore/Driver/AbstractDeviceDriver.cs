using System;

namespace BinHong.FlightViewerCore
{
    internal abstract class AbstractDeviceOperator : IDriverOperate
    {
        public abstract string ErrorToString(uint errID);
        public abstract uint DeviceProbe(uint devID);
        public abstract uint DeviceRemove(uint devID);

    }

    public abstract class AbstractChannelDriver : IChannelID, IDeviceID,IDisposable
    {
        public uint DeviceID { get; protected set; }
        public uint ChannelID { get; protected set; }
        public abstract void Dispose();

        public static readonly uint Normal = 0;
    }
}
