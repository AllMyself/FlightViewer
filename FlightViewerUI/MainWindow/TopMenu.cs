using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using BinHong.FlightViewerCore;
using BinHong.FlightViewerVM;
using BinHong.Utilities;
using UiControls;
using System.Text;
using BinHong.FlightViewerVM.Vm;

namespace BinHong.FlightViewerUI
{
    public class TopMenu : MenuStrip
    {
        private ToolStripMenuItem _fileMenu;
        private ToolStripMenuItem _helpMenu;
        public TreeViewContainer treeViewContainer;
        private readonly List<ToolStripMenuItem> _a429Items = new List<ToolStripMenuItem>();
        private readonly List<ToolStripMenuItem> _a1553Items = new List<ToolStripMenuItem>();

        public void ShowA429Menu()
        {
            foreach (var toolStripMenuItem in _a429Items)
            {
                toolStripMenuItem.Visible = true;
            }
            foreach (var toolStripMenuItem in _a1553Items)
            {
                toolStripMenuItem.Visible = false;
            }
        }

        public void ShowA1553Menu()
        {
            foreach (var toolStripMenuItem in _a429Items)
            {
                toolStripMenuItem.Visible = false;
            }
            foreach (var toolStripMenuItem in _a1553Items)
            {
                toolStripMenuItem.Visible = true;
            }
        }

        public void MenuEnable(bool enabled)
        {
            foreach (var toolStripMenuItem in _a429Items)
            {
                toolStripMenuItem.Enabled = enabled;
            }
            foreach (var toolStripMenuItem in _a1553Items)
            {
                toolStripMenuItem.Enabled = enabled;
            }
        }

        public TopMenu()
        {
            //file,help 菜单
            ToolStripMenuItem typeMenu = new ToolStripMenuItem("文件") { Name = "MenuFile" };
            ToolStripMenuItem operateMenu = new ToolStripMenuItem("新建") { Name = "MenuFile_NewDevice" };
            operateMenu.Click += MenuFile_NewDevice;
            typeMenu.DropDownItems.Add(operateMenu);
            operateMenu = new ToolStripMenuItem("打开工程") { Name = "MenuFile_Open" };
            operateMenu.Click += OnMenuFile_Open;
            typeMenu.DropDownItems.Add(operateMenu);
            operateMenu = new ToolStripMenuItem("保存工程") { Name = "SaveObject" };
            operateMenu.Click += SaveObject;
            typeMenu.DropDownItems.Add(operateMenu);
            _fileMenu = typeMenu;

            typeMenu = new ToolStripMenuItem("帮助") { Name = "MenuHelp" };
            operateMenu = new ToolStripMenuItem("帮助文档") { Name = "OnMenuHelp_HelpDoc", };
            operateMenu.Click += OnMenuHelp_HelpDoc;
            typeMenu.DropDownItems.Add(operateMenu);
            operateMenu = new ToolStripMenuItem("About") { Name = "MenuHelp_About" };
            operateMenu.Click += OnMenuHelp_About;
            typeMenu.DropDownItems.Add(operateMenu);
            _helpMenu = typeMenu;
            //A429的菜单
            typeMenu = new ToolStripMenuItem("编辑") { Name = "MenuEdit" };
            operateMenu = new ToolStripMenuItem("查找") { Name = "MenuEdit_Find" };
            operateMenu.Click += OnMenuEdit_Find;
            typeMenu.DropDownItems.Add(operateMenu);
            operateMenu = new ToolStripMenuItem("配置") { Name = "MenuEdit_Prefreces" };
            operateMenu.Click += OnMenuEdit_Prefreces;
            typeMenu.DropDownItems.Add(operateMenu);
            _a429Items.Add(typeMenu);

            typeMenu = new ToolStripMenuItem("视图") { Name = "MenuView" };
            operateMenu = new ToolStripMenuItem("BufferManager") { Name = "MenuView_BufferManager" };
            operateMenu.Click += OnMenuView_BufferManager;
            typeMenu.DropDownItems.Add(operateMenu);
            _a429Items.Add(typeMenu);

            typeMenu = new ToolStripMenuItem("工具") { Name = "MenuTools" };
            _a429Items.Add(typeMenu);



            //A1553的菜单
            typeMenu = new ToolStripMenuItem("1153编辑") { Name = "MenuEdit" };
            operateMenu = new ToolStripMenuItem("查找") { Name = "MenuEdit_Find" };
            operateMenu.Click += OnMenuEdit_Find;
            typeMenu.DropDownItems.Add(operateMenu);
            operateMenu = new ToolStripMenuItem("配置") { Name = "MenuEdit_Prefreces" };
            operateMenu.Click += OnMenuEdit_Prefreces;
            typeMenu.DropDownItems.Add(operateMenu);
            _a1553Items.Add(typeMenu);

            typeMenu = new ToolStripMenuItem("1153视图") { Name = "MenuView" };
            operateMenu = new ToolStripMenuItem("BufferManager") { Name = "MenuView_BufferManager" };
            operateMenu.Click += OnMenuView_BufferManager;
            typeMenu.DropDownItems.Add(operateMenu);
            _a1553Items.Add(typeMenu);

            typeMenu = new ToolStripMenuItem("1153工具") { Name = "MenuTools" };
            _a1553Items.Add(typeMenu);

            _a1553Items.Add(typeMenu);
        }

