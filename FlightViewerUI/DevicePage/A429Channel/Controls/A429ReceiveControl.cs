using BinHong.FlightViewerCore;
using BinHong.FlightViewerVM;
using System;
using System.Windows.Forms;
using UiControls.Tree;
using UiControls;
using System.Drawing;
using System.Threading;
using BinHong.Utilities;

namespace BinHong.FlightViewerUI
{
    public partial class A429ReceiveControl : FixedSingleForm
    {
        private Device429 _device429;

        public ChannelReceiveControlVm _chVm;

        private readonly CstTreeView _treeView1;

        //private readonly CstTreeView _treeView2;

        private readonly CstThread _thread = new CstThread();//新开一个线程来处理count

        private int SelectChannel;

        public A429ReceiveControl()
        {
            InitializeComponent();
            //设置接收treetree的初始样式
            _treeView1 = new CstTreeView();
            _treeView1.Dock = DockStyle.Fill;
            _treeView1.HideSelection = false;
            _treeView1.BackColor = VmColors.LightBlue;
            _treeView1.SelectedColor = VmColors.DarkBlue;
            groupBox1.Controls.Add(_treeView1);
            groupBox1.BackColor = VmColors.LightBlue;

            this.tabPage1.BackColor = VmColors.LightBlue;
            //this.tabPage2.BackColor = VmColors.LightBlue;
            //this.tabPage3.BackColor = VmColors.LightBlue;
            this.tabControl1.BackColor = VmColors.LightBlue;

            //设置过滤tree
            //_treeView2 = this.cstTreeView1;
            //_treeView2.CheckBoxes = true;

            this.radioButton2.Checked = true;
            //事件
            this.button_clear.Click += ClearData;//清除统计数
            this.button_Receive.Click += ReceiveData;//开始接收
            this.button_Stop.Click += StopReceive;//停止接收
            this.button_analyze.Click += AnalizeData;//快照分析
            //this.button3.Click += AllowBm;//允许BM获取所有消息
            //this.button4.Click += StopBm;//阻止BM获取所有消息
            //this.button5.Click += ApllyStrainer;//应用过滤器

            //this.comboBox1.SelectedValueChanged += ValueChange;
            //this.button1.Click += SetGatherParam;
            this.textBox1.TextChanged += TextChange;

        }
        private void TextChange(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.Text.Length - 1;
            textBox1.ScrollToCaret();
        }
        //private void SetGatherParam(object sender, EventArgs e)
        //{
        //    foreach (var item in _device429.ReceiveComponents)
        //    {
        //        Channe429Receive receive = (Channe429Receive)item;
        //        if (receive.ChannelID == SelectChannel - 1)
        //        {
        //            ChannelGatherParamA429Rx gatherParamA429 = new ChannelGatherParamA429Rx();
        //            if (this.checkBox1.Checked == false)
        //            {
        //                gatherParamA429.gather_enable = 1;
        //            }
        //            else
        //            {
        //                gatherParamA429.gather_enable = 0;
        //            }
        //            if (this.comboBox2.Text == "队列")
        //            {
        //                gatherParamA429.recv_mode = RecvModeA429.BHT_L1_A429_RECV_MODE_LIST;
        //            }
        //            else
        //            {
        //                gatherParamA429.recv_mode = RecvModeA429.BHT_L1_A429_RECV_MODE_SAMPLE;
        //            }
        //            if (!string.IsNullOrEmpty(this.textBox1.Text))
        //            {
        //                gatherParamA429.threshold_count = (ushort)Convert.ToInt32(this.textBox1.Text);
        //            }
        //            if (!string.IsNullOrEmpty(this.textBox2.Text))
        //            {
        //                gatherParamA429.threshold_time = (ushort)Convert.ToInt32(this.textBox2.Text);
        //            }
        //            uint ret = ((Channel429DriverRx)(receive.ChannelDriver)).ChannelGatherParam(ref gatherParamA429,
        //                ParamOptionA429.BHT_L1_PARAM_OPT_SET);
        //            if (ret != 0)
        //            {
        //                RunningLog.Record(string.Format("return value is {0} when invoke ChannelGatherParam", ret));
        //            }
        //        }

        //    }
        //}

