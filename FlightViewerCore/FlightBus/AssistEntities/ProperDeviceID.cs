using System.Collections.Generic;

namespace BinHong.FlightViewerCore
{
    //根据devID
    internal class DeviceIDManager:IGetProperAllDeviceID
    {
        private class BitArea
        {
            public int StartPosition;
            public int MinValue;
            public int MaxValue;
        }

        private readonly BitArea _bitArea8 = new BitArea() {MaxValue = 0x2, MinValue = 0x0, StartPosition = 7};
        //按照bht_L0描述,MinValue是0x10标识429端,MinValue是0到0x10标识1553
        private readonly BitArea _bitArea76 = new BitArea() {MaxValue = 0x1f, MinValue = 0, StartPosition = 5};
        private readonly BitArea _bitArea5 = new BitArea() {MaxValue = 0xf, MinValue = 0, StartPosition = 4};
        //计算所有可能的设备的ID
        public List<uint> GetProperAllDeviceID()
        {
            List<uint> list = new List<uint>();
            for (int value8 = _bitArea8.MinValue; value8 <= _bitArea8.MaxValue; value8++)
            {
                int devID = 0;
                int initialDevID1 = 0;
                devID = ((0xffffff & value8) << (_bitArea8.StartPosition * 4)) | devID;
                initialDevID1 = devID;
                for (int value76 = _bitArea76.MinValue; value76 <= _bitArea76.MaxValue; value76++)
                {
                    devID = initialDevID1;
                    devID = ((0xffffff & value76) << (_bitArea76.StartPosition * 4)) | devID;
                    int initialDevID2 = devID;
                    for (int value5 = _bitArea5.MinValue; value5 <= _bitArea5.MaxValue; value5++)
                    {
                        devID = initialDevID2;
                        devID = ((0xffffff & value5) << (_bitArea5.StartPosition * 4)) | devID;

                        list.Add((uint)devID);
                    }
                }
            }
            return list;
        }
        //通过devID去解析板卡的信息，背板型号，板卡型号，板卡编号等
        public static void AnalysisDevID(uint devID, out uint backplaneType, out BoardType boardType,out uint boardNumber)
        {
            backplaneType = (devID & 0xF0000000) >> 28;
            boardType = (devID & 0x0FF00000) > 0x00F00000 ? BoardType.A429 : BoardType.A1553;
            boardNumber = devID & 0x000F0000;
        }
    }    
}
