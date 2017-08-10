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
    public partial class A429FactorySetting : FixedSingleForm
    {

        private Device429 _device429;

        public A429FactorySetting()
        {
            InitializeComponent();
            this.button2.Click += ReadRev;//写入数据
            this.button1.Click += WriteRev;//读取数据
        }

        private void WriteRev(object sender, EventArgs e)
        {
            ushort addr = 0;
            bool b = ushort.TryParse(this.textBox1.Text, out addr);
            if (!b)
            {
                MessageBox.Show("请填写正确的地址信息！");
                return;
            }
            byte bytes = 0;
            b = byte.TryParse(this.textBox2.Text, out bytes);
            if (!b)
            {
                MessageBox.Show("请填写正确的数据信息！");
                return;
            }
            _device429.WriteDev(addr, bytes);
        }
        private void ReadRev(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                MessageBox.Show("读取数据时，地址不能为空！");
                return;
            }
            ushort addr = 0;
            bool b = ushort.TryParse(this.textBox1.Text, out addr);
            if (!b)
            {
                MessageBox.Show("请填写正确的地址信息！");
                return;
            }
            byte bytes = 0;
            _device429.ReadDev(addr, ref bytes);
            this.textBox2.Text = ((int)bytes).ToString();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //设置对应的设备
            string[] pathParts = this.Name.Split('_');
            _device429 = App.Instance.FlightBusManager.Bus429.GetSpecificItem(pathParts[pathParts.Length - 1]);
        }
    }
}