        //private void ValueChange(object sender, EventArgs e)
        //{
        //    SelectChannel = Convert.ToInt32(this.comboBox1.Text.Split('_')[1]);
        //    foreach (var item in _device429.ReceiveComponents)
        //    {
        //        Channe429Receive receive = (Channe429Receive)item;
        //        if (receive.ChannelID == SelectChannel - 1)
        //        {
        //            //初始化Channel
        //            ChannelGatherParamA429Rx gatherParamA429 = new ChannelGatherParamA429Rx();
        //            uint ret = ((Channel429DriverRx)(receive.ChannelDriver)).ChannelGatherParam(ref gatherParamA429,
        //                ParamOptionA429.BHT_L1_PARAM_OPT_GET);
        //            if (ret != 0)
        //            {
        //                RunningLog.Record(string.Format("return value is {0} when invoke ChannelGatherParam", ret));
        //            }
        //            if (gatherParamA429.gather_enable == 0)
        //            {
        //                this.checkBox1.Checked = true;
        //            }
        //            else
        //            {
        //                this.checkBox1.Checked = false;
        //            }
        //            if (gatherParamA429.recv_mode == RecvModeA429.BHT_L1_A429_RECV_MODE_LIST)
        //            {
        //                this.comboBox2.Text = "队列";
        //            }
        //            else
        //            {
        //                this.comboBox2.Text = "采样";
        //            }
        //            if (!string.IsNullOrEmpty(gatherParamA429.threshold_count.ToString()))
        //            {
        //                this.textBox1.Text = gatherParamA429.threshold_count.ToString();
        //            }
        //            if (!string.IsNullOrEmpty(gatherParamA429.threshold_time.ToString()))
        //            {
        //                this.textBox2.Text = gatherParamA429.threshold_time.ToString();
        //            }
        //        }
        //    }
        //}
        ////过滤器.设置白名单与黑名单
        //private void ApllyStrainer(object sender, EventArgs e)
        //{
        //    int sdiOrSsh = 0;
        //    string sdiOrSshStr = string.Empty;
        //    ChannelFilterParamA429Rx channelFilterParamA429Rx = new ChannelFilterParamA429Rx();
        //    if (radioButton3.Checked == true)
        //    {
        //        channelFilterParamA429Rx.filterMode = FilterModeA429Rx.BHT_L1_A429_FILTER_MODE_WHITELIST;
        //    }
        //    else if (radioButton4.Checked == true)
        //    {
        //        channelFilterParamA429Rx.filterMode = FilterModeA429Rx.BHT_L1_A429_FILTER_MODE_BLACKLIST;
        //    }
        //    for (int i = 0; i < _treeView2.Nodes.Count; i++)
        //    {
        //        TreeNode treeNode = (TreeNode)_treeView2.Nodes[i];
        //        Channel429DriverRx channel429DriverRx = new Channel429DriverRx(_device429.DevID, (uint)i);
        //        foreach (var item in treeNode.Nodes)
        //        {
        //            int label;
        //            string labelStr;
        //            ushort bytelabel;
        //            TreeNode treeLabelNode = (TreeNode)item;
        //            bool isContainLabel = treeLabelNode.Text.Contains("label");
        //            if (isContainLabel)
        //            {
        //                label = Convert.ToInt32(treeLabelNode.Text.Split('_')[1]);
        //                labelStr = Convert.ToString(label, 2);
        //                bytelabel = Convert.ToUInt16(labelStr, 2);
        //                channelFilterParamA429Rx.label = bytelabel;
        //            }
        //            foreach (var item2 in treeLabelNode.Nodes)
        //            {
        //                TreeNode treessNode = (TreeNode)item2;
        //                bool isContainSdi = treessNode.Text.Contains("SDI");
        //                if (isContainSdi)
        //                {
        //                    sdiOrSsh = Convert.ToInt32(treessNode.Text.Split('_')[1]);
        //                    sdiOrSshStr = Convert.ToString(sdiOrSsh);
        //                    channelFilterParamA429Rx.Sdi = Convert.ToUInt16(sdiOrSshStr, 2);
        //                }
        //                foreach (var item3 in treessNode.Nodes)
        //                {
        //                    TreeNode ssmNode = (TreeNode)item3;
        //                    bool isContainSsm = treessNode.Text.Contains("SSM");
        //                    if (isContainSsm)
        //                    {
        //                        sdiOrSsh = Convert.ToInt32(treessNode.Text.Split('_')[1]);
        //                        sdiOrSshStr = Convert.ToString(sdiOrSsh);
        //                        channelFilterParamA429Rx.Ssm = Convert.ToUInt16(sdiOrSshStr, 2);
        //                    }
        //                    uint ret = channel429DriverRx.ChannelFilterCfgRxm(channelFilterParamA429Rx);
        //                    if (ret != 0)
        //                    {
        //                        RunningLog.Record(string.Format("return value is {0} when Aplly Strainer", ret));
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //不允许BM捕获数据
        //private void StopBm(object sender, EventArgs e)
        //{
        //    foreach (var item in _treeView2.Nodes)
        //    {
        //        TreeNode treeNode = (TreeNode)item;
        //        treeNode.Checked = false;
        //        foreach (var item1 in treeNode.Nodes)
        //        {
        //            TreeNode childTreeNode = (TreeNode)item1;
        //            childTreeNode.Checked = false;
        //            foreach (var item2 in childTreeNode.Nodes)
        //            {
        //                TreeNode sdiNode = (TreeNode)item2;
        //                sdiNode.Checked = false;
        //            }
        //        }
        //    }
        //}
        ////允许BM捕获数据
        //private void AllowBm(object sender, EventArgs e)
        //{
        //    foreach (var item in _treeView2.Nodes)
        //    {
        //        TreeNode treeNode = (TreeNode)item;
        //        treeNode.Checked = true;
        //        foreach (var item1 in treeNode.Nodes)
        //        {
        //            TreeNode childTreeNode = (TreeNode)item1;
        //            childTreeNode.Checked = true;
        //            foreach (var item2 in childTreeNode.Nodes)
        //            {
        //                TreeNode sdiNode = (TreeNode)item2;
        //                sdiNode.Checked = true;
        //            }
        //        }
        //    }
        //}
        //快照分析
        private void AnalizeData(object sender, EventArgs e)
        {
            DataAnalysis formSetting = new DataAnalysis();
            formSetting.Name = this.Name;
            formSetting._device429 = _device429;
            formSetting.ShowSingleAtCenterParent(this.FindForm());
        }

