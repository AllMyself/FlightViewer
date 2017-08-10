namespace BinHong.FlightViewerCore
{
    public interface IParameter
    {
        void UpdateParameter(IParameter parameter);

        void ResetParamter();
    }

    public interface IGetParameter
    {
        IParameter Parameter { get; }
    }
}
