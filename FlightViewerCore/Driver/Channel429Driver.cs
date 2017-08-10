using BinHong.Utilities;
using System;
using System.Runtime.InteropServices;

namespace BinHong.FlightViewerCore
{
    /// <summary>
    /// 429接收Driver
    /// </summary>
    public class Channel429DriverRx : AbstractChannelDriver
    {
        //同一个设备不支持“两个线程同时对_mibDataA429IntPtr操作。
        private readonly byte[] _channelParamInitArray;
        private readonly IntPtr _channelParamIntPtr;

        private readonly byte[] _channelGatherParamRxInitArray;
        private readonly IntPtr _channelGatherParamRxIntPtr;

        private readonly byte[] _mibDataInitArray;
        private readonly IntPtr _mibDataIntPtr;

        private readonly byte[] _channelFilterParamInitArray;
        private readonly IntPtr _channelFilterParamIntPtr;

        private readonly byte[] _rxpA429InitArray;
        private readonly IntPtr _rxpA429IntPtr;

        private readonly byte[] _rxpNumInitArray;
        private readonly IntPtr _rxpNumIntPtr;

        public Channel429DriverRx(uint deviceID, uint channelID)
        {
            DeviceID = deviceID;
            ChannelID = channelID + 1;
            int size = 0;
            size = Marshal.SizeOf(typeof(ChannelParamA429));
            _channelParamInitArray = new byte[size];
            _channelParamIntPtr = Marshal.AllocHGlobal(size);

            size = Marshal.SizeOf(typeof(ChannelGatherParamA429Rx));
            _channelGatherParamRxInitArray = new byte[size];
            _channelGatherParamRxIntPtr = Marshal.AllocHGlobal(size);

            size = Marshal.SizeOf(typeof(MibDataA429));
            _mibDataInitArray = new byte[size];
            _mibDataIntPtr = Marshal.AllocHGlobal(size);

            size = Marshal.SizeOf(typeof(ChannelFilterParamA429Rx));
            _channelFilterParamInitArray = new byte[size];
            _channelFilterParamIntPtr = Marshal.AllocHGlobal(size);

            size = Marshal.SizeOf(typeof(RxpA429));
            _rxpA429InitArray = new byte[size];
            _rxpA429IntPtr = Marshal.AllocHGlobal(size);

            size = Marshal.SizeOf(typeof(int));
            _rxpNumInitArray = new byte[size];
            _rxpNumIntPtr = Marshal.AllocHGlobal(size);
        }

        /* a429 rx channel */
        //12. ChannelParamRx       HandleRef commParameter为ChannelParamA429类型
        public uint ChannelParamRx(ref ChannelParamA429 commParameter, ParamOptionA429 paramOpt)
        {
            IntPtr ptr = _channelParamIntPtr;
            byte[] bytes = _channelParamInitArray;
            if (paramOpt == ParamOptionA429.BHT_L1_PARAM_OPT_GET)
            {
                //获取数据前清空，内存中的数据
                Marshal.Copy(bytes, 0, ptr, bytes.Length);
            }
            else
            {
                //若设置，把ref类型参数中的数据写入到内存中
                byte[] parameterBytes = UtilityConvertor.StructToBytes(commParameter);
                Marshal.Copy(parameterBytes, 0, ptr, bytes.Length);
            }
            //调用方法，获取数据
            uint ret = NativeMethods429.ChannelParamRx(DeviceID, ChannelID, ptr, paramOpt);
            //转换非托管数据为托管数据
            commParameter = null;
            if (ret == Normal)
            {
                commParameter = (ChannelParamA429)Marshal.PtrToStructure(ptr, typeof(ChannelParamA429));
            }
            return ret;
        }

