using System;
using System.Windows.Forms;
using BinHong.FlightViewerVM;
using UiControls;
using BinHong.Utilities;

namespace BinHong.FlightViewerUI
{
    public partial class DevicePage : UserControl
    {

        public DevicePage()
        {
            InitializeComponent();
            //接收设置
            btn_ReceiveSetting.Click += (o, e) =>
            {
                var formSetting = new A429ReceiveSetting();
                formSetting.Name = this.Name;
                formSetting.Text = this.Name.Replace(TreeLocalHost.PathString + "_", "") + "_ReceiveSetting";
                formSetting.ShowSingleAtCenterParent(this.FindForm());
            };
            //发送设置
            btn_SendSetting.Click += (o, e) =>
            {
                var formSetting = new A429SendSetting();
                formSetting.Name = this.Name;
                formSetting.Text = this.Name.Replace(TreeLocalHost.PathString + "_", "") + "_SendSetting";
                formSetting.ShowSingleAtCenterParent(this.FindForm());
            };
            btn_PlayBackSetting.Click += (o, e) =>
            {
                var formSetting = new A429PlayBackSetting();
                formSetting.Name = this.Name;
                formSetting.Text = this.Name.Replace(TreeLocalHost.PathString + "_", "") + "_PlayBackSetting";
                formSetting.ShowSingleAtCenterParent(this.FindForm());
            };
            //发送控制
            btn_SendControl.Click += (o, e) =>
            {
                var formSetting = new A429SendControl();
                formSetting.Name = this.Name;
                formSetting.Text = this.Name.Replace(TreeLocalHost.PathString + "_", "") + "_SendControl";
                formSetting.ShowSingleAtCenterParent(this.FindForm());
            };
            btn_ReceiveControl.Click += (o, e) =>
            {
                var formSetting = new A429ReceiveControl();
                formSetting.Name = this.Name;
                formSetting.Text = this.Name.Replace(TreeLocalHost.PathString + "_", "") + "_ReceiveControl";
                formSetting.ShowSingleAtCenterParent(this.FindForm());
            };
            button1.Click += (o, e) =>
            {
                var formSetting = new A429FactorySetting();
                formSetting.Name = this.Name;
                formSetting.Text = this.Name.Replace(TreeLocalHost.PathString + "_", "") + "_FacotorySetting";
                formSetting.ShowSingleAtCenterParent(this.FindForm());
            };
            button2.Click += (o, e) =>//过滤器设置
                {
                    var formSetting = new A429FilterControl();
                    formSetting.Name = this.Name;
                    formSetting.Text = this.Name.Replace(TreeLocalHost.PathString + "_", "") + "_FacotorySetting";
                    formSetting.ShowSingleAtCenterParent(this.FindForm());
                };
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.BackColor = VmColors.LightBlue;
            this.Dock = DockStyle.Fill;
        }
    }
}
