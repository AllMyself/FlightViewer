using BinHong.FlightViewerCore;
using BinHong.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using UiControls.Tree;

namespace BinHong.FlightViewerUI
{
    public partial class A429FilterControl : FixedSingleForm
    {
        private readonly CstTreeView _treeView;
        private Device429 _device429;
        public A429FilterControl()
        {
            InitializeComponent();
            _treeView = this.cstTreeView1;
            _treeView.CheckBoxes = true;
            this.button1.Click += SelectAll;
            this.button2.Click += SelectNo;
            this.button3.Click += Apply;
            _treeView.AfterCheck += treeView1_AfterSelect;
            this.FormClosed += ClosedFunc;
        }
        //窗口关闭后将这些东西写到xml里面
        private void ClosedFunc(object sender, FormClosedEventArgs e)
        {
            //Stopwatch sp = new Stopwatch();
            //sp.Start();
            //string filterStr = string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (var item in _treeView.Nodes)
            {
                TreeNode channelTree = (TreeNode)item;
                foreach (var item1 in channelTree.Nodes)
                {
                    TreeNode labelTree = (TreeNode)item1;
                    foreach (var item2 in labelTree.Nodes)
                    {
                        TreeNode sdiTree = (TreeNode)item2;
                        foreach (var item3 in sdiTree.Nodes)
                        {
                            TreeNode ssmTree = (TreeNode)item3;
                            if (ssmTree.Checked == true)
                            {
                                //filterStr += "1_";
                                sb.Append("1_");
                            }
                            else
                            {
                                //filterStr += "0_";
                                sb.Append("0_");
                            }
                        }
                    }
                }
            }
            //_device429.filterStr = ZipHelper.Compress(filterStr);
            _device429.filterStr = ZipHelper.Compress(sb.ToString());
            //sp.Stop();
            //MessageBox.Show(sp.ElapsedMilliseconds.ToString());
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!_treeView.SelectedNode.FullPath.ToLower().Contains("ssm"))
            {
                return;
            }
            ChannelFilterParamA429Rx channelFilterParamA429Rx = new ChannelFilterParamA429Rx();
            if (_treeView.SelectedNode.Checked == true)
            {
                //_treeView.SelectedNode.Checked = false;
                channelFilterParamA429Rx.filterMode = 0;
            }
            else if (_treeView.SelectedNode.Checked == false)
            {
                //_treeView.SelectedNode.Checked = true;
                channelFilterParamA429Rx.filterMode = 1;
            }
            TreeNode treeNode = (TreeNode)_treeView.SelectedNode;//SSM
            TreeNode SDINodes = treeNode.Parent;//sdi
            TreeNode LabelNodes = SDINodes.Parent;//label
            TreeNode ChannelNodes = LabelNodes.Parent;//channel
            int channelId = Convert.ToInt32(ChannelNodes.Text.Split('_')[1]);
            Channel429DriverRx channel429DriverRx = new Channel429DriverRx(_device429.DevID, (uint)(channelId - 1));
            string sdi = SDINodes.Text.Split('_')[1];
            channelFilterParamA429Rx.sdi = Convert.ToUInt16(sdi, 2);
            int label = Convert.ToInt32(LabelNodes.Text.Split('_')[1]);
            string labelStr = Convert.ToString(label, 2);//问题出在这
            ushort bytelabel = Convert.ToUInt16(labelStr, 2);
            channelFilterParamA429Rx.label = bytelabel;

            string ssm = treeNode.Text.Split('_')[1];
            channelFilterParamA429Rx.ssm = Convert.ToUInt16(ssm, 2);
            uint ret = channel429DriverRx.ChannelFilterCfgRxm(channelFilterParamA429Rx);
            if (ret != 0)
            {
                RunningLog.Record(string.Format("return value is {0} when Aplly Strainer", ret));
            }
        }
        private void Apply(object sender, EventArgs e)
        {
            int sdiOrSsh = 0;
            string sdiOrSshStr = string.Empty;
            ChannelFilterParamA429Rx channelFilterParamA429Rx = new ChannelFilterParamA429Rx();
            for (int i = 0; i < _treeView.Nodes.Count; i++)
            {
                TreeNode treeNode = (TreeNode)_treeView.Nodes[i];
                Channel429DriverRx channel429DriverRx = new Channel429DriverRx(_device429.DevID, (uint)(i - 1));
                foreach (var item in treeNode.Nodes)
                {
                    int label;
                    string labelStr;
                    ushort bytelabel;
                    TreeNode treeLabelNode = (TreeNode)item;
                    bool isContainLabel = treeLabelNode.Text.Contains("label");
                    if (isContainLabel)
                    {
                        label = Convert.ToInt32(treeLabelNode.Text.Split('_')[1]);
                        labelStr = Convert.ToString(label, 2);//问题出在这
                        bytelabel = Convert.ToUInt16(labelStr, 2);
                        channelFilterParamA429Rx.label = bytelabel;
                    }
                    foreach (var item2 in treeLabelNode.Nodes)
                    {
                        TreeNode treessNode = (TreeNode)item2;
                        bool isContainSdi = treessNode.Text.Contains("SDI");
                        if (isContainSdi)
                        {
                            sdiOrSsh = Convert.ToInt32(treessNode.Text.Split('_')[1]);
                            sdiOrSshStr = Convert.ToString(sdiOrSsh);
                            channelFilterParamA429Rx.sdi = Convert.ToUInt16(sdiOrSshStr, 2);
                        }
                        foreach (var item3 in treessNode.Nodes)
                        {
                            TreeNode ssmNode = (TreeNode)item3;
                            bool isContainSsm = ssmNode.Text.Contains("SSM");
                            if (isContainSsm)
                            {
                                sdiOrSsh = Convert.ToInt32(ssmNode.Text.Split('_')[1]);
                                sdiOrSshStr = Convert.ToString(sdiOrSsh);
                                channelFilterParamA429Rx.ssm = Convert.ToUInt16(sdiOrSshStr, 2);
                                if (ssmNode.Checked == true)
                                {
                                    channelFilterParamA429Rx.filterMode = 1;
                                }
                                else
                                {
                                    channelFilterParamA429Rx.filterMode = 0;
                                }
                            }
                            uint ret = channel429DriverRx.ChannelFilterCfgRxm(channelFilterParamA429Rx);
                            if (ret != 0)
                            {
                                RunningLog.Record(string.Format("return value is {0} when Aplly Strainer", ret));
                            }
                        }
                    }
                }
            }
        }

