namespace BinHong.FlightViewerUI
{
    partial class DevicePage
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_PlayBackSetting = new System.Windows.Forms.Button();
            this.btn_ReceiveSetting = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_SendSetting = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_SendControl = new System.Windows.Forms.Button();
            this.btn_ReceiveControl = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_PlayBackSetting);
            this.groupBox1.Controls.Add(this.btn_ReceiveSetting);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btn_SendSetting);
            this.groupBox1.Location = new System.Drawing.Point(20, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 78);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置";
            // 
            // btn_PlayBackSetting
            // 
            this.btn_PlayBackSetting.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_PlayBackSetting.Image = global::BinHong.FlightViewerUI.Properties.Resources.ChannelSetting;
            this.btn_PlayBackSetting.Location = new System.Drawing.Point(8, 49);
            this.btn_PlayBackSetting.Name = "btn_PlayBackSetting";
            this.btn_PlayBackSetting.Size = new System.Drawing.Size(93, 23);
            this.btn_PlayBackSetting.TabIndex = 4;
            this.btn_PlayBackSetting.Text = "数据管理";
            this.btn_PlayBackSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_PlayBackSetting.UseVisualStyleBackColor = false;
            // 
            // btn_ReceiveSetting
            // 
            this.btn_ReceiveSetting.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_ReceiveSetting.Image = global::BinHong.FlightViewerUI.Properties.Resources.ChannelSetting;
            this.btn_ReceiveSetting.Location = new System.Drawing.Point(8, 17);
            this.btn_ReceiveSetting.Name = "btn_ReceiveSetting";
            this.btn_ReceiveSetting.Size = new System.Drawing.Size(93, 23);
            this.btn_ReceiveSetting.TabIndex = 3;
            this.btn_ReceiveSetting.Text = "接收设置";
            this.btn_ReceiveSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_ReceiveSetting.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button2.Image = global::BinHong.FlightViewerUI.Properties.Resources.ChannelSetting;
            this.button2.Location = new System.Drawing.Point(255, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "过滤配置";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // btn_SendSetting
            // 
            this.btn_SendSetting.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_SendSetting.Image = global::BinHong.FlightViewerUI.Properties.Resources.ChannelSetting;
            this.btn_SendSetting.Location = new System.Drawing.Point(133, 17);
            this.btn_SendSetting.Name = "btn_SendSetting";
            this.btn_SendSetting.Size = new System.Drawing.Size(93, 23);
            this.btn_SendSetting.TabIndex = 1;
            this.btn_SendSetting.Text = "发送设置";
            this.btn_SendSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_SendSetting.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_SendControl);
            this.groupBox2.Controls.Add(this.btn_ReceiveControl);
            this.groupBox2.Location = new System.Drawing.Point(455, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(161, 78);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据收发";
            // 
            // btn_SendControl
            // 
            this.btn_SendControl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_SendControl.Image = global::BinHong.FlightViewerUI.Properties.Resources.ChannelSetting;
            this.btn_SendControl.Location = new System.Drawing.Point(6, 49);
            this.btn_SendControl.Name = "btn_SendControl";
            this.btn_SendControl.Size = new System.Drawing.Size(118, 23);
            this.btn_SendControl.TabIndex = 14;
            this.btn_SendControl.Text = "发送控制";
            this.btn_SendControl.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_SendControl.UseVisualStyleBackColor = false;
            // 
            // btn_ReceiveControl
            // 
            this.btn_ReceiveControl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_ReceiveControl.Image = global::BinHong.FlightViewerUI.Properties.Resources.ChannelSetting;
            this.btn_ReceiveControl.Location = new System.Drawing.Point(6, 17);
            this.btn_ReceiveControl.Name = "btn_ReceiveControl";
            this.btn_ReceiveControl.Size = new System.Drawing.Size(118, 23);
            this.btn_ReceiveControl.TabIndex = 5;
            this.btn_ReceiveControl.Text = "接收控制";
            this.btn_ReceiveControl.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_ReceiveControl.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Location = new System.Drawing.Point(28, 472);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "设备出厂设置";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // DevicePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "DevicePage";
            this.Size = new System.Drawing.Size(983, 601);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_PlayBackSetting;
        private System.Windows.Forms.Button btn_ReceiveSetting;
        private System.Windows.Forms.Button btn_SendSetting;
        private System.Windows.Forms.Button btn_ReceiveControl;
        private System.Windows.Forms.Button btn_SendControl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;




    }
}