        private void SaveObject(object sender, EventArgs e)
        {
            if (treeViewContainer.flag == false)
            {
                MessageBox.Show("当前没有设备，无需保存工程！");
                return;
            }
            SaveFileDialog sFD = new SaveFileDialog();
            sFD.Title = "保存文件";
            sFD.ShowHelp = true;
            sFD.Filter = "工程文件(*.cfg)|*.cfg";//过滤格式
            sFD.FilterIndex = 1;                                    //格式索引
            sFD.RestoreDirectory = false;
            sFD.InitialDirectory = "c:\\";                          //默认路径
            if (sFD.ShowDialog() == DialogResult.OK)
            {
                //获得保存文件的路径
                string filePath = sFD.FileName;
                string xmlResult = string.Empty;
                DeviceForXml deviceForXml = new DeviceForXml();
                List<DeviceForXml> list = new List<DeviceForXml>();
                //遍历整个tree获取device
                foreach (var item in treeViewContainer.treeView.Nodes)
                {
                    TreeNode treeNode = (TreeNode)item;
                    foreach (var A429Dev in treeNode.Nodes)
                    {
                        TreeNode treeNodeA429Dev = (TreeNode)A429Dev;
                        foreach (var treeNodes in treeNodeA429Dev.Nodes)
                        {
                            TreeNode treeNodes429 = (TreeNode)treeNodes;
                            //string[] pathParts = treeNodes429.Name.Split('_');
                            Device429 _device429 = App.Instance.FlightBusManager.Bus429.GetSpecificItem(treeNodes429.Name);
                            TopMenuVM topMenuVM = new TopMenuVM(_device429);
                            topMenuVM.SaveSetting();
                            deviceForXml = topMenuVM._deviceInfo;
                            list.Add(deviceForXml);
                        }
                    }
                }
                xmlResult = SimpleSerializer.Serialize<List<DeviceForXml>>(list);
                //保存
                using (FileStream fsWrite = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    byte[] buffer = Encoding.Default.GetBytes(xmlResult);
                    fsWrite.Write(buffer, 0, buffer.Length);
                }
            }
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            this.BackColor = VmColors.DarkBlue;

            //设置菜单状态
            this.Items.Add(_fileMenu);
            foreach (var toolStripMenuItem in _a429Items)
            {
                this.Items.Add(toolStripMenuItem);
            }
            foreach (var toolStripMenuItem in _a1553Items)
            {
                this.Items.Add(toolStripMenuItem);
            }
            this.Items.Add(_helpMenu);
        }

        private void OnMenuHelp_HelpDoc(object sender, EventArgs e)
        {
            string filePath = App.Instance.ApplicationDirectory + "FlightViewer帮助文档.chm";
            if (!File.Exists(filePath))
            {
                RunningLog.Record(LogLevel.Error, "FlightViewer帮助文档缺失！");
            }
            else
            {
                System.Diagnostics.Process.Start(filePath);
            }
        }

        private void MenuFile_NewDevice(object sender, EventArgs e)
        {
            Form form = new FrmLoad();
            form.Text = "设备登录";
            form.ShowSingleAtCenterParent(this.FindForm());
        }

        private void OnMenuHelp_About(object sender, EventArgs e)
        {
            var frm = new AboutSoftware();
            frm.ShowSingleAtCenterParent(this.FindForm());
        }

        private void OnMenuView_BufferManager(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnMenuEdit_Prefreces(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnMenuEdit_Find(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnMenuFile_Open(object sender, EventArgs e)
        {
            OpenFileDialog sFD = new OpenFileDialog();
            sFD.Title = "保存文件";
            sFD.ShowHelp = true;
            sFD.Filter = "工程文件(*.cfg)|*.cfg";//过滤格式
            sFD.FilterIndex = 1;                                    //格式索引
            sFD.RestoreDirectory = false;
            sFD.InitialDirectory = "c:\\";                          //默认路径
            if (sFD.ShowDialog() == DialogResult.OK)
            {
                string filePath = sFD.FileName;
                List<DeviceForXml> list = ReadXml(filePath);
                foreach (DeviceForXml item in list)
                {
                    DeviceMessage devMsg = item.devMsg;
                    Device429 device = new Device429();
                    device.BoardNo = devMsg.BoardNo;
                    device.BoardType = devMsg.BoardType;
                    device.ChannelCount = devMsg.ChannelCount;
                    device.ChannelType = devMsg.ChannelType;
                    device.DevID = devMsg.DevID;
                    device.filterStr = devMsg.filter;
                    TopMenuVM topMenuVM = new TopMenuVM(device);
                    topMenuVM.OpenSetting();//将xml初始化设备及软件
                }
            }
        }
        private List<DeviceForXml> ReadXml(string path)
        {
            using (FileStream fsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            {                                
                StreamReader sReader = new StreamReader(fsWrite);
                string xmlStr = sReader.ReadToEnd();
                Type type = typeof(List<DeviceForXml>);
                return (List<DeviceForXml>)SimpleSerializer.Deserialize(type, xmlStr);
            }
        }
    }
}
