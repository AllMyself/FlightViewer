using System;
using BinHong.FlightViewerCore;

namespace BinHong.FlightViewerVM
{
    [Serializable]
    public class DeviceUiInfo:IDeviceInfo
    {
        public string Name { get; set; }
        public uint BoardNo { get; set; }
        public BoardType BoardType { get; set; }
        public ChannelType ChannelType { get; set; }
        public uint DevID { get; set; }
        public uint ChannelCount { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected { get; set; }

        public override string ToString()
        {
            return BoardType.ToString() + BoardNo + ChannelType + ChannelCount;
        }
    }
}
