using System;
using System.Drawing;
using BinHong.FlightViewerCore;
using BinHong.FlightViewerVM;
using C1.Win.C1FlexGrid;

namespace BinHong.FlightViewerUI
{
    public partial class A429SendSetting : FixedSingleForm
    {
               private Device429 _device429;

        readonly ChannelSendSettingVm _chVm = new ChannelSendSettingVm();

        public A429SendSetting()
        {
            InitializeComponent();

            //设置界面元素
            this.flgView.DataSource = _chVm.Channels;
            flgView.Cols["ChannelType"].Visible = false;
            flgView.Cols["ChannelID"].AllowEditing = false;

            flgView.Cols["Name"].Caption = "名字";
            flgView.Cols["ChannelID"].Caption = "ChannelID";
            flgView.Cols["Enabled"].Caption = "是否矢能";
            flgView.Cols["Parity"].Caption = "是否奇偶校验";
            flgView.Cols["BaudRate"].Caption = "波特率(bit)";
            flgView.Cols["LoopEnable"].Caption = "是否环回";
            flgView.Cols["BaudRate"].ComboList = "12500|50000|100000";
            flgView.Cols["BaudRate"].AllowEditing = true;
            flgView.Cols["Parity"].ComboList = "偶校验|奇校验|不校验";
            flgView.Cols["Parity"].AllowEditing = true;

            flgView.Cols["Name"].Width = 150;
            flgView.Cols["ChannelID"].Width = 100;
            flgView.Cols["Enabled"].Width = 100;
            flgView.Cols["Parity"].Width = 80;
            flgView.Cols["BaudRate"].Width = 200;
            flgView.Cols["LoopEnable"].Width = 100;

            flgView.Cols["Name"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["ChannelID"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["Enabled"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["Parity"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["BaudRate"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["LoopEnable"].TextAlign = TextAlignEnum.CenterCenter;

            flgView.Styles.Normal.TextAlign = TextAlignEnum.CenterCenter;
            flgView.Styles.Editor.TextAlign = TextAlignEnum.CenterCenter;
            flgView.Styles.EmptyArea.BackColor = Color.White;
            flgView.Styles.EmptyArea.Border.Width = 0;
            flgView.ExtendLastCol = true;

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //设置对应的设备
            string[] pathParts = this.Name.Split('_');
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

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <returns></returns>
        private bool UpdateData()
        {
            _chVm.UpdataDevice(_device429);
            return true;
        }

        private void A429SendSetting_Load(object sender, EventArgs e)
        {

        }
    }
}
