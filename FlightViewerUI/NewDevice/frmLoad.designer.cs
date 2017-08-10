namespace BinHong.FlightViewerUI
{
    partial class FrmLoad
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLoad));
            this.flgView = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1ToolBar1 = new C1.Win.C1Command.C1ToolBar();
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            this.cmdLoadConfig = new C1.Win.C1Command.C1Command();
            this.cmdSaveConfig = new C1.Win.C1Command.C1Command();
            this.cmdAddLoginItem = new C1.Win.C1Command.C1Command();
            this.cmdDeleteLoginItem = new C1.Win.C1Command.C1Command();
            this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink3 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink4 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandDock1 = new C1.Win.C1Command.C1CommandDock();
            this.btnLogin = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.flgView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).BeginInit();
            this.c1CommandDock1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnLogin)).BeginInit();
            this.SuspendLayout();
            // 
            // flgView
            // 
            this.flgView.AllowDelete = true;
            this.flgView.ColumnInfo = "1,1,0,0,0,100,Columns:0{Visible:False;}\t";
            this.flgView.Location = new System.Drawing.Point(0, 28);
            this.flgView.Name = "flgView";
            this.flgView.Rows.Count = 1;
            this.flgView.Rows.DefaultSize = 20;
            this.flgView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.flgView.ScrollOptions = C1.Win.C1FlexGrid.ScrollFlags.AlwaysVisible;
            this.flgView.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.flgView.Size = new System.Drawing.Size(634, 283);
            this.flgView.TabIndex = 4;
            // 
            // c1ToolBar1
            // 
            this.c1ToolBar1.AccessibleName = "Tool Bar";
            this.c1ToolBar1.AutoSize = false;
            this.c1ToolBar1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.c1ToolBar1.CommandHolder = this.c1CommandHolder1;
            this.c1ToolBar1.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink2,
            this.c1CommandLink1,
            this.c1CommandLink3,
            this.c1CommandLink4});
            this.c1ToolBar1.Location = new System.Drawing.Point(0, 0);
            this.c1ToolBar1.Movable = false;
            this.c1ToolBar1.Name = "c1ToolBar1";
            this.c1ToolBar1.Size = new System.Drawing.Size(634, 32);
            this.c1ToolBar1.Text = "c1ToolBar1";
            this.c1ToolBar1.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
            this.c1ToolBar1.VisualStyleBase = C1.Win.C1Command.VisualStyle.System;
            // 
            // c1CommandHolder1
            // 
            this.c1CommandHolder1.Commands.Add(this.cmdLoadConfig);
            this.c1CommandHolder1.Commands.Add(this.cmdSaveConfig);
            this.c1CommandHolder1.Commands.Add(this.cmdAddLoginItem);
            this.c1CommandHolder1.Commands.Add(this.cmdDeleteLoginItem);
            this.c1CommandHolder1.Owner = this;
            // 
            // cmdLoadConfig
            // 
            this.cmdLoadConfig.Image = global::BinHong.FlightViewerUI.Properties.Resources.Load;
            this.cmdLoadConfig.Name = "cmdLoadConfig";
            this.cmdLoadConfig.ShortcutText = "";
            this.cmdLoadConfig.Text = "载入配置";
            // 
            // cmdSaveConfig
            // 
            this.cmdSaveConfig.Image = global::BinHong.FlightViewerUI.Properties.Resources.Save;
            this.cmdSaveConfig.Name = "cmdSaveConfig";
            this.cmdSaveConfig.ShortcutText = "";
            this.cmdSaveConfig.Text = "保存配置";
            // 
            // cmdAddLoginItem
            // 
            this.cmdAddLoginItem.Image = global::BinHong.FlightViewerUI.Properties.Resources.DeviceAdd;
            this.cmdAddLoginItem.Name = "cmdAddLoginItem";
            this.cmdAddLoginItem.ShortcutText = "";
            this.cmdAddLoginItem.Text = "添加登录项";
            // 
            // cmdDeleteLoginItem
            // 
            this.cmdDeleteLoginItem.Image = global::BinHong.FlightViewerUI.Properties.Resources.DeviceDelete;
            this.cmdDeleteLoginItem.Name = "cmdDeleteLoginItem";
            this.cmdDeleteLoginItem.ShortcutText = "";
            this.cmdDeleteLoginItem.Text = "删除登录项";
            // 
            // c1CommandLink2
            // 
            this.c1CommandLink2.Command = this.cmdLoadConfig;
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.Command = this.cmdSaveConfig;
            this.c1CommandLink1.SortOrder = 1;
            // 
            // c1CommandLink3
            // 
            this.c1CommandLink3.Command = this.cmdAddLoginItem;
            this.c1CommandLink3.Delimiter = true;
            this.c1CommandLink3.SortOrder = 2;
            // 
            // c1CommandLink4
            // 
            this.c1CommandLink4.Command = this.cmdDeleteLoginItem;
            this.c1CommandLink4.SortOrder = 3;
            // 
            // c1CommandDock1
            // 
            this.c1CommandDock1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.c1CommandDock1.Controls.Add(this.c1ToolBar1);
            this.c1CommandDock1.Id = 1;
            this.c1CommandDock1.Location = new System.Drawing.Point(0, 0);
            this.c1CommandDock1.Name = "c1CommandDock1";
            this.c1CommandDock1.Size = new System.Drawing.Size(634, 32);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(529, 317);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(93, 23);
            this.btnLogin.TabIndex = 12;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            // 
            // FrmLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(634, 367);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.c1CommandDock1);
            this.Controls.Add(this.flgView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(650, 405);
            this.Name = "FrmLoad";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设备登录";
            ((System.ComponentModel.ISupportInitialize)(this.flgView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).EndInit();
            this.c1CommandDock1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnLogin)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid flgView;
        private C1.Win.C1Command.C1ToolBar c1ToolBar1;
        private C1.Win.C1Command.C1CommandLink c1CommandLink2;
        private C1.Win.C1Command.C1CommandLink c1CommandLink1;
        private C1.Win.C1Command.C1CommandDock c1CommandDock1;
        private C1.Win.C1Command.C1CommandLink c1CommandLink3;
        private C1.Win.C1Command.C1CommandLink c1CommandLink4;
        private C1.Win.C1Input.C1Button btnLogin;
        private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
        private C1.Win.C1Command.C1Command cmdLoadConfig;
        private C1.Win.C1Command.C1Command cmdSaveConfig;
        private C1.Win.C1Command.C1Command cmdAddLoginItem;
        private C1.Win.C1Command.C1Command cmdDeleteLoginItem;
    }
}