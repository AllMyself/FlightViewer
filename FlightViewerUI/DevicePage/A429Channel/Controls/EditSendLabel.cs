using System;
using System.Collections.Generic;
using System.Drawing;
using BinHong.FlightViewerCore;
using BinHong.FlightViewerVM;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;

namespace BinHong.FlightViewerUI
{
    public partial class EditSendLabel : FixedSingleForm
    {
        private readonly ChannelSendControlVm _chVm;

        private readonly A429SendControl _a429SendControl;
        public EditSendLabel(A429SendControl a429SendControl)
        {
            this._chVm = a429SendControl.ChVm;
            this._a429SendControl = a429SendControl;

            InitializeComponent();
            
            //设置界面元素
            this.flgView.DataSource = _chVm.LabelList;
            flgView.Cols["ActualValue"].Visible = false;
            flgView.Cols["Name"].Visible = false;

            flgView.Cols["IsSelected"].Caption = "生效";
            flgView.Cols["Interval"].Caption = "发送间隔";
            flgView.Cols["Label"].Caption = "标号";
            flgView.Cols["SDI"].Caption = "SDI";
            flgView.Cols["Data"].Caption = "数据";
            flgView.Cols["SymbolState"].Caption = "符号状态";
            flgView.Cols["Parity"].Caption = "奇偶校验";
            flgView.Cols["isAutoIncrement"].Caption = "是否自增";

            flgView.Cols["IsSelected"].Width = 50;
            flgView.Cols["Interval"].Width = 80;
            flgView.Cols["Label"].Width = 80;
            flgView.Cols["SDI"].Width = 80;
            flgView.Cols["Data"].Width = this.flgView.Width-560;
            flgView.Cols["SymbolState"].Width = 50;
            flgView.Cols["Parity"].Width = 50;
            flgView.Cols["isAutoIncrement"].Width = 50;

            flgView.Cols["IsSelected"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["Interval"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["Label"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["SDI"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["Data"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["SymbolState"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["Parity"].TextAlign = TextAlignEnum.CenterCenter;
            flgView.Cols["isAutoIncrement"].TextAlign = TextAlignEnum.CenterCenter;

            flgView.Styles.Normal.TextAlign = TextAlignEnum.CenterCenter;
            flgView.Styles.Editor.TextAlign = TextAlignEnum.CenterCenter;
            flgView.Styles.EmptyArea.BackColor = Color.White;
            flgView.Styles.EmptyArea.Border.Width = 0;
            flgView.ExtendLastCol = true;

            //设置按钮响应
            btnOk.Click += OnOk;
            cmdAddItem.Click += OnAddItem;
            cmdDeleteItem.Click += OnDeleteItem;
        }

        private void OnDeleteItem(object sender, ClickEventArgs e)
        {
            RowCollection delDevices = this.flgView.Rows.Selected;
            foreach (Row delDevice in delDevices)
            {
                SendLabelUi info = delDevice.DataSource as SendLabelUi;
                _chVm.LabelList.Remove(info);
            }
        }

        private void OnAddItem(object sender, ClickEventArgs e)
        {
            _chVm.LabelList.Add(new SendLabelUi("Label" + _chVm.LabelCount++));
        }

        private void OnOk(object sender, EventArgs e)
        {
            _chVm.UpdataLabel(true);
            _a429SendControl.UpdateTreeView();
            this.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.BackColor = VmColors.LightBlue;

            _chVm.UpdataLabel(false);
            
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
             _chVm.LabelList.Clear();
        }
    }
}
