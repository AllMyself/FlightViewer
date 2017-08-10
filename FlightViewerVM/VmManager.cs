using BinHong.FlightViewerCore;

namespace BinHong.FlightViewerVM
{
    public class VmManager
    {
        public static readonly MainWindowVm MainWindowVm = new MainWindowVm();

        public static void GetBusAndDevice(string name, out AbstractBus bus, out AbstractDevice device)
        {
            string[] pathParts = name.Split('_');
            string busName = pathParts[1];
            string deviceName = pathParts[2];
            bus = App.Instance.FlightBusManager.GetBus(busName);
            device = (AbstractDevice)bus.GetItem(deviceName);
        }
    }
}