        // 13. ChannelGatherParam      HandleRef data为ChannelGatherParamA429Rx类型
        public uint ChannelGatherParam(ref ChannelGatherParamA429Rx gatherParameter, ParamOptionA429 paramOpt)
        {
            IntPtr ptr = _channelGatherParamRxIntPtr;
            byte[] bytes = _channelGatherParamRxInitArray;
            //获取数据前清空，内存中的数据
            if (paramOpt == ParamOptionA429.BHT_L1_PARAM_OPT_GET)
            {
                //获取数据前清空，内存中的数据
                Marshal.Copy(bytes, 0, ptr, bytes.Length);
            }
            else
            {
                //若设置，把ref类型参数中的数据写入到内存中
                byte[] parameterBytes = UtilityConvertor.StructToBytes(gatherParameter);
                Marshal.Copy(parameterBytes, 0, ptr, bytes.Length);
            }
            //调用方法，获取数据
            uint ret = NativeMethods429.ChannelGatherParam(DeviceID, ChannelID, ptr, paramOpt);
            //转换非托管数据为托管数据
            gatherParameter = null;
            if (ret == Normal)
            {
                gatherParameter = (ChannelGatherParamA429Rx)Marshal.PtrToStructure(ptr, typeof(ChannelGatherParamA429Rx));
            }
            return ret;

        }

        // 14. ChannelMibGetRx         HandleRef data为MibDataA429类型
        public uint ChannelMibGetRx(out MibDataA429 mibDataA429)
        {
            IntPtr ptr = _mibDataIntPtr;
            byte[] bytes = _mibDataInitArray;
            //获取数据前清空，内存中的数据
            Marshal.Copy(bytes, 0, ptr, bytes.Length);
            //调用方法，获取数据
            uint ret = NativeMethods429.ChannelMibGetRx(DeviceID, ChannelID, ptr);
            //转换非托管数据为托管数据
            mibDataA429 = null;
            if (ret == Normal)
            {
                mibDataA429 = (MibDataA429)Marshal.PtrToStructure(ptr, typeof(MibDataA429));
            }
            return ret;
        }

        //15. ChannelMibClearRx
        public uint ChannelMibClearRx()
        {
            uint ret = NativeMethods429.ChannelMibClearRx(DeviceID, ChannelID);
            return ret;
        }

        //16. ChannelFilterCfgRx     HandleRef fltParam为ChannelFilterParamA429Rx类型
        //public uint ChannelFilterCfgRx(out ChannelFilterParamA429Rx filterParams)
        //{
        //    IntPtr ptr = _channelFilterParamIntPtr;
        //    byte[] bytes = _channelFilterParamInitArray;
        //    //获取数据前清空，内存中的数据
        //    Marshal.Copy(bytes, 0, ptr, bytes.Length);
        //    ChannelFilterParamA429Rx filterParam = filterParams;
        //    //调用方法，获取数据
        //    uint ret = NativeMethods429.ChannelFilterCfgRx(DeviceID, ChannelID, filterParam);
        //    //转换非托管数据为托管数据
        //    filterParams = null;
        //    if (ret == Normal)
        //    {
        //        filterParams = (ChannelFilterParamA429Rx)Marshal.PtrToStructure(ptr, typeof(ChannelFilterParamA429Rx));
        //    }
        //    return ret;
        //}
        //16. ChannelFilterCfgRx     HandleRef fltParam为ChannelFilterParamA429Rx类型 2017-07-18 by mayu
        public uint ChannelFilterCfgRxm(ChannelFilterParamA429Rx filterParam)
        {
            IntPtr ptr = _channelFilterParamIntPtr;
            Marshal.StructureToPtr(filterParam, ptr, true);//將傳入的結構參數送到非託管內存中
            uint ret = NativeMethods429.ChannelFilterCfgRx(DeviceID, ChannelID, ptr);
            string s = getMemory(filterParam);
            return ret;
        }

