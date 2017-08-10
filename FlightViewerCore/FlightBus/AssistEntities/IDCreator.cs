namespace BinHong.FlightViewerCore
{
    public class IDCreator:ICreateID
    {
        private int _id=0;

        public int GetOneID()
        {
            return _id++;
        }
    }
}