        private void StopReceive(object sender, EventArgs e)
        {
            _chVm.Stop();
        }

        private void ReceiveData(object sender, EventArgs e)
        {
            _chVm.Start();

        }
        //清除数据
        private void ClearData(object sender, EventArgs e)
        {
            foreach (var item in _device429.ReceiveComponents)
            {
                Channe429Receive sendChanel = (Channe429Receive)item;
                sendChanel.count = 0;
                sendChanel.errCount = 0;
                sendChanel.DeviceCount = 0;
                sendChanel.errDeviceCount = 0;
                for (int i = 0; i <= 377; i++)
                {
                    ReceiveLabel429 label = (ReceiveLabel429)sendChanel.GetSpecificItem(i);
                    if (label != null)
                    {
                        sendChanel.Delete(label);
                    }
                }
                Channel429DriverRx channel429DriverRx = new Channel429DriverRx(_device429.DevID, sendChanel.ChannelID);
                uint ret = channel429DriverRx.ChannelMibClearRx();
                if (ret != 0)
                {
                    RunningLog.Record(string.Format("return value is {0} when clear receive data", ret));
                }
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //设置对应的设备
            string[] pathParts = this.Name.Split('_');
            _device429 = App.Instance.FlightBusManager.Bus429.GetSpecificItem(pathParts[pathParts.Length - 1]);

            _chVm = new ChannelReceiveControlVm(_device429);
            //初始化数据
            InitializeData();
            //提示信息
            _chVm.MsgShow.Initialize(
                () => this.StatusStrip.ClearMsg(),
                info => this.StatusStrip.ShowErrorInfo(info),
                info => this.StatusStrip.ShowWarningInfo(info),
                info => this.StatusStrip.ShowInfo(info));

            _thread.ThreadEvent += OnProcess;//创建一个新的线程来处理count

            if (this.radioButton2.Checked == true)
            {
                _chVm.IsFileSaveAllow = true;
            }
            else
            {
                _chVm.IsFileSaveAllow = false;
            }
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitializeData()
        {
            //初始化tree1
            _treeView1.Nodes.Add(new SimpleTreeNode()
            {
                Text = "A429Device_" + _device429.Name,
                Name = _device429.Name,
                Path = _device429.Path
            });
            UpdateTreeView1();
            //UpdateTreeView2();
            _treeView1.DoubleClick += OnTreeViewAfterSelect;
            //_treeView2.AfterCheck += treeView2AfterCheck;
        }

        private void treeView2AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked == true)
            {
                if (e.Action != TreeViewAction.Unknown)
                {
                    cycleChild(e.Node, true);
                }
                if (e.Node.Parent != null)
                {
                    if (nextCheck(e.Node))
                    {
                        cycleParent(e.Node, true);
                    }
                    else
                    {
                        cycleParent(e.Node, false);
                    }
                }
            }
            if (e.Node.Checked == false)
            {
                if (e.Action != TreeViewAction.Unknown)
                {
                    cycleChild(e.Node, false);  //中间节点不选中则子节点全部不选中
                    //cycleParent(e.Node, false);       //父节点不选中
                }
            }
            return;
        }

