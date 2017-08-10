using System;
using BinHong.FlightViewerCore;

namespace BinHong.FlightViewerVM
{
    public class MainWindowVm
    {
        public void Update(IDeviceInfo info)
        {
            if (UpdateUi != null)
            {
                UpdateUi(info);
            }
        }

        public event Action<IDeviceInfo> UpdateUi;
    }
}
