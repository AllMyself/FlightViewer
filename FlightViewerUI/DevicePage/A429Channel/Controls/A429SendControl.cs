using System;
using System.Drawing;
using System.Windows.Forms;
using BinHong.FlightViewerCore;
using BinHong.FlightViewerVM;
using UiControls;
using UiControls.Tree;
using System.Threading;
using BinHong.Utilities;

namespace BinHong.FlightViewerUI
{
    public partial class A429SendControl : FixedSingleForm
    {
        private Device429 _device429;

        public ChannelSendControlVm ChVm;

        private readonly CstTreeView _treeView1;

        private readonly CstThread _thread = new CstThread();//新开一个线程来处理count

        public A429SendControl()
        {
            InitializeComponent();

            _treeView1 = new CstTreeView();
            _treeView1.Dock = DockStyle.Fill;
            _treeView1.HideSelection = false;
            _treeView1.BackColor = VmColors.LightBlue;
            _treeView1.SelectedColor = VmColors.DarkBlue;
            groupBox1.Controls.Add(_treeView1);
            groupBox1.BackColor = VmColors.LightBlue;

            btn_AllChannelStart.Click += OnAllChannelStart;//全部开始
            btn_AllChannelStop.Click += OnAllChannelStop;
            btn_AddLabel.Click += OnAddLabel;
            btn_DelAllLabel.Click += OnDelAllLabel;
            btn_DelLabel.Click += OnDelLabel;
            btn_EditLabel.Click += OnEditLabel;
            //btn_EditProjectValue.Click += OnEditProjectValue;
            button_clearData.Click += ClearData;
            button1.Click += SendPerid;
            button2.Click += FinishPerid;
            cycleSend_btn.Click += CycleSend;
            textBox_sendValue.TextChanged += TextChange;
        }
        //周期发送
        private void CycleSend(object sender, EventArgs e)
        {
            
        }
        private void TextChange(object sender, EventArgs e)
        {
            textBox_sendValue.SelectionStart = textBox_sendValue.Text.Length - 1;
            textBox_sendValue.ScrollToCaret();
        }

        private void FinishPerid(object sender, EventArgs e)
        {
            Channel429DriverTx driverTx = new Channel429DriverTx(_device429.DevID, ChVm._curSelectedChannel.ChannelID);
            uint ret = driverTx.ChannelSendTx(0, SendOptA429.BHT_L1_A429_OPT_PERIOD_SEND_STOP);
            if (ret != 0)
            {
                RunningLog.Record(string.Format("return value is {0} when invoke ChannelSendTx Set Data", ret));
            }
        }

        private void SendPerid(object sender, EventArgs e)
        {
            Channel429DriverTx driverTx = new Channel429DriverTx(_device429.DevID, ChVm._curSelectedChannel.ChannelID);
            uint ret = driverTx.ChannelSendTx(0, SendOptA429.BHT_L1_A429_OPT_PERIOD_SEND_START);
            if (ret != 0)
            {
                RunningLog.Record(string.Format("return value is {0} when invoke ChannelSendTx Set Data", ret));
            }
        }

        private void ClearData(object sender, EventArgs e)
        {
            foreach (var ch in _device429.SendComponents)
            {
                Channe429Send chennel = (Channe429Send)ch;
                chennel.labelCount = 0;
                chennel.errCount = 0;
                chennel.DeviceCount = 0;
                chennel.errDeviceCount = 0;
                this.textBox_sendValue.Text = "";
                Channel429DriverTx channel429DriverRx = new Channel429DriverTx(_device429.DevID, chennel.ChannelID);
                uint ret = channel429DriverRx.ChannelMibClearTx();
                if (ret != 0)
                {
                    RunningLog.Record(string.Format("return value is {0} when clear send data", ret));
                }
            }
        }
        //编辑或者新增label
        private void ShowEditSendLabel()
        {
            EditSendLabel sendLabel = new EditSendLabel(this);
            sendLabel.ShowSingleAtCenterParent(this);
        }

        private void OnAddLabel(object sender, EventArgs e)
        {
            ShowEditSendLabel();
        }

        private void OnEditProjectValue(object sender, EventArgs e)
        {

        }

        private void OnEditLabel(object sender, EventArgs e)
        {
            ShowEditSendLabel();
        }

        private void OnDelLabel(object sender, EventArgs e)
        {
            ChVm.DelLabel(ChVm.CurSelectedLabel);
            UpdateTreeView();
        }

        private void OnDelAllLabel(object sender, EventArgs e)
        {
            ChVm.DelAllLabel();
            UpdateTreeView();
        }

        private void OnChannelStop(object sender, EventArgs e)
        {
            ChVm.ChannelStop();
        }

        private void OnChannelStart(object sender, EventArgs e)
        {
            ChVm.ChannelStart();
        }

        private void OnAllChannelStop(object sender, EventArgs e)
        {
            ChVm.StopAll();
        }

        private void OnAllChannelStart(object sender, EventArgs e)
        {
            ChVm.StartAll();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //设置对应的设备
            string[] pathParts = this.Name.Split('_');
            _device429 = App.Instance.FlightBusManager.Bus429.GetSpecificItem(pathParts[pathParts.Length - 1]);

            ChVm = new ChannelSendControlVm(_device429);
            //初始化数据
            InitializeData();


            //提示信息
            ChVm.MsgShow.Initialize(
                () => this.StatusStrip.ClearMsg(),
                info => this.StatusStrip.ShowErrorInfo(info),
                info => this.StatusStrip.ShowWarningInfo(info),
                info => this.StatusStrip.ShowInfo(info));


            _thread.ThreadEvent += OnProcess;//创建一个新的线程来处理count

        }

