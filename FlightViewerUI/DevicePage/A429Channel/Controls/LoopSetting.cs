using BinHong.FlightViewerCore;
using BinHong.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UiControls.Tree;

namespace BinHong.FlightViewerUI
{
    public partial class LoopSetting : FixedSingleForm
    {
        private readonly CstTreeView _treeView2;
        private Device429 _device429;

        public LoopSetting()
        {
            InitializeComponent();

            _treeView2 = this.cstTreeView1;
            _treeView2.CheckBoxes = true;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //设置对应的设备
            string[] pathParts = this.Name.Split('_');
            _device429 = App.Instance.FlightBusManager.Bus429.GetSpecificItem(pathParts[pathParts.Length - 1]);
            UpdateTree();
        }
        private void UpdateTree()
        {
            foreach (var item in _device429.SendComponents)
            {
                Channe429Send chaneSend = (Channe429Send)item;
                string nodeName = "chanel_" + chaneSend.ChannelID.ToString();
                TreeNode node = new TreeNode(nodeName);
                node.Checked = true;
                _treeView2.Nodes.Add(node);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var item in _treeView2.Nodes)
            {
                TreeNode node = (TreeNode)item;
                node.Checked = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var item in _treeView2.Nodes)
            {
                TreeNode node = (TreeNode)item;
                node.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in _treeView2.Nodes)
            {
                TreeNode node = (TreeNode)item;
                uint chanelId = Convert.ToUInt32(node.Text.Split('_')[1]);
                uint ret;
                Channe429Receive channe429ReNow = null;
                //Channe429Receive channe429Re
                foreach (var itemOfRev in _device429.ReceiveComponents)
                {
                    Channe429Receive channe429Re = (Channe429Receive)itemOfRev;
                    if (channe429Re.ChannelID == chanelId)
                    {
                        channe429ReNow = channe429Re;
                    }
                }
                if (node.Checked == true)
                {
                    Channel429DriverTx driverTx = new Channel429DriverTx(_device429.DevID, chanelId);
                    ret = driverTx.ChannelLoopTx(AbleStatusA429.BHT_L1_OPT_ENABLE);
                    channe429ReNow.isLoop = false;
                }
                else
                {
                    Channel429DriverTx driverTx = new Channel429DriverTx(_device429.DevID, chanelId);
                    ret = driverTx.ChannelLoopTx(AbleStatusA429.BHT_L1_OPT_DISABLE);
                    channe429ReNow.isLoop = true;
                }
                if (ret != 0)
                {
                    RunningLog.Record(string.Format("return value is {0} when invoke ChannelLoopTx", ret));
                }
            }
        }
    }
}
