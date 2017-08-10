using System.Collections.Generic;

namespace BinHong.FlightViewerCore
{
    public interface ICheckDevice
    {
        List<IDeviceInfo> CheckDevice();
    }

    public interface IGetBus
    {
        AbstractBus GetBus(string name);
    }
}
