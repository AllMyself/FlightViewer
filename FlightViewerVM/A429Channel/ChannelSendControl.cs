using System.Collections.Generic;
using System.ComponentModel;
using BinHong.FlightViewerCore;
using BinHong.FlightViewerVM.Annotations;
using System.Threading;
using UiControls;
using BinHong.Utilities;

namespace BinHong.FlightViewerVM
{
    public class SendLabelUi : ILabel429Info, INotifyPropertyChanged, IIsSelected, IName
    {
        public SendLabelUi(SendLabel429 label429)
        {
            IsSelected = label429.IsSelected;
            Interval = label429.Interval;
            ActualValue = label429.ActualValue;
            Label = label429.Label;
            SDI = label429.SDI;
            Data = label429.Data;
            SymbolState = label429.SymbolState;
            Parity = label429.Parity;
            Name = label429.Name;
            isAutoIncrement = label429.isAutoIncrement;
            cycleInterval = label429.cycleInterval;
        }

        public SendLabelUi(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public int cycleInterval { get; set; }//软件周期间隔
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected { get; set; }
        public bool isAutoIncrement { get; set; }
        /// <summary>
        /// 发送间隔
        /// </summary>
        public int Interval { get; set; }

        public int ActualValue { get; set; }

        public int Label
        {
            get { return (byte)(ActualValue & 0xff); }
            set { ActualValue = ((ActualValue & (~0xff)) | (value & 0xff)); }
        }

        public int SDI
        {
            get { return (byte)((ActualValue >> 8) & 0x3); }
            set { ActualValue = ((ActualValue & (~(0x3 << 8))) | ((value & 0x3) << 8)); }
        }

        public int Data
        {
            get { return (byte)((ActualValue >> 10) & 0x7FFFF); }
            set { ActualValue = ((ActualValue & (~(0x7FFFF << 10))) | ((value & 0x7FFFF) << 10)); }
        }

        public int SymbolState
        {
            get { return (byte)((ActualValue >> 29) & 0x3); }
            set { ActualValue = ((ActualValue & (~(0x3 << 29))) | ((value & 0x3) << 29)); }
        }

        public int Parity
        {
            get { return (byte)((ActualValue >> 31) & 0x1); }
            set { ActualValue = ((ActualValue & (~(0x1 << 31))) | (value & 0x1) << 31); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    //控制前端的label行为
    public class ChannelSendControlVm : ISelectByName
    {

        public readonly StatusStripMsgShow MsgShow = new StatusStripMsgShow();
        private readonly Device429 _device429;

        public Channe429Send _curSelectedChannel { get; set; }

        private int _labelCount = 0;
        public int LabelCount
        {
            get
            {
                return _labelCount;
            }
            set
            {
                _labelCount = value;
                OnPropertyChanged("LabelCount");//进行监控数据是否变化
            }
        }

        public SendLabel429 CurSelectedLabel { get; private set; }

        public readonly BindingList<SendLabelUi> LabelList = new BindingList<SendLabelUi>();
        private readonly List<string> _labelNameList = new List<string>();

        public ChannelSendControlVm(Device429 device429)
        {
            this._device429 = device429;
        }
        //开始全部任务
        public void StartAll()
        {
            MsgShow.ShowWarning("数据发送中。。。");
            foreach (var item in _device429.SendComponents)
            {
                Channe429Send seletedItem = item as Channe429Send;//这里从iselect修改到了chanel429send
                if (seletedItem != null)
                {
                    seletedItem.isSend = true;
                }
            }
        }

        //停止所有的chanel，只需要将选中置为false就ok了，在chanel里面进行了选中判断
        public void StopAll()
        {
            MsgShow.ShowWarning("停止数据发送！");
            foreach (var item in _device429.SendComponents)
            {
                //IIsSelected seletedItem = item as IIsSelected;
                Channe429Send seletedItem = item as Channe429Send;
                if (seletedItem != null)
                {
                    seletedItem.isSend = false;
                }
            }
        }

        //获取当前选中的label
        public void Select(string path)
        {
            _curSelectedChannel = null;
            CurSelectedLabel = null;

            string[] pathParts = path.Split('_');
            if (pathParts.Length > 3)
            {
                string chName = pathParts[3];
                _curSelectedChannel = (Channe429Send)_device429.GetSpecificItem(chName);//这个方法是获取子组件
                if (pathParts.Length > 4)
                {
                    string lableName = pathParts[4];
                    CurSelectedLabel = (SendLabel429)_curSelectedChannel.GetSpecificItem(lableName);
                    //这里将数据初始化到周期发送里面
                    Channel429DriverTx driverTx = new Channel429DriverTx(_device429.DevID, _curSelectedChannel.ChannelID);
                    uint ret = driverTx.ChannelSendTx((uint)CurSelectedLabel.ActualValue, SendOptA429.BHT_L1_A429_OPT_PERIOD_SEND_UPDATE);
                    if (ret != 0)
                    {
                        RunningLog.Record(string.Format("return value is {0} when invoke ChannelSendTx Set Data", ret));
                    }
                    ret = driverTx.ChannelPeriodParamTx(CurSelectedLabel.Interval, ParamOptionA429.BHT_L1_PARAM_OPT_SET);
                    if (ret != 0)
                    {
                        RunningLog.Record(string.Format("return value is {0} when invoke ChannelPeriodParamTx", ret));
                    }
                }
            }
        }
        //删除选中chanel的所有label
        public void DelAllLabel()
        {
            if (_curSelectedChannel != null)
            {
                for (int index = 0; index < 100; index++)
                {
                    _curSelectedChannel.Delete(0);
                }
            }
        }
        //删除选中的label
        public void DelLabel(Label429 label)
        {
            if (_curSelectedChannel != null
                && label != null)
            {
                _curSelectedChannel.Delete(label);
            }
        }

        #region 界面数据和后台数据的同步

        public void UpdataLabel(bool isUiToChannel)
        {
            _labelNameList.Clear();
            for (int i = 0; i < LabelList.Count; i++)
            {
                _labelNameList.Add(LabelList[i].Name);
            }

            //从LabelList更新数据到_curSelectedChannel
            if (isUiToChannel)
            {
                UpdataUiToChannel();
                RemoveChannelRedundantItem();
            }
            else //从_curSelectedChannel更新数据到LabelList
            {
                UpdataChannelToUi();
                RemoveUiRedundantItem();
            }
        }

        private void UpdataChannelToUi()
        {
            for (int i = 0; true; i++)
            {
                SendLabel429 label = _curSelectedChannel.GetSpecificItem(i) as SendLabel429;
                if (label == null)
                {
                    break;
                }
                string labelName = label.Name;
                if (_labelNameList.Contains(labelName))
                {
                    int index = _labelNameList.IndexOf(labelName);
                    LabelList[index] = new SendLabelUi(label);
                    _labelNameList[index] = labelName;
                }
                else
                {
                    LabelList.Add(new SendLabelUi(label));
                    _labelNameList.Add(labelName);
                }
            }
        }

        private void RemoveUiRedundantItem()
        {
            for (int index = 0; index < LabelList.Count; index++)
            {
                string name = LabelList[index].Name;
                Label429 label = _curSelectedChannel.GetSpecificItem(name) as Label429;
                if (label == null)
                {
                    LabelList.RemoveAt(index);
                    _labelNameList.RemoveAt(index);
                }
            }
        }
        //前台数据跟新到后台
        private void UpdataUiToChannel()
        {
            for (int index = 0; index < LabelList.Count; index++)
            {
                string labelName = _labelNameList[index];
                SendLabel429 label = _curSelectedChannel.GetSpecificItem(labelName) as SendLabel429;
                if (label == null)
                {
                    label = new SendLabel429(labelName)
                    {
                        ActualValue = LabelList[index].ActualValue,
                        Label = LabelList[index].Label,
                        SDI = LabelList[index].SDI,
                        Data = LabelList[index].Data,
                        SymbolState = LabelList[index].SymbolState,
                        Parity = LabelList[index].Parity,
                        IsSelected = LabelList[index].IsSelected,
                        Interval = LabelList[index].Interval,
                        isAutoIncrement = LabelList[index].isAutoIncrement,
                        cycleInterval = LabelList[index].cycleInterval
                    };

                    _curSelectedChannel.Add(label);
                    //_curSelectedChannel.Initialize();
                }
                else
                {
                    label.ActualValue = LabelList[index].ActualValue;
                    label.Label = LabelList[index].Label;
                    label.SDI = LabelList[index].SDI;
                    label.Data = LabelList[index].Data;
                    label.SymbolState = LabelList[index].SymbolState;
                    label.Parity = LabelList[index].Parity;
                    label.IsSelected = LabelList[index].IsSelected;
                    label.Interval = LabelList[index].Interval;
                    label.isAutoIncrement = LabelList[index].isAutoIncrement;
                    label.cycleInterval = LabelList[index].cycleInterval;
                }

            }
        }

        private void RemoveChannelRedundantItem()
        {
            for (int index = 0; index > -1; index++)
            {
                Label429 label = _curSelectedChannel.GetSpecificItem(index) as Label429;
                if (label == null)
                {
                    break;
                }
                string name = label.Name;
                if (!_labelNameList.Contains(name))
                {
                    _curSelectedChannel.Remove(index);
                    index--;
                }
            }
        }

        #endregion

        #region common
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void ChannelStop()
        {
            if (_curSelectedChannel != null)
            {
                _curSelectedChannel.IsSelected = false;
            }
        }

        public void ChannelStart()
        {
            if (_curSelectedChannel != null)
            {
                _curSelectedChannel.IsSelected = true;
                _curSelectedChannel.isSend = true;//发送
            }
        }
    }
}