        public void UpdateTreeView()
        {
            //设备里的信息的新信息添加到_treeView1中
            TreeNode node = _treeView1.TopNode;
            foreach (var ch in _device429.SendComponents)
            {
                Channe429Send sendCh = (Channe429Send)ch;
                if (!sendCh.Enabled)
                {
                    continue;
                }
                SimpleTreeNode chNode;
                if (!node.Nodes.ContainsKey(sendCh.Name))
                {
                    chNode = new SimpleTreeNode()
                    {
                        Text = "SendChannel" + (sendCh.ChannelID + 1).ToString(),//sendCh.Name,
                        Name = sendCh.Name,
                        Path = sendCh.Path
                    };
                    node.Nodes.Add(chNode);
                }
                else
                {
                    chNode = (SimpleTreeNode)node.Nodes[sendCh.Name];
                }

                int count = 0;
                while (true)
                {
                    AbstractLabel label429 = null;
                    label429 = sendCh.GetSpecificItem(count++);

                    if (label429 == null)
                    {
                        break;
                    }

                    if (!chNode.Nodes.ContainsKey(label429.Name))
                    {
                        SimpleTreeNode labelNode = new SimpleTreeNode()
                        {
                            Text = label429.Name,
                            Name = label429.Name,
                            Path = label429.Path
                        };
                        chNode.AddChildNode(labelNode);
                    }
                    if (!((Label429)label429).IsSelected)
                    {
                        chNode.Nodes[label429.Name].ForeColor = Color.Gray;
                    }
                    else
                    {
                        chNode.Nodes[label429.Name].ForeColor = Color.Black;
                    }
                }
            }

            //去除Treeview中多余的条目
            TreeNode topNode = _treeView1.TopNode;
            for (int i = 0; i < topNode.Nodes.Count; i++)
            {
                SimpleTreeNode channelNode = (SimpleTreeNode)topNode.Nodes[i];
                string name = channelNode.Name;
                Channe429Send channe429Send = (Channe429Send)_device429.GetSpecificItem(name);
                if (null == channe429Send)
                {
                    topNode.Nodes.Remove(channelNode);
                    i--;
                }
                else
                {
                    for (int j = 0; j < channelNode.Nodes.Count; j++)
                    {
                        SimpleTreeNode labelNode = (SimpleTreeNode)channelNode.Nodes[j];
                        Label429 label = (Label429)channe429Send.GetSpecificItem(labelNode.Name);
                        if (null == label)
                        {
                            channelNode.Nodes.Remove(labelNode);
                            j--;
                        }
                    }
                }
            }

            node.ExpandAll();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitializeData()
        {
            _treeView1.Nodes.Add(new SimpleTreeNode()
            {
                Text = "A429Device_" + _device429.Name,
                Name = _device429.Name,
                Path = _device429.Path
            });

            UpdateTreeView();
            //treeView1点击时
            _treeView1.AfterSelect += OnTreeViewAfterSelect;
        }

        private void OnTreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            string selectedItemPath = ((SimpleTreeNode)_treeView1.SelectedNode).Path;
            ChVm.Select(selectedItemPath);

            bool isContained = selectedItemPath.Contains("A429");
            gpBox_DeviceLayer.Enabled = isContained;

            isContained = selectedItemPath.Contains("Channel");
            gpBox_ChannelLayer.Enabled = isContained;

            isContained = selectedItemPath.Contains("Label");
            gpBox_LabelLayer.Enabled = isContained;

            isContained = selectedItemPath.Contains("Project");
            //gpBox_ProjectLayer.Enabled = isContained;
        }
        protected void OnProcess()
        {
            string selectedItemPath = ((SimpleTreeNode)_treeView1.SelectedNode).Text;
            while (true)
            {
                int count = 0;
                int errCount = 0;
                int devCount = 0;
                int devCountErr = 0;
                bool isContained429 = selectedItemPath.Contains("429");
                bool isContainedChanel = selectedItemPath.Contains("Channel");
                //string textValue = string.Empty;
                if (isContained429)
                {
                    foreach (var ch in _device429.SendComponents)
                    {
                        Channe429Send chennel = (Channe429Send)ch;
                        count += chennel.labelCount;
                        errCount += chennel.errCount;
                        devCount += (int)chennel.DeviceCount;
                        devCountErr += (int)chennel.errDeviceCount;
                        if (chennel.currentLabel != null && chennel.isSend != false)
                        {
                            if (isRadioBtn.Checked == false)
                            {
                                this.textBox_sendValue.Text += DateTime.Now.ToString() + ":\r\n" + "通道ID：" + (chennel.ChannelID + 1).ToString() + "\r\n" + chennel.currentLabel.ActualValue.ToString("x2") + "\r\n";
                            }
                            else
                            {
                                this.textBox_sendValue.Text += DateTime.Now.ToString() + ":\r\n" + "通道ID：" + (chennel.ChannelID + 1).ToString() + "\r\n" + chennel.currentLabel.ActualValue.ToString("x8") + "\r\n";
                            }
                        }
                    }
                }
                else if (isContainedChanel)
                {
                    Channe429Send chennel = ChVm._curSelectedChannel;
                    count = chennel.labelCount;
                    errCount = chennel.errCount;
                }
                this.label2.Text = count.ToString();
                this.label4.Text = errCount.ToString();
                this.label6.Text = devCount.ToString();
                this.label8.Text = devCountErr.ToString();
                //this.textBox_sendValue.Text = textValue;

                Thread.Sleep(500);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void A429SendControl_Load(object sender, EventArgs e)
        {

        }

        private void button_clearData_Click(object sender, EventArgs e)
        {

        }
    }
}