        private void OnTreeViewAfterSelect(object sender, EventArgs e)
        {
            string selectedItemPath = ((SimpleTreeNode)_treeView1.SelectedNode).Path;
            _chVm.Select(selectedItemPath);
            bool isContained = selectedItemPath.Contains("Channel");
            if (isContained)
            {
                Channe429Receive chanel = _chVm.SelectChannelClick;
                var clickReceiveData = new ClickReceiveData(chanel);
                clickReceiveData.Name = this.Name;
                clickReceiveData.Text = this.Name.Replace(TreeLocalHost.PathString + "_", "") + "_ReceiveSettingg";
                clickReceiveData.ShowSingleAtCenterParent(this.FindForm());
            }
        }

        //private void UpdateTreeView2()
        //{
        //    for (int i = 0; i < 16; i++)
        //    {
        //        string childNode = "chanel_" + i.ToString();
        //        TreeNode treeNode = new TreeNode(childNode);
        //        treeNode.Checked = true;
        //        _treeView2.Nodes.Add(treeNode);
        //    }
        //    foreach (var item in _treeView2.Nodes)
        //    {
        //        int geWei = 0;
        //        int shiWei = 0;
        //        TreeNode treeNode = (TreeNode)item;
        //        for (int i = 0; i <= 377; i++)
        //        {
        //            geWei = i % 100 % 10;
        //            shiWei = i % 100 / 10;
        //            if (geWei > 7 || shiWei > 7)
        //            {
        //                continue;
        //            }
        //            string childNode = "label_" + i.ToString();
        //            TreeNode labelNode = new TreeNode(childNode);
        //            labelNode.Checked = true;
        //            for (int j = 0; j < 4; j++)
        //            {
        //                string SDILabel = string.Empty;
        //                switch (j)
        //                {
        //                    case 0: SDILabel = "SDI_00"; break;
        //                    case 1: SDILabel = "SDI_01"; break;
        //                    case 2: SDILabel = "SDI_10"; break;
        //                    case 3: SDILabel = "SDI_11"; break;
        //                }
        //                TreeNode SDINode = new TreeNode(SDILabel);
        //                for (int k = 0; k < 4; k++)
        //                {
        //                    string SSMLabel = string.Empty;
        //                    switch (k)
        //                    {
        //                        case 0: SSMLabel = "SSM_00"; break;
        //                        case 1: SSMLabel = "SSM_01"; break;
        //                        case 2: SSMLabel = "SSM_10"; break;
        //                        case 3: SSMLabel = "SSM_11"; break;
        //                    }
        //                    TreeNode SSMNode = new TreeNode(SSMLabel);
        //                    SSMNode.Checked = true;
        //                    SDINode.Nodes.Add(SSMNode);
        //                }
        //                SDINode.Checked = true;
        //                labelNode.Nodes.Add(SDINode);
        //            }
        //            treeNode.Nodes.Add(labelNode);

