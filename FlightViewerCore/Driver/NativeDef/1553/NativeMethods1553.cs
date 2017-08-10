using System;
using System.Runtime.InteropServices;

namespace BinHong.FlightViewerCore
{
    internal class NativeMethods1553
    {
        /* general */

        // 1. ErrorToString
        [DllImport(NativeDll.Path, EntryPoint = NativeDll.ErrorToString, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ErrorToString(uint errID);

        // 2. DeviceProbe
        [DllImport(NativeDll.Path, EntryPoint = NativeDll.DeviceProbe, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint DeviceProbe(uint devID);

        // 3. DeviceRemove
         [DllImport(NativeDll.Path, EntryPoint = NativeDll.DeviceRemove, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint DeviceRemove(uint devID);

         // 4. DefaultInit
         [DllImport(NativeDll.Path, EntryPoint = NativeDll.DefaultInit, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint DefaultInit(uint devID);
    }
}
