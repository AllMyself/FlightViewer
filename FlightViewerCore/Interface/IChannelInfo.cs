namespace BinHong.FlightViewerCore
{
    public interface IChannelInfo : IChannelType, IChannelID
    {
        bool Enabled { get; set; }
        bool Parity { get; set; }
        int BaudRate { get; set; }
    }

    public interface ISend
    {
        void Send();
    }

    public interface IReceive
    {
        void Receive();
    }
}
