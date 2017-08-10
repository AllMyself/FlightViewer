namespace BinHong.FlightViewerUI
{
    partial class EditSendLabel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditSendLabel));
            this.flgView = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1ToolBar1 = new C1.Win.C1Command.C1ToolBar();
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            this.cmdAddItem = new C1.Win.C1Command.C1Command();
            this.cmdDeleteItem = new C1.Win.C1Command.C1Command();
            this.c1CommandLink3 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink4 = new C1.Win.C1Command.C1CommandLink();
            this.btnOk = new C1.Win.C1Input.C1Button();
            this.c1CommandDock1 = new C1.Win.C1Command.C1CommandDock();
            ((System.ComponentModel.ISupportInitialize)(this.flgView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).BeginInit();
            this.c1CommandDock1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flgView
            // 
            this.flgView.AllowDelete = true;
            this.flgView.ColumnInfo = "1,1,0,0,0,100,Columns:0{Visible:False;}\t";
            this.flgView.Location = new System.Drawing.Point(0, 38);
            this.flgView.Name = "flgView";
            this.flgView.Rows.Count = 1;
            this.flgView.Rows.DefaultSize = 20;
            this.flgView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.flgView.ScrollOptions = C1.Win.C1FlexGrid.ScrollFlags.AlwaysVisible;
            this.flgView.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.flgView.Size = new System.Drawing.Size(634, 283);
            this.flgView.TabIndex = 14;
            // 
            // c1ToolBar1
            // 
            this.c1ToolBar1.AccessibleName = "Tool Bar";
            this.c1ToolBar1.AutoSize = false;
            this.c1ToolBar1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.c1ToolBar1.CommandHolder = this.c1CommandHolder1;
            this.c1ToolBar1.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
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
            this.c1CommandHolder1.Commands.Add(this.cmdAddItem);
            this.c1CommandHolder1.Commands.Add(this.cmdDeleteItem);
            this.c1CommandHolder1.Owner = this;
            // 
            // cmdAddItem
            // 
            this.cmdAddItem.Image = global::BinHong.FlightViewerUI.Properties.Resources.Add;
            this.cmdAddItem.Name = "cmdAddItem";
            this.cmdAddItem.ShortcutText = "";
            this.cmdAddItem.Text = "添加";
            // 
            // cmdDeleteItem
            // 
            this.cmdDeleteItem.Image = global::BinHong.FlightViewerUI.Properties.Resources.delete;
            this.cmdDeleteItem.Name = "cmdDeleteItem";
            this.cmdDeleteItem.ShortcutText = "";
            this.cmdDeleteItem.Text = "删除";
            // 
            // c1CommandLink3
            // 
            this.c1CommandLink3.Command = this.cmdAddItem;
            this.c1CommandLink3.Delimiter = true;
            this.c1CommandLink3.Text = "添加";
            // 
            // c1CommandLink4
            // 
            this.c1CommandLink4.Command = this.cmdDeleteItem;
            this.c1CommandLink4.SortOrder = 1;
            this.c1CommandLink4.Text = "删除";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(529, 327);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(93, 23);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "确认";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            // 
            // c1CommandDock1
            // 
            this.c1CommandDock1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.c1CommandDock1.Controls.Add(this.c1ToolBar1);
            this.c1CommandDock1.Id = 1;
            this.c1CommandDock1.Location = new System.Drawing.Point(0, 0);
            this.c1CommandDock1.Name = "c1CommandDock1";
            this.c1CommandDock1.Size = new System.Drawing.Size(642, 32);
            // 
            // EditSendLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 381);
            this.Controls.Add(this.flgView);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.c1CommandDock1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditSendLabel";
            this.Text = "EditSendLabel";
            ((System.ComponentModel.ISupportInitialize)(this.flgView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).EndInit();
            this.c1CommandDock1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid flgView;
        private C1.Win.C1Command.C1ToolBar c1ToolBar1;
        private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
        private C1.Win.C1Command.C1Command cmdAddItem;
        private C1.Win.C1Command.C1Command cmdDeleteItem;
        private C1.Win.C1Command.C1CommandLink c1CommandLink3;
        private C1.Win.C1Command.C1CommandLink c1CommandLink4;
        private C1.Win.C1Input.C1Button btnOk;
        private C1.Win.C1Command.C1CommandDock c1CommandDock1;


    }
}