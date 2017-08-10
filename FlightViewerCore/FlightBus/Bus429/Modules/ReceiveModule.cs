using System.Threading;

namespace BinHong.FlightViewerCore
{
    public class ReceiveModule : A429AbstractTxAndRxModule
    {
        public ReceiveModule(Device429 device429)
            : base(device429)
        {
        }


        protected override void OnProcess()
        {
            while (true)
            {
                foreach (var ch in Device429.ReceiveComponents)
                {
                    IReceive receiveItem = ch as IReceive;
                    if (receiveItem != null)
                    {
                        receiveItem.Receive();
                    }
                }
                Thread.Sleep(500);
            }
        }
    }
}