        //        }
        //    }
        //}
        public void UpdateTreeView1()
        {
            //设备里的信息的新信息添加到_treeView1中
            TreeNode node = _treeView1.TopNode;
            for (int i = 0; i < 16; i++)
            {
                Channe429Receive receiveCh = (Channe429Receive)_device429.GetSpecificItem(i);
                if (!receiveCh.Enabled)
                {
                    continue;
                }
                SimpleTreeNode chNode;
                if (!node.Nodes.ContainsKey(receiveCh.Name))
                {
                    chNode = new SimpleTreeNode()
                    {
                        Text = "ReceiveChannel" + (i + 1).ToString(),//receiveCh.Name,
                        Name = receiveCh.Name,
                        Path = receiveCh.Path
                    };
                    node.Nodes.Add(chNode);
                }
                else
                {
                    chNode = (SimpleTreeNode)node.Nodes[receiveCh.Name];
                }

                int count = 0;
                while (true)
                {
                    AbstractLabel label429 = null;
                    label429 = receiveCh.GetSpecificItem(count++);

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
                Channe429Receive channe429Receive = (Channe429Receive)_device429.GetSpecificItem(name);
                if (null == channe429Receive)
                {
                    topNode.Nodes.Remove(channelNode);
                    i--;
                }
                else
                {
                    for (int j = 0; j < channelNode.Nodes.Count; j++)
                    {
                        SimpleTreeNode labelNode = (SimpleTreeNode)channelNode.Nodes[j];
                        Label429 label = (Label429)channe429Receive.GetSpecificItem(labelNode.Name);
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
        #region check选择事件

        private bool nextCheck(TreeNode n)   //判断同级的节点是否全选
        {
            foreach (TreeNode tn in n.Parent.Nodes)
            {
                if (tn.Checked == false) return false;
            }
            return true;
        }

        private bool nextNotCheck(TreeNode n)  //判断同级的节点是否全不选
        {
            if (n.Checked == true)
            {
                return false;
            }
            if (n.NextNode == null)
            {
                return true;
            }

            return this.nextNotCheck(n.NextNode);
        }

        private void cycleChild(TreeNode tn, bool check)    //遍历节点下的子节点
        {
            if (tn.Nodes.Count != 0)
            {
                foreach (TreeNode child in tn.Nodes)
                {
                    child.Checked = check;
                    if (child.Nodes.Count != 0)
                    {
                        cycleChild(child, check);
                    }
                }
            }
            else
                return;
        }

        private void cycleParent(TreeNode tn, bool check)    //遍历节点上的父节点
        {
            if (tn.Parent != null)
            {
                if (nextCheck(tn))
                {
                    tn.Parent.Checked = true;
                }
                else
                {
                    tn.Parent.Checked = false;
                }
                cycleParent(tn.Parent, check);
            }
            return;
        }
        #endregion

        protected void OnProcess()
        {
            while (true)
            {
                int countRe = 0;
                int totalError = 0;
                uint totalDviceCount = 0;
                uint totalDeviceErrCount = 0;
                string result = string.Empty;
                foreach (var ch in _device429.ReceiveComponents)
                {
                    uint data = 0;
                    Channe429Receive chennel = (Channe429Receive)ch;
                    countRe += chennel.count;
                    totalError += chennel.errCount;
                    totalDviceCount += chennel.DeviceCount;
                    totalDeviceErrCount += chennel.errDeviceCount;
                    if (chennel.isSend == true)
                    {
                        if (chennel.rxpA429Result.data != 0)
                        {
                            data = chennel.rxpA429Result.data;
                        }
                        if (data != 0)
                        {
                            //对data进行解析
                            AnalysisLabel label = new AnalysisLabel();
                            label.ActualValue = (int)data;
                            ReceiveLabel429 receiveLabel = new ReceiveLabel429("label_" + label.Label.ToString());
                            receiveLabel.ActualValue = label.ActualValue;
                            ReceiveLabel429 receiveLabelExactHas = (ReceiveLabel429)chennel.GetSpecificItem("label_" + label.Label.ToString());
                            if (receiveLabelExactHas == null)
                            {
                                chennel.Add(receiveLabel);
                            }
                            result += DateTime.Now.ToString() + "\r\n" + "通道ID：" + (chennel.ChannelID + 1).ToString() + "label:\n";
                            if (checkBox1.Checked == false)
                            {
                                result += data.ToString("x2");
                            }
                            else
                            {
                                result += data.ToString("x8");
                            }
                            //这里需要保存log
                            textBox1.Text += result + "\r\n";
                        }
                    }


                }
                if (!this.IsDisposed)//如果窗口没有释放，那么就不停的刷新tree，释放了就不在使用刷新这个方法
                {
                    UpdateTreeView1();
                }

                label_totalCount.Text = countRe.ToString();
                label_totalError.Text = totalError.ToString();
                label4.Text = totalDviceCount.ToString();
                label6.Text = totalDeviceErrCount.ToString();
                Thread.Sleep(500);
            }
        }

        private void label_totalError_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
    //这是copy过来专门用于data解析的
    public class AnalysisLabel
    {
        public int ActualValue { get; set; }

        public int Label
        {
            get { return (byte)(ActualValue & 0xff); }
            set { ActualValue = ((ActualValue & (~0xff)) | (value & 0xff)); }
        }

        public int SDI
        {
            get { return (byte)((ActualValue >> 8) & 0x3); }
            set { ActualValue = ((ActualValue & (~(0x3 << 8))) | ((value & 0x3) << 8)); }
        }

        public int Data
        {
            get { return (byte)((ActualValue >> 10) & 0x7FFFF); }
            set { ActualValue = ((ActualValue & (~(0x7FFFF << 10))) | ((value & 0x7FFFF) << 10)); }
        }

        public int SymbolState
        {
            get { return (byte)((ActualValue >> 29) & 0x3); }
            set { ActualValue = ((ActualValue & (~(0x3 << 29))) | ((value & 0x3) << 29)); }
        }

        public int Parity
        {
            get { return (byte)((ActualValue >> 31) & 0x1); }
            set { ActualValue = ((ActualValue & (~(0x1 << 31))) | (value & 0x1) << 31); }
        }
    }
}