        private void SelectNo(object sender, EventArgs e)
        {
            foreach (var item in _treeView.Nodes)
            {
                TreeNode treeNode = (TreeNode)item;
                treeNode.Checked = false;
                foreach (var item1 in treeNode.Nodes)
                {
                    TreeNode childTreeNode = (TreeNode)item1;
                    childTreeNode.Checked = false;
                    foreach (var item2 in childTreeNode.Nodes)
                    {
                        TreeNode sdiNode = (TreeNode)item2;
                        sdiNode.Checked = false;
                        foreach (var item3 in sdiNode.Nodes)
                        {
                            TreeNode sdiNode1 = (TreeNode)item3;
                            sdiNode1.Checked = false;
                        }
                    }
                }
            }
        }

        private void SelectAll(object sender, EventArgs e)
        {
            foreach (var item in _treeView.Nodes)
            {
                TreeNode treeNode = (TreeNode)item;
                treeNode.Checked = true;
                foreach (var item1 in treeNode.Nodes)
                {
                    TreeNode childTreeNode = (TreeNode)item1;
                    childTreeNode.Checked = true;
                    foreach (var item2 in childTreeNode.Nodes)
                    {
                        TreeNode sdiNode = (TreeNode)item2;
                        sdiNode.Checked = true;
                        foreach (var item3 in sdiNode.Nodes)
                        {
                            TreeNode sdiNode1 = (TreeNode)item3;
                            sdiNode1.Checked = true;
                        }
                    }
                }
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string[] pathParts = this.Name.Split('_');
            _device429 = App.Instance.FlightBusManager.Bus429.GetSpecificItem(pathParts[pathParts.Length - 1]);
            updateTree();
            _treeView.AfterCheck += treeView2AfterCheck;
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
                    cycleParent(e.Node, false);       //父节点不选中
                }
            }
            return;
        }
        public void updateTree()
        {
            for (int i = 1; i <= 16; i++)
            {
                string childNode = "channel_" + i.ToString();
                TreeNode treeNode = new TreeNode(childNode);
                treeNode.Checked = true;
                _treeView.Nodes.Add(treeNode);
            }
            foreach (var item in _treeView.Nodes)
            {
                int geWei = 0;
                int shiWei = 0;
                TreeNode treeNode = (TreeNode)item;
                for (int i = 0; i <= 377; i++)
                {
                    geWei = i % 100 % 10;
                    shiWei = i % 100 / 10;
                    if (geWei > 7 || shiWei > 7)
                    {
                        continue;
                    }
                    string childNode = "label_" + i.ToString();
                    TreeNode labelNode = new TreeNode(childNode);
                    labelNode.Checked = true;
                    for (int j = 0; j < 4; j++)
                    {
                        string SDILabel = string.Empty;
                        switch (j)
                        {
                            case 0: SDILabel = "SDI_00"; break;
                            case 1: SDILabel = "SDI_01"; break;
                            case 2: SDILabel = "SDI_10"; break;
                            case 3: SDILabel = "SDI_11"; break;
                        }
                        TreeNode SDINode = new TreeNode(SDILabel);
                        for (int k = 0; k < 4; k++)
                        {
                            string SSMLabel = string.Empty;
                            switch (k)
                            {
                                case 0: SSMLabel = "SSM_00"; break;
                                case 1: SSMLabel = "SSM_01"; break;
                                case 2: SSMLabel = "SSM_10"; break;
                                case 3: SSMLabel = "SSM_11"; break;
                            }
                            TreeNode SSMNode = new TreeNode(SSMLabel);
                            SSMNode.Checked = true;
                            SDINode.Nodes.Add(SSMNode);
                        }
                        SDINode.Checked = true;
                        labelNode.Nodes.Add(SDINode);
                    }
                    treeNode.Nodes.Add(labelNode);

                }
            }
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
    }
}
