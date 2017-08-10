namespace BinHong.FlightViewerCore
{
    public interface IDeviceInfo : IBoardType, IBoardNo, IChannelType
    {
        uint DevID { get; set; }
         uint ChannelCount { get; set; }
    }

    public interface IBoardType
    {
        BoardType BoardType { get; set; }
    }

    public interface IBoardNo
    {
        uint BoardNo { get; set; }
    }

    public interface IChannelType
    {
        ChannelType ChannelType { get; set; }
    }

    public enum BoardType
    {
        A429,
        A1553
    }

    public enum ChannelType
    {
        Receive,
        Send
    }
}
