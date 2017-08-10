using System.Threading;

namespace BinHong.FlightViewerCore
{
    public abstract class A429AbstractTxAndRxModule : IStartStop
    {
        protected readonly Device429 Device429;
        private readonly CstThread _thread = new CstThread();

        protected A429AbstractTxAndRxModule(Device429 device429)
        {
            Device429 = device429;
        }

        public void Start()
        {
            _thread.ThreadEvent += OnProcess;
        }

        public void Stop()
        {
            _thread.ThreadEvent -= OnProcess;
        }

        protected abstract void OnProcess();

    }

    public class A429SendModule : A429AbstractTxAndRxModule 
    {
        public A429SendModule(Device429 device429) : base(device429)
        {
        }

        protected override void OnProcess()
        {
            while (true)
            {
                foreach (var ch in Device429.SendComponents)
                {
                    ISend sendItem = ch as ISend;
                    if (sendItem != null)
                    {
                        sendItem.Send();
                    }
                }
                Thread.Sleep(500);
            }
        }
    }
}
