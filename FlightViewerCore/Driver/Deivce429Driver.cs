using System;
using System.Runtime.InteropServices;

namespace BinHong.FlightViewerCore
{
    internal class Device429Operator : AbstractDeviceOperator
    {
        //获取设备的异常信息
        public override string ErrorToString(uint errID)
        {
            IntPtr strIntPtr = NativeMethods429.ErrorToString(errID);
            string errorMsg = Marshal.PtrToStringAnsi(strIntPtr);
            return errorMsg;
        }
        //获取板卡信息
        public override uint DeviceProbe(uint devID)
        {
            uint ret = NativeMethods429.DeviceProbe(devID);
            return ret;
        }
        //移除板卡
        public override uint DeviceRemove(uint devID)
        {
            uint ret = NativeMethods429.DeviceRemove(devID);
            return ret;
        }
        //设备复位
        public uint DefaultInit(uint devID)
        {
            uint ret = NativeMethods429.DefaultInit(devID);
            return ret;
        }
        //设备信息读取
        public uint FpgaEepromRead(uint devID, ushort addr, ref IntPtr data)
        {
            uint ret = NativeMethods429.FpgaEepromRead(devID,addr,data);
            return ret;
        }
        //设备信息写入
        public uint FpgaEepromWrite(uint devID, ushort addr, byte data)
        {
            uint ret = NativeMethods429.FpgaEepromWrite(devID, addr, data);
            return ret;
        }
    }
}
