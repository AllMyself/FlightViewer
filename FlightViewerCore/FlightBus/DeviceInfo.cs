namespace BinHong.FlightViewerCore
{
    public class DeviceInfo : IDeviceInfo
    {
        public BoardType BoardType { get; set; }
        public uint BoardNo { get; set; }
        public ChannelType ChannelType { get; set; }
        public uint DevID { get; set; }
        public uint ChannelCount { get; set; }
    }
}
