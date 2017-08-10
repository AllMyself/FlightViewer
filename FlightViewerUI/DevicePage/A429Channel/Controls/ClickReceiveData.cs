using BinHong.FlightViewerCore;
using BinHong.FlightViewerVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BinHong.FlightViewerUI
{
    public partial class ClickReceiveData : FixedSingleForm
    {
        private readonly CstThread _thread = new CstThread();//新开一个线程来处理count
        public Channe429Receive _channe429Receive;
        TextBox textBox1;
        bool flag = true;
        public ClickReceiveData(Channe429Receive channe429Receive)
        {
            this._channe429Receive = channe429Receive;

            textBox1 = new TextBox();
            textBox1.BackColor = Color.LightYellow;
            textBox1.Dock = DockStyle.Fill;
            textBox1.Multiline = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            this.Controls.Add(textBox1);
            _thread.ThreadEvent += OnProcess;//创建一个新的线程来处理count

            this.FormClosed += ClearThread;
            this.textBox1.TextChanged += TextChange;//始终显示最下面的
        }
        private void TextChange(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.Text.Length - 1;
            textBox1.ScrollToCaret();
        }
        private void ClearThread(object sender, FormClosedEventArgs e)
        {
            _thread.ThreadEvent -= OnProcess;
            flag = false;
        }

        protected void OnProcess()
        {
            
            while (true)
            {
                string result = string.Empty;
                if (!flag)
                {
                    break;
                }
                if (_channe429Receive.rxpA429Result.data != 0 && _channe429Receive != null)
                {
                    result += DateTime.Now.ToString() + "\r\n" + "label:";
                    uint data = _channe429Receive.rxpA429Result.data;
                    result += data.ToString();
                    result += "\t" + "实际接收：" + _channe429Receive.count + "\t" + "硬件接收：" + _channe429Receive.DeviceCount;
                    result += "\n";
                    textBox1.Text += result + "\r\n";
                }
                Thread.Sleep(500);
            }
        }
    }
}
