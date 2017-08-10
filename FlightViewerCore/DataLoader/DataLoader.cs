using System;

namespace BinHong.FlightViewerCore
{
    public class DataLoader : IBuildModule, IInitialize, IDisposable,IName
    {
        public void BuildModule()
        {

        }

        public void Initialize()
        {

        }

        public void UnInitialize()
        {

        }

        public void Dispose()
        {

        }

        public string Name { get; private set; }
    }
}
