using UiControls;
namespace BinHong.FlightViewerUI
{
    partial class MainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.c1CommandDock2 = new C1.Win.C1Command.C1CommandDock();
            this.tabOutputForm = new C1.Win.C1Command.C1DockingTab();
            this.tbpOutput = new C1.Win.C1Command.C1DockingTabPage();
            this.tbpLogs = new C1.Win.C1Command.C1DockingTabPage();
            this.tabEquipmentView = new C1.Win.C1Command.C1DockingTab();
            this.tbpDevices = new C1.Win.C1Command.C1DockingTabPage();
            this.c1CommandDock3 = new C1.Win.C1Command.C1CommandDock();
            this.tabDetailPanel = new C1.Win.C1Command.C1DockingTab();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock2)).BeginInit();
            this.c1CommandDock2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabOutputForm)).BeginInit();
            this.tabOutputForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabEquipmentView)).BeginInit();
            this.tabEquipmentView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock3)).BeginInit();
            this.c1CommandDock3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabDetailPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // c1CommandDock2
            // 
            this.c1CommandDock2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.c1CommandDock2.Controls.Add(this.tabOutputForm);
            this.c1CommandDock2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c1CommandDock2.Id = 5;
            this.c1CommandDock2.Location = new System.Drawing.Point(0, 491);
            this.c1CommandDock2.Name = "c1CommandDock2";
            this.c1CommandDock2.Size = new System.Drawing.Size(1065, 204);
            // 
            // tabOutputForm
            // 
            this.tabOutputForm.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabOutputForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabOutputForm.CanAutoHide = true;
            this.tabOutputForm.CanMoveTabs = true;
            this.tabOutputForm.Controls.Add(this.tbpOutput);
            this.tabOutputForm.Controls.Add(this.tbpLogs);
            this.tabOutputForm.Location = new System.Drawing.Point(0, 0);
            this.tabOutputForm.Name = "tabOutputForm";
            this.tabOutputForm.ShowCaption = true;
            this.tabOutputForm.Size = new System.Drawing.Size(1065, 204);
            this.tabOutputForm.TabIndex = 0;
            this.tabOutputForm.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            this.tabOutputForm.TabsSpacing = 5;
            this.tabOutputForm.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2010;
            this.tabOutputForm.VisualStyle = C1.Win.C1Command.VisualStyle.Office2010Blue;
            this.tabOutputForm.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            // 
            // tbpOutput
            // 
            this.tbpOutput.CaptionVisible = true;
            this.tbpOutput.Location = new System.Drawing.Point(1, 4);
            this.tbpOutput.Name = "tbpOutput";
            this.tbpOutput.Size = new System.Drawing.Size(1063, 175);
            this.tbpOutput.TabIndex = 1;
            this.tbpOutput.Text = "输出";
            this.tbpOutput.Click += new System.EventHandler(this.tbpOutput_Click);
            // 
            // tbpLogs
            // 
            this.tbpLogs.CaptionVisible = true;
            this.tbpLogs.Location = new System.Drawing.Point(1, 4);
            this.tbpLogs.Name = "tbpLogs";
            this.tbpLogs.Size = new System.Drawing.Size(1063, 175);
            this.tbpLogs.TabIndex = 0;
            this.tbpLogs.Text = "日志";
            // 
            // tabEquipmentView
            // 
            this.tabEquipmentView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabEquipmentView.CanAutoHide = true;
            this.tabEquipmentView.CanMoveTabs = true;
            this.tabEquipmentView.Controls.Add(this.tbpDevices);
            this.tabEquipmentView.Location = new System.Drawing.Point(0, 0);
            this.tabEquipmentView.MaximumSize = new System.Drawing.Size(300, 5524);
            this.tabEquipmentView.MinimumSize = new System.Drawing.Size(25, 25);
            this.tabEquipmentView.Name = "tabEquipmentView";
            this.tabEquipmentView.ShowCaption = true;
            this.tabEquipmentView.ShowSingleTab = false;
            this.tabEquipmentView.Size = new System.Drawing.Size(180, 491);
            this.tabEquipmentView.TabIndex = 0;
            this.tabEquipmentView.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            this.tabEquipmentView.TabsSpacing = 5;
            this.tabEquipmentView.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2010;
            this.tabEquipmentView.VisualStyle = C1.Win.C1Command.VisualStyle.Office2010Blue;
            this.tabEquipmentView.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            // 
            // tbpDevices
            // 
            this.tbpDevices.CaptionVisible = true;
            this.tbpDevices.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbpDevices.Location = new System.Drawing.Point(1, 1);
            this.tbpDevices.Name = "tbpDevices";
            this.tbpDevices.Size = new System.Drawing.Size(175, 489);
            this.tbpDevices.TabIndex = 0;
            // 
            // c1CommandDock3
            // 
            this.c1CommandDock3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.c1CommandDock3.Controls.Add(this.tabEquipmentView);
            this.c1CommandDock3.Dock = System.Windows.Forms.DockStyle.Left;
            this.c1CommandDock3.Id = 9;
            this.c1CommandDock3.Location = new System.Drawing.Point(0, 0);
            this.c1CommandDock3.Name = "c1CommandDock3";
            this.c1CommandDock3.Size = new System.Drawing.Size(180, 491);
            // 
            // tabDetailPanel
            // 
            this.tabDetailPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabDetailPanel.CanCloseTabs = true;
            this.tabDetailPanel.CanMoveTabs = true;
            this.tabDetailPanel.CloseBox = C1.Win.C1Command.CloseBoxPositionEnum.ActivePage;
            this.tabDetailPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDetailPanel.Location = new System.Drawing.Point(180, 0);
            this.tabDetailPanel.Name = "tabDetailPanel";
            this.tabDetailPanel.Size = new System.Drawing.Size(885, 491);
            this.tabDetailPanel.TabIndex = 14;
            this.tabDetailPanel.TabsSpacing = 5;
            this.tabDetailPanel.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2010;
            this.tabDetailPanel.VisualStyle = C1.Win.C1Command.VisualStyle.Office2010Blue;
            this.tabDetailPanel.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 695);
            this.Controls.Add(this.tabDetailPanel);
            this.Controls.Add(this.c1CommandDock3);
            this.Controls.Add(this.c1CommandDock2);
            this.MinimumSize = new System.Drawing.Size(1081, 726);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock2)).EndInit();
            this.c1CommandDock2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabOutputForm)).EndInit();
            this.tabOutputForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabEquipmentView)).EndInit();
            this.tabEquipmentView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock3)).EndInit();
            this.c1CommandDock3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabDetailPanel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
        private C1.Win.C1Command.C1Command cmdLogin;
        private C1.Win.C1Command.C1Command cmdTaskSwitch;
        private C1.Win.C1Command.C1Command cmdCPURate;
        private C1.Win.C1Command.C1Command cmdMemPoolView;
        private C1.Win.C1Command.C1Command cmdEquipDiagnose;
        private C1.Win.C1Command.C1Command cmdMsgAnalyse;
        private C1.Win.C1Command.C1Command cmdMemImport;
        private C1.Win.C1Command.C1Command cmdMemGraphicsView;
        private C1.Win.C1Command.C1Command cmdVarAnalyse;
        private C1.Win.C1Command.C1CommandControl c1CommandControl1;
        private C1.Win.C1Command.C1CommandControl c1CommandControl2;
        private C1.Win.C1Command.C1Command cmdDrawTaskRate;
        private C1.Win.C1Command.C1ContextMenu cmdOnlineAnalyse;
        private C1.Win.C1Command.C1CommandLink c1CommandLink17;
        private C1.Win.C1Command.C1CommandLink c1CommandLink16;
        private C1.Win.C1Command.C1CommandMenu c1CommandMenu1;
        private C1.Win.C1Command.C1CommandLink c1CommandLink4;
        private C1.Win.C1Command.C1CommandLink c1CommandLink5;
        private C1.Win.C1Command.C1CommandLink c1CommandLink2;
        private C1.Win.C1Command.C1CommandLink c1CommandLink6;
        private C1.Win.C1Command.C1Command c1Command2;
        private C1.Win.C1Command.C1CommandLink c1CommandLink7;
        private C1.Win.C1Command.C1Command c1Command3;
        private C1.Win.C1Command.C1CommandLink c1CommandLink9;
        private C1.Win.C1Command.C1Command c1Command4;
        private C1.Win.C1Command.C1Command c1Command1;
        private C1.Win.C1Command.C1Command cmdMemExport;
        private C1.Win.C1Command.C1CommandLink c1CommandLink3;
        private C1.Win.C1Command.C1CommandMenu cmdStatusSta;
        private C1.Win.C1Command.C1CommandLink c1CommandLink11;
        private C1.Win.C1Command.C1Command cmdDrawError;
        private C1.Win.C1Command.C1CommandLink c1CommandLink12;
        private C1.Win.C1Command.C1Command cmdDrawInfo;
        private C1.Win.C1Command.C1CommandLink c1CommandLink10;
        private C1.Win.C1Command.C1Command cmdDrawWarning;
        private C1.Win.C1Command.C1CommandLink c1CommandLink13;
        private C1.Win.C1Command.C1Command cmdDrawVarCount;
        private C1.Win.C1Command.C1CommandLink c1CommandLink14;
        private C1.Win.C1Command.C1Command cmdDrawDuration;
        private C1.Win.C1Command.C1Command cmdWatchMemory;
        private C1.Win.C1Command.C1Command cmdWatchVariable;
        private C1.Win.C1Command.C1DockingTab tabDetailPanel;
        private C1.Win.C1Command.C1CommandDock c1CommandDock3;
        private C1.Win.C1Command.C1DockingTab tabEquipmentView;
        private C1.Win.C1Command.C1DockingTabPage tbpDevices;
        private C1.Win.C1Command.C1CommandDock c1CommandDock2;
        private C1.Win.C1Command.C1DockingTab tabOutputForm;
        private C1.Win.C1Command.C1DockingTabPage tbpOutput;
        private C1.Win.C1Command.C1DockingTabPage tbpLogs;
    }
}