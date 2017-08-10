namespace BinHong.FlightViewerUI
{
    partial class NewDevice
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
            this.btn_Ok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_BoardNo = new System.Windows.Forms.TextBox();
            this.textBox_ChannelCount = new System.Windows.Forms.TextBox();
            this.comboBox_BoardType = new System.Windows.Forms.ComboBox();
            this.comboBox_ChannelType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(240, 174);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 11;
            this.btn_Ok.Text = "确定";
            this.btn_Ok.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "名称：";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(88, 17);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(100, 21);
            this.textBox_Name.TabIndex = 14;
            // 
            // button1
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(345, 174);
            this.btn_Cancel.Name = "button1";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 15;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "板卡类型：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(256, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "板卡号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(244, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "通道类型：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "通道个数：";
            // 
            // textBox_BoardNo
            // 
            this.textBox_BoardNo.Location = new System.Drawing.Point(320, 17);
            this.textBox_BoardNo.Name = "textBox_BoardNo";
            this.textBox_BoardNo.Size = new System.Drawing.Size(100, 21);
            this.textBox_BoardNo.TabIndex = 24;
            // 
            // textBox_ChannelCount
            // 
            this.textBox_ChannelCount.Location = new System.Drawing.Point(88, 95);
            this.textBox_ChannelCount.Name = "textBox_ChannelCount";
            this.textBox_ChannelCount.Size = new System.Drawing.Size(100, 21);
            this.textBox_ChannelCount.TabIndex = 25;
            // 
            // comboBox_BoardType
            // 
            this.comboBox_BoardType.FormattingEnabled = true;
            this.comboBox_BoardType.Location = new System.Drawing.Point(88, 57);
            this.comboBox_BoardType.Name = "comboBox_BoardType";
            this.comboBox_BoardType.Size = new System.Drawing.Size(100, 20);
            this.comboBox_BoardType.TabIndex = 26;
            // 
            // comboBox_ChannelType
            // 
            this.comboBox_ChannelType.FormattingEnabled = true;
            this.comboBox_ChannelType.Location = new System.Drawing.Point(320, 57);
            this.comboBox_ChannelType.Name = "comboBox_ChannelType";
            this.comboBox_ChannelType.Size = new System.Drawing.Size(100, 20);
            this.comboBox_ChannelType.TabIndex = 27;
            // 
            // NewDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 231);
            this.Controls.Add(this.comboBox_ChannelType);
            this.Controls.Add(this.comboBox_BoardType);
            this.Controls.Add(this.textBox_ChannelCount);
            this.Controls.Add(this.textBox_BoardNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewDevice";
            this.Text = "NewDevice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_BoardNo;
        private System.Windows.Forms.TextBox textBox_ChannelCount;
        private System.Windows.Forms.ComboBox comboBox_BoardType;
        private System.Windows.Forms.ComboBox comboBox_ChannelType;
    }
}