        //17. ChannelRecvRx            HandleRef rxpBuf为RxpA429类型
        public uint ChannelRecvRx(out RxpA429 rxpBuf, uint maxRxp, out int rxpNum,
            WaitStatusA429 opt)
        {
            IntPtr ptr = _rxpA429IntPtr;
            byte[] bytes = _rxpA429InitArray;

            //获取数据前清空，内存中的数据
            Marshal.Copy(bytes, 0, ptr, bytes.Length);
            Marshal.Copy(_rxpNumInitArray, 0, _rxpNumIntPtr, _rxpNumInitArray.Length);
            //调用方法，获取数据
            uint ret = NativeMethods429.ChannelRecvRx(DeviceID, ChannelID, ptr, maxRxp, _rxpNumIntPtr, opt);
            //转换非托管数据为托管数据
            rxpBuf = null;
            rxpNum = 0;
            if (ret == Normal)
            {
                rxpBuf = (RxpA429)Marshal.PtrToStructure(ptr, typeof(RxpA429));
                rxpNum = (int)Marshal.PtrToStructure(_rxpNumIntPtr, typeof(int));
            }
            return ret;
        }

        public override void Dispose()
        {
            Marshal.Release(_channelParamIntPtr);
            Marshal.Release(_channelGatherParamRxIntPtr);
            Marshal.Release(_mibDataIntPtr);
            Marshal.Release(_channelFilterParamIntPtr);
            Marshal.Release(_rxpA429IntPtr);
            Marshal.Release(_rxpNumIntPtr);
        }
        public static string getMemory(object o) // 获取引用类型的内存地址方法  
        {
            GCHandle h = GCHandle.Alloc(o, GCHandleType.Pinned);
            IntPtr addr = h.AddrOfPinnedObject();
            return "0x" + addr.ToString("X");
        }
    }

    /// <summary>
    /// 429发送Driver
    /// </summary>
    public class Channel429DriverTx : AbstractChannelDriver
    {
        private readonly byte[] _mibDataA429InitArray;
        private readonly IntPtr _mibDataA429IntPtr;

        private readonly byte[] _channelParamTxInitArray;
        private readonly IntPtr _channelParamTxIntPtr;

        private readonly byte[] _channelInjectParamTxInitArray;
        private readonly IntPtr _channelInjectParamTxIntPtr;

        private readonly byte[] _periodInitArray;
        private readonly IntPtr _periodIntPtr;

        public Channel429DriverTx(uint deviceID, uint channelID)
        {
            DeviceID = deviceID;
            ChannelID = channelID + 1;

            int size = 0;
            size = Marshal.SizeOf(typeof(MibDataA429));
            _mibDataA429InitArray = new byte[size];
            _mibDataA429IntPtr = Marshal.AllocHGlobal(size);

            size = Marshal.SizeOf(typeof(ChannelParamA429));
            _channelParamTxInitArray = new byte[size];
            _channelParamTxIntPtr = Marshal.AllocHGlobal(size);

            size = Marshal.SizeOf(typeof(ChannelParamA429));
            _channelInjectParamTxInitArray = new byte[size];
            _channelInjectParamTxIntPtr = Marshal.AllocHGlobal(size);

            size = Marshal.SizeOf(typeof(int));
            _periodInitArray = new byte[size];
            _periodIntPtr = Marshal.AllocHGlobal(size);
        }

        /* tx channel */

        // 5. ChannelMibGetTx      HandleRef data为MibDataA429类型
        public uint ChannelMibGetTx(out MibDataA429 data)
        {
            IntPtr ptr = _mibDataA429IntPtr;
            byte[] bytes = _mibDataA429InitArray;
            //获取数据前清空，内存中的数据
            Marshal.Copy(bytes, 0, ptr, bytes.Length);
            //调用方法，获取数据
            uint ret = NativeMethods429.ChannelMibGetTx(DeviceID, ChannelID, ptr);
            //转换非托管数据为托管数据
            data = null;
            if (ret == Normal)
            {
                data = (MibDataA429)Marshal.PtrToStructure(ptr, typeof(MibDataA429));
            }
            return ret;
        }

