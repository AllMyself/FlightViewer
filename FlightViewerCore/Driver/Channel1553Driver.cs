namespace BinHong.FlightViewerCore
{
    class Channel1553DriverRx : AbstractChannelDriver
    {
        public Channel1553DriverRx(uint ownerID, uint id)
        {
            DeviceID = ownerID;
            ChannelID = id;
        }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }

    class Channel1553DriverTx : AbstractChannelDriver
    {
        public Channel1553DriverTx(uint ownerID, uint id)
        {
            DeviceID = ownerID;
            ChannelID = id;
        }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
