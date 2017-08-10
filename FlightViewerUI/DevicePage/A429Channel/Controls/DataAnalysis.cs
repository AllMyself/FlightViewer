using BinHong.FlightViewerCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BinHong.FlightViewerUI
{
    public partial class DataAnalysis : FixedSingleForm
    {

        public Device429 _device429;

        List<int> channelIds { get; set; }//有可能是多个

        List<Channe429Receive> channelReceives;//有可能是多个channel

        private const string chanelStr = "anychanel";

        int scrollBarValue { get; set; }

        List<string> list;

        public DataAnalysis()
        {
            InitializeComponent();
            comboBox1.SelectedText = chanelStr;//设置第一个为默认项
            this.comboBox1.SelectedValueChanged += ComboxValueChange;
            this.vScrollBar1.ValueChanged += ValueChange;
            this.button1.Click += LoadData;
        }

        private void LoadData(object sender, EventArgs e)
        {
            list = new List<string>();
            foreach (var item in channelReceives)
            {
                Channe429Receive receive = item;
                List<string> listReturn = receive.DataAnalysis(scrollBarValue);
                list.AddRange(listReturn);
            }
            foreach (string item in list)
            {
                this.textBox1.Text += item + "\r\n";
            }
        }
        //combox事件
        private void ComboxValueChange(object sender, EventArgs e)
        {
            ComboxChange();
        }
        //滚动条事件
        private void ValueChange(object sender, EventArgs e)
        {
            scrollBarValue = this.vScrollBar1.Value;//获取当前滚动条的值
            list = new List<string>();
            foreach (var item in channelReceives)
            {
                Channe429Receive receive = item;
                List<string> listReturn = receive.DataAnalysis(scrollBarValue);
                list.AddRange(listReturn);
            }
            foreach (string item in list)
            {
                this.textBox1.Text += item + "\r\n";
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        public void ComboxChange()
        {
            if (!this.comboBox1.Text.Contains(chanelStr))
            {
                channelReceives = new List<Channe429Receive>();//清理所有元素
                uint channelId = Convert.ToUInt32(this.comboBox1.Text.Split('_')[1]) - 1;
                foreach (var item in _device429.ReceiveComponents)
                {
                    Channe429Receive channe429Receive = (Channe429Receive)item;
                    if (channe429Receive.ChannelID == channelId)
                    {
                        channelReceives.Add(channe429Receive);
                    }
                }

            }
            else
            {
                channelReceives = new List<Channe429Receive>();//清理所有元素
                foreach (var item in _device429.ReceiveComponents)
                {
                    Channe429Receive channe429Receive = (Channe429Receive)item;
                    channelReceives.Add(channe429Receive);
                }
            }
        }
    }
}
