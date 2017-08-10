using System.Collections.Generic;

namespace BinHong.FlightViewerCore
{
    public interface IDriverOperate
    {
        string ErrorToString(uint errID);

        uint DeviceProbe(uint devID);

        uint DeviceRemove(uint devID);
    }

    public interface IChannelID
    {
        uint ChannelID { get;}
    }

    public interface IDeviceID
    {
        uint DeviceID { get; }
    }

    public interface IGetProperAllDeviceID
    {
        List<uint> GetProperAllDeviceID();
    }
}
