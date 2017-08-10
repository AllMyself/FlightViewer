using System;
using System.Runtime.InteropServices;

namespace BinHong.FlightViewerCore
{
    class Device1553Operator : AbstractDeviceOperator
    {
        public override string ErrorToString(uint errID)
        {
            IntPtr strIntPtr = NativeMethods429.ErrorToString(errID);
            string errorMsg = Marshal.PtrToStringAnsi(strIntPtr);
            return errorMsg;
        }

        public override uint DeviceProbe(uint devID)
        {
            uint ret = NativeMethods429.DeviceProbe(devID);
            return ret;
        }

        public override uint DeviceRemove(uint devID)
        {
            uint ret = NativeMethods429.DeviceRemove(devID);
            return ret;
        }

        public uint DefaultInit(uint devID)
        {
            uint ret = NativeMethods429.DefaultInit(devID);
            return ret;
        }
    }
}
