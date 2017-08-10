namespace BinHong.FlightViewerUI
{
    partial class A429ReceiveControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(A429ReceiveControl));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox_statistics = new System.Windows.Forms.GroupBox();
            this.button_clear = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label_totalError = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_totalCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_control = new System.Windows.Forms.GroupBox();
            this.button_analyze = new System.Windows.Forms.Button();
            this.button_Stop = new System.Windows.Forms.Button();
            this.button_Receive = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox_statistics.SuspendLayout();
            this.groupBox_control.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 348);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "接收 Table";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox_statistics);
            this.tabPage1.Controls.Add(this.groupBox_control);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(324, 323);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "控制";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Location = new System.Drawing.Point(6, 250);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(312, 72);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "保存设置";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(110, 32);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(71, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "允许保存";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(17, 32);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(71, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "禁止保存";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // groupBox_statistics
            // 
            this.groupBox_statistics.Controls.Add(this.button_clear);
            this.groupBox_statistics.Controls.Add(this.label6);
            this.groupBox_statistics.Controls.Add(this.label_totalError);
            this.groupBox_statistics.Controls.Add(this.label5);
            this.groupBox_statistics.Controls.Add(this.label4);
            this.groupBox_statistics.Controls.Add(this.label2);
            this.groupBox_statistics.Controls.Add(this.label3);
            this.groupBox_statistics.Controls.Add(this.label_totalCount);
            this.groupBox_statistics.Controls.Add(this.label1);
            this.groupBox_statistics.Location = new System.Drawing.Point(6, 6);
            this.groupBox_statistics.Name = "groupBox_statistics";
            this.groupBox_statistics.Size = new System.Drawing.Size(312, 111);
            this.groupBox_statistics.TabIndex = 3;
            this.groupBox_statistics.TabStop = false;
            this.groupBox_statistics.Text = "信息统计";
            // 
            // button_clear
            // 
            this.button_clear.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button_clear.Location = new System.Drawing.Point(192, 40);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(87, 37);
            this.button_clear.TabIndex = 4;
            this.button_clear.Text = "清除统计数";
            this.button_clear.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(145, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "label3";
            this.label6.Click += new System.EventHandler(this.label_totalError_Click);
            // 
            // label_totalError
            // 
            this.label_totalError.AutoSize = true;
            this.label_totalError.Location = new System.Drawing.Point(145, 41);
            this.label_totalError.Name = "label_totalError";
            this.label_totalError.Size = new System.Drawing.Size(41, 12);
            this.label_totalError.TabIndex = 3;
            this.label_totalError.Text = "label3";
            this.label_totalError.Click += new System.EventHandler(this.label_totalError_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "Total device err：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(145, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Total Error Count：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Total Device Count：";
            // 
            // label_totalCount
            // 
            this.label_totalCount.AutoSize = true;
            this.label_totalCount.Location = new System.Drawing.Point(145, 19);
            this.label_totalCount.Name = "label_totalCount";
            this.label_totalCount.Size = new System.Drawing.Size(41, 12);
            this.label_totalCount.TabIndex = 1;
            this.label_totalCount.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Label Count：";
            // 
            // groupBox_control
            // 
            this.groupBox_control.Controls.Add(this.button_analyze);
            this.groupBox_control.Controls.Add(this.button_Stop);
            this.groupBox_control.Controls.Add(this.button_Receive);
            this.groupBox_control.Location = new System.Drawing.Point(6, 123);
            this.groupBox_control.Name = "groupBox_control";
            this.groupBox_control.Size = new System.Drawing.Size(312, 121);
            this.groupBox_control.TabIndex = 2;
            this.groupBox_control.TabStop = false;
            this.groupBox_control.Text = "接收控制";
            // 
            // button_analyze
            // 
            this.button_analyze.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button_analyze.Location = new System.Drawing.Point(17, 76);
            this.button_analyze.Name = "button_analyze";
            this.button_analyze.Size = new System.Drawing.Size(75, 30);
            this.button_analyze.TabIndex = 2;
            this.button_analyze.Text = "快照分析";
            this.button_analyze.UseVisualStyleBackColor = false;
            // 
            // button_Stop
            // 
            this.button_Stop.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button_Stop.Location = new System.Drawing.Point(192, 30);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(75, 30);
            this.button_Stop.TabIndex = 1;
            this.button_Stop.Text = "停止接收";
            this.button_Stop.UseVisualStyleBackColor = false;
            // 
            // button_Receive
            // 
            this.button_Receive.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button_Receive.Location = new System.Drawing.Point(17, 30);
            this.button_Receive.Name = "button_Receive";
            this.button_Receive.Size = new System.Drawing.Size(75, 30);
            this.button_Receive.TabIndex = 0;
            this.button_Receive.Text = "开始接收";
            this.button_Receive.UseVisualStyleBackColor = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(251, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(332, 348);
            this.tabControl1.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Location = new System.Drawing.Point(589, 17);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 343);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "接收数据监控";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(7, 321);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 16);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "16进制显示";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 17);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(188, 298);
            this.textBox1.TabIndex = 0;
            // 
            // A429ReceiveControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 394);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "A429ReceiveControl";
            this.Text = "A429ReceiveControl";
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox_statistics.ResumeLayout(false);
            this.groupBox_statistics.PerformLayout();
            this.groupBox_control.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox_statistics;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_totalError;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_totalCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox_control;
        private System.Windows.Forms.Button button_analyze;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.Button button_Receive;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}