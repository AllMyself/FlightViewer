namespace BinHong.FlightViewerCore
{
    public interface IChannelInfo : IChannelType, IChannelID
    {
        bool Enabled { get; set; }
        int Parity { get; set; }
        int BaudRate { get; set; }
    }
    public interface IChannelInfoMsg : IChannelType, IChannelID
    {
        bool Enabled { get; set; }
        string Parity { get; set; }
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

    public interface IChannelParameter
    {
        void SetChannelParameter();
    }
}
