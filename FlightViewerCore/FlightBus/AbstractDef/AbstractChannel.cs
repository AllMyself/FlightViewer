namespace BinHong.FlightViewerCore
{
    public abstract class AbstractChannel : AbstractPathComponent, IInitialize, IBuildModule, IChannelID
    {
        public string NamePrefix = "Channel_";

        public AbstractChannelDriver ChannelDriver { get; protected set; }

        public uint ChannelID { get; protected set; }

        /// <summary>
        /// Channel为核心变量，我想应该需要一个Initialize函数
        /// </summary>
        public virtual void Initialize()
        {
            Name = NamePrefix + ChannelID;
        }

        public abstract void BuildModule();
    }
}
