using System;
using System.Drawing;
using BinHong.FlightViewerCore;
using BinHong.FlightViewerVM;
using C1.Win.C1FlexGrid;

namespace BinHong.FlightViewerUI
{
    public partial class A429ReceiveSetting : FixedSingleForm
    {
        private Device429 _device429;

        readonly ChannelReceiveSettingVm _chVm=new ChannelReceiveSettingVm();

        public A429ReceiveSetting()
        {
            InitializeComponent();

            //设置界面元素
            this.flgView.DataSource = _chVm.Channels;
            flgView.Cols["ChannelType"].Visible = false;
            flgView.Cols["ChannelID"].AllowEditing = false;

            flgView.Cols["Name"].Caption = "名字";
            flgView.Cols["ChannelID"].Caption = "ChannelID";
            flgView.Cols["Enabled"].Caption = "是否使能";
            flgView.Cols["Parity"].Caption = "奇偶校验";
            flgView.Cols["BaudRate"].Caption = "波特率(bps)";
            flgView.Cols["isFilter"].Caption = "是否过滤";
            flgView.Cols["receiveType"].Caption = "接收类型";
            flgView.Cols["deepCount"].Caption = "深度阀值";
            flgView.Cols["timeCount"].Caption = "时间阀值";
            flgView.Cols["receiveType"].ComboList = "采样|队列";
            flgView.Cols["Parity"].ComboList = "奇校验|偶校验|不校验";

            flgView.Cols["Name"].Width = 150;
            flgView.Cols["ChannelID"].Width = 100;
            flgView.Cols["Enabled"].Width = 100;
            flgView.Cols["Parity"].Width = 80;
            flgView.Cols["BaudRate"].Width = 200;
            flgView.Cols["isFilter"].Width = 50;
            flgView.Cols["receiveType"].Width = 70;
            flgView.Cols["deepCount"].Width = 80;
            flgView.Cols["timeCount"].Width = 80;

            flgView.Cols["Name"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["ChannelID"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["Enabled"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["Parity"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["BaudRate"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["isFilter"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["receiveType"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["deepCount"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["timeCount"].TextAlign = TextAlignEnum.CenterCenter;

            flgView.Styles.Normal.TextAlign = TextAlignEnum.CenterCenter;
            flgView.Styles.Editor.TextAlign = TextAlignEnum.CenterCenter;
            flgView.Styles.EmptyArea.BackColor = Color.White;
            flgView.Styles.EmptyArea.Border.Width = 0;
            flgView.ExtendLastCol = true;

            //flgView.Cols["Parity"].ComboList = "1111111";
            
            //设置响应方法
            button_Ok.Click += (o, e) =>
            {
                bool ret = UpdateData();
                if (ret == false)
                {
                    return;
                }
                this.Close();
            };
            button_Cancel.Click += (o, e) => this.Close();
        }

        private bool UpdateData()
        {
            //this.StatusStrip.ShowErrorInfo(string.Format("数据格式不正确"));
            _chVm.UpdataDevice(_device429);
            return true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //设置对应的设备
            string[] pathParts = this.Name.Split('_');
            //根据路径去解析当前device的信息
            _device429 = App.Instance.FlightBusManager.Bus429.GetSpecificItem(pathParts[pathParts.Length - 1]);
            //初始化数据
            InitializeData();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitializeData()
        {
            _chVm.Initialize(_device429);
        }
    }
}
