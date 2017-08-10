using System;
using BinHong.FlightViewerCore;
using BinHong.FlightViewerVM;

namespace BinHong.FlightViewerUI
{
    public partial class NewDevice : FixedSingleForm
    {
        private readonly DeviceUiInfo _deviceUiInfo = new DeviceUiInfo();

        private readonly NewDeviceUi _newDeviceUi;

        public NewDevice(NewDeviceUi newDeviceUi)
        {
            InitializeComponent();
            
            //设置响应
            this.btn_Ok.Click += OnOk;
            this.btn_Cancel.Click += OnCancel;

            //设置界面和属性的绑定
            this.textBox_Name.DataBindings.Add("Text", _deviceUiInfo, "Name");
            this.textBox_BoardNo.DataBindings.Add("Text", _deviceUiInfo, "BoardNo");
            this.comboBox_BoardType.DataBindings.Add("Text", _deviceUiInfo, "BoardType");
            this.comboBox_ChannelType.DataBindings.Add("Text", _deviceUiInfo, "ChannelType");
            this.textBox_ChannelCount.DataBindings.Add("Text", _deviceUiInfo, "ChannelCount");
            comboBox_BoardType.DataSource = Enum.GetNames(typeof (BoardType));
            comboBox_ChannelType.DataSource = Enum.GetNames(typeof (ChannelType));

            _deviceUiInfo.Name = "Board0";

            _newDeviceUi = newDeviceUi;
        }

        private void OnCancel(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnOk(object sender, EventArgs e)
        {
            _newDeviceUi.AddDevice(_deviceUiInfo);
            this.Close();
        }
    }
}
