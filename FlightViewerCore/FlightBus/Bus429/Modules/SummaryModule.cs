namespace BinHong.FlightViewerCore
{
    public class SummaryModule : ISummary, IComponent
    {
        private readonly Channe429Receive _receive429;

        public SummaryModule(Channe429Receive receive429)
        {
            Owner = receive429;
            _receive429 = receive429;
        }

        public IOwner Owner { get;  private set; }
        public void Dispose()
        {
            
        }
    }
}
