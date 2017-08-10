using System;
using System.Runtime.InteropServices;

namespace BinHong.FlightViewerCore
{
    internal class NativeMethods429
    {
        /* general */

        // 1. ErrorToString 根据错误字去获取错误信息
        [DllImport(NativeDll.Path, EntryPoint = NativeDll.ErrorToString, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ErrorToString(uint errID);

        // 2. DeviceProbe 设备检测
        [DllImport(NativeDll.Path, EntryPoint = NativeDll.DeviceProbe, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint DeviceProbe(uint devID);

        // 3. DeviceRemove 设备移除
         [DllImport(NativeDll.Path, EntryPoint = NativeDll.DeviceRemove, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint DeviceRemove(uint devID);

         // 4. DefaultInit 设备初始化
         [DllImport(NativeDll.Path, EntryPoint = NativeDll.DefaultInit, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint DefaultInit(uint devID);

         /************************************************************* tx channel **********************************/

         // 5. ChannelMibGetTx   发送通道获取统计数据
         [DllImport(NativeDll.Path, EntryPoint = NativeDll.ChannelMibGetTx, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ChannelMibGetTx(uint devID, uint chanID, IntPtr data);
         // 6. ChannelMibClearTx  清除发送通道统计数据
         [DllImport(NativeDll.Path, EntryPoint = NativeDll.ChannelMibClearTx, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ChannelMibClearTx(uint devID, uint chanID);

         // 7. ChannelParamTx 发送通道通用参数配置
         [DllImport(NativeDll.Path, EntryPoint = NativeDll.ChannelParamTx, CallingConvention = CallingConvention.Cdecl)]
         public static extern uint ChannelParamTx(uint devID, uint chanID,IntPtr commParameter, ParamOptionA429 paramOpt);

         //8. ChanInjectParamTx.  发送通道错误注入参数配置
         [DllImport(NativeDll.Path, EntryPoint = NativeDll.ChannelInjectParamTx, CallingConvention = CallingConvention.Cdecl)]
         public static extern uint ChannelInjectParamTx(uint devID, uint chanID, IntPtr injectParam, ParamOptionA429 paramOpt);

         //9. ChanLoopTx 发送通道回环配置
         [DllImport(NativeDll.Path, EntryPoint = NativeDll.ChannelLoopTx, CallingConvention = CallingConvention.Cdecl)]
         public static extern uint ChannelLoopTx(uint devID, uint chanID, AbleStatusA429 opt);

         //10. ChanSendTx  发送数据
         [DllImport(NativeDll.Path, EntryPoint = NativeDll.ChannelSendTx, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ChannelSendTx(uint devID, uint chanID, SendOptA429 opt,uint data);

         //11. ChanSlopeCfgTx 倾斜配置
         [DllImport(NativeDll.Path, EntryPoint = NativeDll.ChannelSlopeCfgTx, CallingConvention = CallingConvention.Cdecl)]
         public static extern uint ChannelSlopeCfgTx(uint devID, uint chanID, SlopeA429 slope);

         //12.ChannelPeriodParamTx 发送一段数据
        [DllImport(NativeDll.Path, EntryPoint = NativeDll.ChannelPeriodParamTx, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ChannelPeriodParamTx(uint devID,uint chanID,IntPtr period,ParamOptionA429 paramOpt);


        /********************************************* a429 rx channel *****************************************/

         //13. ChannelParamRx   接收通道参数配置    IntPtr commParameter为ChannelParamA429类型
         [DllImport(NativeDll.Path, EntryPoint = NativeDll.ChannelParamRx, CallingConvention = CallingConvention.Cdecl)]
         public static extern uint ChannelParamRx(uint devID, uint chanID,IntPtr commParameter,ParamOptionA429 paramOpt);

         // 14. ChannelGatherParam      IntPtr data为ChannelGatherParamA429Rx类型
         [DllImport(NativeDll.Path, EntryPoint = NativeDll.ChannelGatherParam, CallingConvention = CallingConvention.Cdecl)]
         public static extern uint ChannelGatherParam(uint devID, uint chanID, IntPtr gatherParameter, ParamOptionA429 paramOpt);

         // 15. ChannelMibGetRx     获取接收通道统计数据    IntPtr data为MibDataA429类型
         [DllImport(NativeDll.Path, EntryPoint = NativeDll.ChannelMibGetRx, CallingConvention = CallingConvention.Cdecl)]
         public static extern uint ChannelMibGetRx(uint devID, uint chanID, IntPtr data);
         //16. ChannelMibClearRx 清除接收通道统计数据
         [DllImport(NativeDll.Path, EntryPoint = NativeDll.ChannelMibClearRx, CallingConvention = CallingConvention.Cdecl)] 
         public static extern uint ChannelMibClearRx(uint devID, uint chanID);

         //17. ChannelFilterCfgRx   过滤器  IntPtr fltParam为ChannelFilterParamA429Rx类型
         [DllImport(NativeDll.Path, EntryPoint = NativeDll.ChannelFilterCfgRx, CallingConvention = CallingConvention.Cdecl)]
         public static extern uint ChannelFilterCfgRx(uint devID, uint chanID, IntPtr filterParam);

         //18. ChannelRecvRx        接收数据    IntPtr rxpBuf为RxpA429类型
         [DllImport(NativeDll.Path, EntryPoint = NativeDll.ChannelRecvRx, CallingConvention = CallingConvention.Cdecl)]
         public static extern uint ChannelRecvRx(uint devID, uint chanID, IntPtr rxpBuf, uint maxRxp, IntPtr rxpNum, WaitStatusA429 opt);

        /********************************************* a429 rx Msg *****************************************/

         [DllImport(NativeDll.Path, EntryPoint = NativeDll.FpgaEepromRead, CallingConvention = CallingConvention.Cdecl)]
         public static extern uint FpgaEepromRead(uint devID, ushort addr, IntPtr data);

         [DllImport(NativeDll.Path, EntryPoint = NativeDll.FpgaEepromWrite, CallingConvention = CallingConvention.Cdecl)]
         public static extern uint FpgaEepromWrite(uint devID, ushort addr, byte data);
    }
}
