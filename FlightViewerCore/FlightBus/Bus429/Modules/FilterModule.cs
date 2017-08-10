namespace BinHong.FlightViewerCore
{
    //暂时没想到如何去使用这个module，所以我在control里面做了这些东西
    public class FilterModule : IFilter, IComponent
    {
        public Device429 _device429;
        private readonly Channe429Receive _receive429;
        public Label429 label { get; set; }
        public byte SDI { get; set; }
        public byte SSM { get; set; }
        public bool filterMode { get; set; }//黑名单(true)与白名单(false)的状态

        public FilterModule(Channe429Receive receive429)
        {
            Owner = receive429;
            _receive429 = receive429;
        }
        public void SetFilter()
        {
            ChannelFilterParamA429Rx channelFilterParamA429Rx = new ChannelFilterParamA429Rx();
            if (filterMode)
            {
                channelFilterParamA429Rx.filterMode = 0;
            }
            else
            {
                channelFilterParamA429Rx.filterMode = 1;
            }
            channelFilterParamA429Rx.sdi = SDI;
            channelFilterParamA429Rx.ssm = SSM;
            Channel429DriverRx channel429DriverRx = new Channel429DriverRx(_device429.DevID, _receive429.ChannelID);
            channel429DriverRx.ChannelFilterCfgRxm(channelFilterParamA429Rx);
        }
        public IOwner Owner { get; private set; }
        public void Dispose()
        {

        }
    }
}
