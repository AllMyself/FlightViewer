using BinHong.FlightViewerCore;
using BinHong.FlightViewerVM.Annotations;
using BinHong.Utilities;
using System;
using System.ComponentModel;
using UiControls;

namespace BinHong.FlightViewerVM
{
    public delegate void ChangedHandler(object sender, object v);
    //用于数据处理
    public class ReceiveLabelUi : INotifyPropertyChanged
    {
        public ReceiveLabelUi()
        { }
        public ReceiveLabelUi(DateTime date)
        {
            this.time = date.ToString("yyyy-MM-dd HH24:mm:ss ff");
            this._rxpA429 = new RxpA429();
        }

        public string time { get; set; }
        public AbstractChannel429 chanel { get; set; }
        public RxpA429 _rxpA429;
        public RxpA429 rxpA429
        {
            get
            {
                return _rxpA429;
            }
            set
            {
                _rxpA429 = value;
                OnPropertyChanged("_rxpA429");//进行监控数据是否变化
            }
        }
        //public bool isFilter { get; set; }
        //public string receiveType { get; set; }
        //public string deepCount { get; set; }
        //public string timeCount { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class ChannelReceiveControlVm : IStartStop, IIsSelected//这里这个select知识用来当做标识
    {
        public readonly StatusStripMsgShow MsgShow = new StatusStripMsgShow();

        private readonly Device429 _device429;//对应的设备

        private Channe429Receive _curSelectedChannel;//可以查看当前chanel的label

        public Channe429Receive SelectChannelClick;//点击选中的channel



        public BindingList<ReceiveLabelUi> Labellist = new BindingList<ReceiveLabelUi>();

        //这里面永远只有一条当前的label
        public ReceiveLabelUi Label;//应为我希望在里面永远只保持一条记录，所以，这边我没有readonly

        ReceiveLabelUi receiveLabelUi;

        public bool IsSelected { get; set; }//这个只是作为是否开始接受的标识而已，没有任何其他的功能

        public bool IsFileSaveAllow { get; set; }//是否允许文件保存

        public ChannelReceiveControlVm(Device429 device429)
        {
            this._device429 = device429;
        }
        public void Start()
        {
            MsgShow.ShowWarning("正在接收消息。。。");
            IsSelected = true;
            foreach (var item in _device429.ReceiveComponents)
            {
                Channe429Receive chanelReceive = (Channe429Receive)item;
                chanelReceive.IsFileSaveAllow = IsFileSaveAllow;//是否进行文件保存
                chanelReceive.isSend = true;//开始接收,为什么？？因为在login的时候在后台多开了一个线程在不停的调用接收和发送，那么只需要将它的标识设置为true就ok了
                receiveLabelUi = new ReceiveLabelUi(DateTime.Now);
                receiveLabelUi.chanel = chanelReceive;
                Labellist.Add(receiveLabelUi);//将新的记录保存到ui中
                if (_curSelectedChannel == chanelReceive)//如果选中的chanel与这个chanel一样的话，执行下面的操作
                {
                    Label = receiveLabelUi;
                }
            }

        }

        public void Stop()
        {
            foreach (var item in _device429.ReceiveComponents)
            {
                Channe429Receive chanelReceive = (Channe429Receive)item;
                chanelReceive.isSend = false;//将标识设置为false，就可以停用
            }
            MsgShow.ShowWarning("已经停止接收消息！");
        }
        //这里我的想法是直接将count置为0，并且将存在本地的文件改个名字，
        public void ClearData()
        {
            Labellist = null;//直接将数据源的数据置为null
            Label = null;//我也不知道，顺便将它也置为null
        }
        //获取当前选中的chanel
        public void Select(string path)
        {
            _curSelectedChannel = null;

            string[] pathParts = path.Split('_');
            if (pathParts.Length > 3)
            {
                string chName = pathParts[3];
                //_curSelectedChannel = (Channe429Receive)_device429.GetSpecificItem(chName);//这个方法是获取子组件
                SelectChannelClick = (Channe429Receive)_device429.GetSpecificItem(chName);
            }
        }
        public ReceiveLabelUi ClickCurrentData()//点击选中的chanel数据
        {
            if (SelectChannelClick != null && _curSelectedChannel == SelectChannelClick && _curSelectedChannel != null)//选中的不为空并且选中的与当前的chanel一致，那么就将数据返回
            {
                return receiveLabelUi;
            }
            else
            {
                return null;
            }
        }
        public ReceiveLabelUi CurrentData()//不分是否选中，全部的chanel
        {
            if (_curSelectedChannel != null)
            {
                return receiveLabelUi;
            }
            else
            {
                return null;
            }
        }
        public BindingList<ReceiveLabelUi> LabelList()//获取全部的label，主要是用于文件分析
        {
            return Labellist;
        }

        #region common
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
