using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BinHong.FlightViewerCore;
using BinHong.FlightViewerUI.Properties;
using UiControls;

namespace BinHong.FlightViewerUI
{
    public partial class AboutSoftware : FixedSingleForm
    {
        private Point _dragStartPoint;

        public AboutSoftware()
        {
            InitializeComponent();

            //设置版本号
            lbVersion.Text = "版本 "+App.Instance.ConfigManager.VersionNo;

            this.linkLabel_Website.Click += (o, e) =>
            {
                LinkLabel linkLabel = this.linkLabel_Website;

                string webSite = linkLabel.Text;
                int startedIndex = linkLabel.LinkArea.Start;
                int len = linkLabel.LinkArea.Length - startedIndex;
                webSite = webSite.Substring(startedIndex, len);
                try
                {
                    Process.Start("explorer.exe", webSite);
                }
                catch (Exception)
                {
                }            
            };

            //获取注册的基本信息
            KeyAuthorization activator = new KeyAuthorization(App.Instance.ConfigDirectory);
            string deadline;
            string registerKey;
            activator.GetRegisterInfo(out deadline, out registerKey);
            this.lb_DeadLine.Text = deadline;
            this.tb_Register.Text = registerKey;

            this.MouseDown += (o, e) =>
            {
                _dragStartPoint = new Point(e.X, e.Y);
            };
            this.MouseMove += (o, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.Left += (e.X - _dragStartPoint.X);
                    this.Top += (e.Y - _dragStartPoint.Y);
                }
            };

            this.btn_Register.Click += (o, e) =>
            {
                string key = tb_Register.Text;
                //检测授权
                if (!activator.Activate(key))
                {
                    MessageBox.Show("激活失败。授权码不正确或过期！", "提示");
                    return;
                }
                else
                {
                    MessageBox.Show("授权成功！", "提示");
                    string deadline1;
                    string registerKey1;
                    activator.GetRegisterInfo(out deadline1, out registerKey1);
                    this.lb_DeadLine.Text = deadline1;
                    this.tb_Register.Text = registerKey1;
                }
            };
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {

        }
    }
}
