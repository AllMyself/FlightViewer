namespace BinHong.FlightViewerCore
{
    /// <summary>
    /// 
    /// 1,没有id接口。因为用Name就可以唯一标识了
    /// 2，有ICreateID接口。其child使用Id来唯一标识。
    /// 3，没IInitialize接口。因为登录后没执行Initialize方法。在构造函数中通过IDeviceInfo初始化。
    /// </summary>
    public abstract class AbstractDevice : AbstractPathComponent, IDeviceInfo, IBuildModule, IParameter, IGetParameter
    {
        public void InitializeParameter(IDeviceInfo info)
        {
            this.BoardNo = info.BoardNo;
            this.BoardType = info.BoardType;
            this.ChannelType = info.ChannelType;
            this.ChannelCount = info.ChannelCount;
            this.DevID = info.DevID;
            
            Name = BoardNo.ToString();//此处以BoardNo来作为Name，标识唯一性。
        }

        public uint BoardNo { get;  set; }
        public BoardType BoardType { get;  set; }
        public ChannelType ChannelType { get;   set; }
        public uint DevID { get; set; }
        public uint ChannelCount { get;  set; }
        public string filterStr { get; set; }

        #region IParameter
        
        private readonly IParameter _parameter = new Parameter429();
        public void UpdateParameter(IParameter device)//更新参数
        {
            _parameter.UpdateParameter(device);
        }

        public void ResetParamter()//重置参数
        {
            _parameter.ResetParamter();
        }

        #endregion

        public IParameter Parameter { get { return _parameter; } }

        public abstract void BuildModule();
    }
}
