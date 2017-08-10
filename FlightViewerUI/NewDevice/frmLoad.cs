//********************************************************
//建立日期:2015.11.1
//作者:litao
//內容说明:　FrmLoad登录界面。

//修改日期：
//作者:
//內容说明:
//********************************************************

using System;
using System.Drawing;
using System.Windows.Forms;
using BinHong.FlightViewerCore;
using BinHong.FlightViewerVM;
using C1.Win.C1FlexGrid;
using UiControls;

namespace BinHong.FlightViewerUI
{
    public partial class FrmLoad : FixedSingleForm
    {
        private readonly NewDeviceUi _newDeviceUi=new NewDeviceUi();

        public FrmLoad()
        {
            InitializeComponent();

            //设置界面元素
            this.flgView.DataSource = _newDeviceUi.DeviceUiInfos;
            flgView.Cols["ChannelType"].Visible = false;
            flgView.Cols["ChannelCount"].Visible = false;

            flgView.Cols["Name"].Caption = "名字";
            flgView.Cols["BoardNo"].Caption = "板卡号";
            flgView.Cols["BoardType"].Caption = "板卡类型";
            flgView.Cols["ChannelType"].Caption = "通道类型";
            flgView.Cols["ChannelCount"].Caption = "通道个数";
            flgView.Cols["IsSelected"].Caption = "是否选中";

            flgView.Cols["Name"].Width = 150;
            flgView.Cols["BoardNo"].Width = 100;
            flgView.Cols["BoardType"].Width = 100;
            flgView.Cols["ChannelType"].Width = 100;
            flgView.Cols["ChannelCount"].Width = 80;
            flgView.Cols["IsSelected"].Width = 65;

            flgView.Cols["Name"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["BoardNo"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["BoardType"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["ChannelType"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["ChannelCount"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["IsSelected"].TextAlign = TextAlignEnum.CenterCenter;

            flgView.Styles.Normal.TextAlign = TextAlignEnum.CenterCenter;
            flgView.Styles.Editor.TextAlign = TextAlignEnum.CenterCenter;
            flgView.Styles.EmptyArea.BackColor = Color.White;
            flgView.Styles.EmptyArea.Border.Width = 0;
            flgView.ExtendLastCol = true;

            //事件绑定
            this.btnLogin.Click += OnLogin;

            this.cmdDeleteLoginItem.Click += OnDel; //删除配置
            this.cmdLoadConfig.Click += OnLoadDeviceConfig; //装载设备配置
            this.cmdSaveConfig.Click += OnSaveDeviceConfig; //保存设备配置
            this.cmdAddLoginItem.Click += OnAdd; //添加新行

            //选择条目变化时，设置删除键的可用性
            this.flgView.AfterSelChange += SetDelButtonEnableState;

            //提示信息
            _newDeviceUi.MsgShow.Initialize(
                () => this.StatusStrip.ClearMsg(),
                info => this.StatusStrip.ShowErrorInfo(info),
                info => this.StatusStrip.ShowWarningInfo(info),
                info => this.StatusStrip.ShowInfo(info));
            this.Closed +=(o,e)=> _newDeviceUi.MsgShow.Uninitialize();
        }

        /// <summary>
        /// 界面加载事件
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

#if SaveLoginInfMacro
            //_newDeviceUi.LoadConfig(App.Instance.ConfigDirectory+"login.xml");
#endif
            //自动检测当前电脑的板卡
            _newDeviceUi.CheckAllDevice();
        }

        /// <summary>
        /// 界面关闭事件
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
#if SaveLoginInfMacro
            _newDeviceUi.SaveConfig(App.Instance.ConfigDirectory + "login.xml");
#endif
        }

        private void OnLogin(object sender, EventArgs e)
        {
            //检测授权
            var activator = new KeyAuthorization(App.Instance.ConfigDirectory);
            if (!activator.Check())
            {
                MessageBox.Show(@"软件过期，请重新授权！", @"提示");
                return;
            }
            _newDeviceUi.Login();

#if Test
            Device429 device =
                 ((Bus429)App.Instance.FlightBusManager.GetBus(BoardType.A429.ToString())).GetSpecificItem(0);
            if (device != null)
            {
                Channe429Receive ch = (Channe429Receive)device.GetSpecificItem(0);
                Channel429DriverRx driver = (Channel429DriverRx) ch.ChannelDriver;
                MibDataA429 mibDataA429;
                driver.ChannelMibGetRx(out mibDataA429);
                Console.WriteLine("mibDataA429:{0},{1}", mibDataA429.cnt, mibDataA429.err_cnt);
            }
#endif
            this.Close();
        }

        /// <summary>
        ///设置删除键的可用性
        /// </summary>
        private void SetDelButtonEnableState(object sender, EventArgs e)
        {
            RowCollection delDevices = this.flgView.Rows.Selected;
            if (delDevices.Count == 0)
            {
                this.cmdDeleteLoginItem.Enabled = false;
            }
            else
            {
                this.cmdDeleteLoginItem.Enabled = true;
            }
        }

        /// <summary>
        /// 添加一个空设备
        /// </summary>
        public void OnAdd(object sender, EventArgs e)
        {
            NewDevice form = new NewDevice(_newDeviceUi);
            form.Text = "新建设备";
            form.ShowSingleAtCenterParent(this);
        }

        /// <summary>
        /// 删除选中的设备
        /// </summary>
        private void OnDel(object sender, EventArgs e)
        {
            RowCollection delDevices = this.flgView.Rows.Selected;
            foreach (Row delDevice in delDevices)
            {
                DeviceUiInfo info = delDevice.DataSource as DeviceUiInfo;
                _newDeviceUi.DelDevice(info);
            }
            //删除设备后，要检测一下Rows.Selected。
            SetDelButtonEnableState(null,null);
        }

        /// <summary>
        /// 装载设备配置
        /// </summary>
        private void OnLoadDeviceConfig(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog=new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = @"xml files(*.xml)|*.xml";
            DialogResult result=openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _newDeviceUi.LoadConfig(openFileDialog.FileName);
            }
        }

        /// <summary>
        /// 保存设备配置
        /// </summary>
        private void OnSaveDeviceConfig(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog=new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Filter = @"xml files(*.xml)|*.xml";
            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _newDeviceUi.SaveConfig(saveFileDialog.FileName);
            }
        }
    }
}
