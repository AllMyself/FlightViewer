using System;
using System.Windows.Forms;
using BinHong.FlightViewerUI.Properties;
using BinHong.FlightViewerVM;
using UiControls;
using StatusStrip = UiControls.StatusStrip;

namespace BinHong.FlightViewerUI
{
    public class FixedSingleForm : EscForm
    {
        protected StatusStrip StatusStrip;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //设置窗口属性
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = VmColors.LightBlue;
            this.Icon = Resources.PXILOGO;
            //添加界面元素
            StatusStrip = new StatusStrip();
            StatusStrip.Dock = DockStyle.Bottom;
            this.Controls.Add(StatusStrip);
        }
    }
}
