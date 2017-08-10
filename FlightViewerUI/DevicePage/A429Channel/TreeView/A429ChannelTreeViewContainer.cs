using System;
using System.Drawing;
using System.Windows.Forms;
using BinHong.FlightViewerCore;
using BinHong.FlightViewerUI.Properties;
using BinHong.FlightViewerVM;

namespace BinHong.FlightViewerUI
{
    public partial class A429ChannelTreeViewContainer : UserControl
    {
        private A429ChanelTreeView _treeView;

        private readonly DevicePage _devicePage;

        public A429ChannelTreeViewContainer(DevicePage devicePage)
        {
            InitializeComponent();
            _devicePage = devicePage;
            string path = _devicePage.Name;
            string[] pathParts = path.Split('_');
            Bus429 bus429 = (Bus429)App.Instance.FlightBusManager.GetBus(pathParts[1]);
            Device429 device429 = bus429.GetSpecificItem(pathParts[2]);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _treeView = new A429ChanelTreeView();
            _treeView.Dock = DockStyle.Fill;
            _treeView.HideSelection = false;
            _treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
            _treeView.DrawNode +=OnTreeViewDrawNode;
            this.Controls.Add(_treeView);

            //给设备树添加工具栏
            ToolStrip topToolStrip = new ToolStrip();
            topToolStrip.Dock = DockStyle.Top;
            topToolStrip.Height = 25;
            topToolStrip.BackColor = VmColors.DarkBlue;
            this.Controls.Add(topToolStrip);
            //在工具栏上添加，删除按钮
            ToolStripButton addChannelBtn = new ToolStripButton();
            addChannelBtn.ToolTipText = "添加通道";
            addChannelBtn.Image = Resources.Add;
            addChannelBtn.Click += OnAddChannel;
            topToolStrip.Items.Add(addChannelBtn);
            ToolStripButton delChannelBtn = new ToolStripButton();
            delChannelBtn.ToolTipText = "删除通道";
            delChannelBtn.Image = Resources.delete;
            delChannelBtn.Click += OnDelChannel;
            topToolStrip.Items.Add(delChannelBtn);

            _treeView.Nodes.Add(new TreeChannels());
            _treeView.AfterSelect += OnTreeViewAfterSelect;
            _treeView.MouseClick += OnTreeViewClick;
          
            
        }

        private void OnTreeViewClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode tn = _treeView.GetNodeAt(e.X, e.Y);
                if (tn != null)
                {
                    _treeView.SelectedNode = tn;
                }
            }
        }

        private void OnTreeViewDrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            //e.DrawDefault = true; //我这里用默认颜色即可，只需要在TreeView失去焦点时选中节点仍然突显
            //return;

            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                //演示为绿底白字
                e.Graphics.FillRectangle(new SolidBrush(VmColors.DarkBlue), e.Node.Bounds);

                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null) nodeFont = ((TreeView)sender).Font;
                e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.White, Rectangle.Inflate(e.Bounds, 2, 0));
            }
            else
            {
                e.DrawDefault = true;
            }

            if ((e.State & TreeNodeStates.Focused) != 0)
            {
                using (Pen focusPen = new Pen(VmColors.DarkBlue))
                {
                    focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    Rectangle focusBounds = e.Node.Bounds;
                    focusBounds.Size = new Size(focusBounds.Width - 1,
                    focusBounds.Height - 1);
                    e.Graphics.DrawRectangle(focusPen, focusBounds);
                }
            }
        }

        private void OnTabSelectedDeviceChanged(string path)
        {
            if (_treeView.SelectedNode != null)
            {
                AbstractTreeNode node0 = _treeView.SelectedNode as AbstractTreeNode;
                if (node0 != null
                    && node0.Path == path)
                {
                    return;
                }
            }
            TreeNode node = _treeView.TopNode;
            node = FindeTreeNodeByPath(node, path);
            if (node != null)
            {
                _treeView.SelectedNode = node;
            }
        }

        private TreeNode FindeTreeNodeByPath(TreeNode node, string path)
        {
            if (node == null)
            {
                return null;
            }
            if (((AbstractTreeNode)node).Path == path)
            {
                return node;
            }
            foreach (TreeNode nodeItem in node.Nodes)
            {
                TreeNode findNode = FindeTreeNodeByPath(nodeItem, path);
                if (findNode != null)
                {
                    return findNode;
                }
            }

            return null;
        }

        private void OnAddChannel(object sender, EventArgs e)
        {
            AbstractTreeNode parentNode = (AbstractTreeNode)_treeView.TopNode;
            if (parentNode is TreeChannels)
            {
              
            }
        }

        private void OnDelChannel(object sender, EventArgs e)
        {
            AbstractTreeNode node = _treeView.SelectedNode as AbstractTreeNode;
            if (node != null)
            {
                string path=node.Path;
                //bool ret = _mainWindow.PageManager.CloseRightPage(path);
                //int childCount = node.Nodes.Count;
                //if (ret)
                //{
                //    _treeView.SelectedNode = node.PrevNode;
                //    node.Remove();
                //}
                //else if (childCount == 0 && !(node is TreeLocalHost))
                //{
                //    node.Remove();
                //}
            }
        }

        private void OnTreeViewAfterSelect(object sender, EventArgs e)
        {
            //SimpleTreeNode node = _treeView.SelectedNode as SimpleTreeNode;
            //if (node != null)
            //{
            //    node.OnClick(_mainWindow.PageManager);
            //}
        }

        /// <summary>
        /// 更新设备树。
        /// 设备树只有在登录的时候才更新
        /// </summary>
        private void OnUpdateDeviceTree(IDeviceInfo info)
        {
            //此处界面以boardType，boardNo，channelType，channelCount来标识唯一性。
            //core中bus，device等也应该包含这个几个属性，用它来标识唯一性
            AbstractTreeNode parentNode = (AbstractTreeNode)_treeView.TopNode;
            string boardType = info.BoardType.ToString();
            string boardNo = info.BoardNo.ToString();
            string channelType = info.ChannelType.ToString();
            string channelCount = info.ChannelCount.ToString();

            //Bus级
            if (!parentNode.Nodes.ContainsKey(boardType))
            {
                AbstractTreeNode node = new TreeBusNode(boardType);
                parentNode.AddChildNode(node);
            }
            //Device 级
            parentNode = (AbstractTreeNode)parentNode.Nodes[boardType];
            if (!parentNode.Nodes.ContainsKey(boardNo))
            {
                AbstractTreeNode node = new TreeDeviceNode(boardNo);
                parentNode.AddChildNode(node);
            }
        }
    }
}
