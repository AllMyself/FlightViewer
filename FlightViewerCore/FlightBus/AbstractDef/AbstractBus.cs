namespace BinHong.FlightViewerCore
{
    /// <summary>
    /// Bus
    /// 1,没有id接口。因为用Name就可以唯一标识了
    /// 2,没有ICreateId接口，因为其child是Device使用Name就可以唯一标识了
    /// </summary>
    public abstract class AbstractBus : AbstractPathComponent, ILogin, IBoardType
    {
        public BoardType BoardType { get; set; }

        public IDriverOperate DriverOperate { get; protected set; }

        public abstract void Login(IDeviceInfo info);

        public abstract void Logout(string name);
    }
}