        // 6. ChannelMibClearTx

        public uint ChannelMibClearTx()
        {
            uint ret = NativeMethods429.ChannelMibClearTx(DeviceID, ChannelID);
            return ret;
        }

        // 7. ChannelParamTx       HandleRef commParameter为ChannelParamA429类型//发送通道参数设置

        public uint ChannelParamTx(ref ChannelParamA429 commParameter, ParamOptionA429 paramOpt)
        {
            IntPtr ptr = _channelParamTxIntPtr;
            byte[] bytes = _channelParamTxInitArray;
            if (paramOpt == ParamOptionA429.BHT_L1_PARAM_OPT_GET)
            {
                //获取数据前清空，内存中的数据
                Marshal.Copy(bytes, 0, ptr, bytes.Length);
            }
            else
            {
                //若设置，把ref类型参数中的数据写入到内存中
                byte[] parameterBytes = UtilityConvertor.StructToBytes(commParameter);
                Marshal.Copy(parameterBytes, 0, ptr, bytes.Length);
            }
            //调用方法，获取数据
            uint ret = NativeMethods429.ChannelParamTx(DeviceID, ChannelID, ptr, paramOpt);
            //转换非托管数据为托管数据
            commParameter = null;
            if (ret == Normal)
            {
                commParameter = (ChannelParamA429)Marshal.PtrToStructure(ptr, typeof(ChannelParamA429));
            }
            return ret;
        }

        //8. ChanInjectParamTx.   HandleRef injectParam为ChannelInjectParamA429Tx类型
        public uint ChannelInjectParamTx(out ChannelInjectParamA429Tx injectParam, ParamOptionA429 paramOpt)
        {
            IntPtr ptr = _channelInjectParamTxIntPtr;
            byte[] bytes = _channelInjectParamTxInitArray;
            //获取数据前清空，内存中的数据
            Marshal.Copy(bytes, 0, ptr, bytes.Length);
            //调用方法，获取数据
            uint ret = NativeMethods429.ChannelInjectParamTx(DeviceID, ChannelID, ptr, paramOpt);
            //转换非托管数据为托管数据
            injectParam = null;
            if (ret == Normal)
            {
                injectParam = (ChannelInjectParamA429Tx)Marshal.PtrToStructure(ptr, typeof(ChannelInjectParamA429Tx));
            }
            return ret;
        }

        //9. ChanLoopTx
        public uint ChannelLoopTx(AbleStatusA429 opt)
        {
            uint ret = NativeMethods429.ChannelLoopTx(DeviceID, ChannelID, opt);
            return ret;
        }

        //10. ChanSendTx
        public uint ChannelSendTx(uint data, SendOptA429 opt)
        {
            uint ret = NativeMethods429.ChannelSendTx(DeviceID, ChannelID, opt, data);
            return ret;
        }

        //11. ChanSlopeCfgTx
        public uint ChannelSlopeCfgTx(SlopeA429 slope)
        {
            uint ret = NativeMethods429.ChannelSlopeCfgTx(DeviceID, ChannelID, slope);
            return ret;
        }

        //12
        public uint ChannelPeriodParamTx(int period, ParamOptionA429 opt)
        {
            IntPtr ptr = _periodIntPtr;
            byte[] bytes = BitConverter.GetBytes(period);

            //设置托管数据到非托管内存
            Marshal.Copy(bytes, 0, ptr, bytes.Length);
            //调用方法，获取数据
            uint ret = NativeMethods429.ChannelPeriodParamTx(DeviceID, ChannelID, ptr, opt);
            return ret;
        }

        public override void Dispose()
        {
            Marshal.Release(_mibDataA429IntPtr);
            Marshal.Release(_channelParamTxIntPtr);
            Marshal.Release(_channelInjectParamTxIntPtr);
        }
    }
}
