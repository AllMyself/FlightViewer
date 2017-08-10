using System;
using System.Windows.Forms;
using BinHong.FlightViewerCore;
using BinHong.FlightViewerUI.Properties;
using BinHong.FlightViewerVM;

namespace BinHong.FlightViewerUI
{
    public partial class TreeViewContainer : UserControl
    {
        private SimpleTreeView _treeView;
        public SimpleTreeView treeView;
        private readonly MainWindow _mainWindow;

        private ToolStripButton _delDevice429Btn;
        private ToolStripButton _delDevice1553Btn;
        public bool flag = false;//判断设备是不是登陆了
        public TreeViewContainer(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _treeView = new SimpleTreeView();
            _treeView.BackColor = _mainWindow.BackColor;
            _treeView.Dock = DockStyle.Fill;
            _treeView.HideSelection = false;
            treeView = _treeView;
            this.Controls.Add(_treeView);

            //给设备树添加的工具栏
            ToolStrip toolStrip = new ToolStrip();
            toolStrip.Dock = DockStyle.Top;
            toolStrip.Height = 25;
            toolStrip.BackColor = VmColors.DarkBlue;
            this.Controls.Add(toolStrip);
            //在工具栏A429上添加删除按钮
            _delDevice429Btn = new ToolStripButton();
            _delDevice429Btn.ToolTipText = "删除设备";
            _delDevice429Btn.Image = Resources.DeviceDelete;
            _delDevice429Btn.Click += OnDelDevice;
            toolStrip.Items.Add(_delDevice429Btn);

            //在工具栏A1553上添加删除按钮
            _delDevice1553Btn = new ToolStripButton();
            _delDevice1553Btn.ToolTipText = "删除设备";
            _delDevice1553Btn.Image = Resources.delete;
            _delDevice1553Btn.Click += OnDelDevice;
            toolStrip.Items.Add(_delDevice1553Btn);
            _delDevice1553Btn.Visible = false;

            _treeView.Nodes.Add(new TreeLocalHost());
            _treeView.AfterSelect += OnTreeViewAfterSelect;
            _treeView.MouseClick += OnTreeViewClick;
           
            //tree
            VmManager.MainWindowVm.UpdateUi += OnUpdateDeviceTree;
            _mainWindow.PageManager.SelectedDeviceChanged += OnTabSelectedDeviceChanged;
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
            AbstractTreeNode node = (AbstractTreeNode)_treeView.TopNode;
            node = node.FindeTreeNodeByPath(node, path);
            if (node != null)
            {
                _treeView.SelectedNode = node;
            }
        }

        private void OnDelDevice(object sender, EventArgs e)
        {
            AbstractTreeNode node = _treeView.SelectedNode as AbstractTreeNode;
            if (node != null)
            {
                string path = node.Path;
                bool ret = _mainWindow.PageManager.CloseRightPage(path);
                int childCount = node.Nodes.Count;
                if (ret)
                {
                    _treeView.SelectedNode = node.PrevNode;
                    node.Remove();
                }
                else if (childCount == 0 && !(node is TreeLocalHost))
                {
                    node.Remove();
                }
                //登出设备
                string[] pathParts = path.Split('_');
                if (pathParts.Length > 1)
                {
                    AbstractBus bus = App.Instance.FlightBusManager.GetBus(pathParts[1]);
                    if (pathParts.Length > 2)
                    {
                        bus.Logout(pathParts[2]);
                    }
                }
            }
        }

        private void OnTreeViewAfterSelect(object sender, EventArgs e)
        {
            AbstractTreeNode node = _treeView.SelectedNode as AbstractTreeNode;
            if (node != null)
            {
                if (node.Path == TreeLocalHost.PathString)
                {
                    _mainWindow.TopMenu.MenuEnable(false);
                    return;
                }
                string path = node.Path; 
                if (path.Contains(BoardType.A429.ToString()))
                {
                    _delDevice429Btn.Visible = true;
                    _delDevice1553Btn.Visible = false;
                    _mainWindow.TopMenu.ShowA429Menu();
                    _mainWindow.TopMenu.MenuEnable(true);
                }
                else if(path.Contains(BoardType.A1553.ToString()))
                {
                    _delDevice429Btn.Visible = false;
                    _delDevice1553Btn.Visible = true;
                    _mainWindow.TopMenu.ShowA1553Menu();
                    _mainWindow.TopMenu.MenuEnable(true);
                }
                node.OnClick(_mainWindow.PageManager);
            }
        }

        /// <summary>
        /// 更新设备树。
        /// 设备树只有在登录的时候才更新
        /// </summary>
        private void OnUpdateDeviceTree(IDeviceInfo info)
        {
            flag = true;//这里判断设备是不是登陆了
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
