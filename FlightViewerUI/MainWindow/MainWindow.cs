using System.Drawing;
using System.Windows.Forms;
using BinHong.FlightViewerCore;
using BinHong.FlightViewerUI.Properties;
using BinHong.FlightViewerVM;
using BinHong.Utilities;

namespace BinHong.FlightViewerUI
{
    public partial class MainWindow : Form
    {
        private readonly UiControls.StatusStrip _bottomStatusStrip;
        public readonly PageManager PageManager;
        public readonly TopMenu TopMenu;
        public readonly TreeViewContainer treeViewContainer;
        public MainWindow()
        {
            InitializeComponent();
            //添加底部提示界面元素
            _bottomStatusStrip = new UiControls.StatusStrip();
            _bottomStatusStrip.Dock = DockStyle.Bottom;
            this.Controls.Add(_bottomStatusStrip);
            //添加顶部菜单栏
            TopMenu = new TopMenu();
            
            TopMenu.ShowA429Menu();
            TopMenu.MenuEnable(false);
            this.Controls.Add(TopMenu);
            //添加顶部工具栏 todo 暂时没有用
            Panel topToolStrip = new Panel();
            topToolStrip.Dock=DockStyle.Top;
            topToolStrip.Height = 10;
            topToolStrip.BackColor = VmColors.DarkBlue;
            this.Controls.Add(topToolStrip);

            //设置图标
            Icon = Resources.PXILOGO;
            BackColor = VmColors.LightBlue;
            //设置版本号
            Text = "FlightViewer Version " + App.Instance.ConfigManager.VersionNo;
            
            //设置左侧的设备树
            treeViewContainer = new TreeViewContainer(this);
            treeViewContainer.Dock=DockStyle.Fill;
            this.tbpDevices.Controls.Add(treeViewContainer);

            TopMenu.treeViewContainer = treeViewContainer;

            PageManager = new PageManager(this.tabDetailPanel);

            //添加运行日志界面
            AddRunningLog();
        }

        private void AddRunningLog()
        {
            //添加运行日志窗口
            var listBox = new RunningHistoryListBox
            {
                BackColor = tbpOutput.BackColor,
                DrawMode = DrawMode.OwnerDrawFixed,
                ItemHeight = 20,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                Font = new Font(Font.FontFamily, 10)
            };
            listBox.AfterAddNewItem += logItem =>
            {
                if (logItem.Level == LogLevel.Error)
                {
                    _bottomStatusStrip.ForeColor = VmColors.Error;
                }
                else if (logItem.Level == LogLevel.Warning)
                {
                    _bottomStatusStrip.ForeColor = VmColors.Warining;
                }
                else if (logItem.Level == LogLevel.Information)
                {
                    _bottomStatusStrip.ForeColor = VmColors.Info;
                }
                _bottomStatusStrip.Text = logItem.Text;
            };
            tbpOutput.Controls.Add(listBox);
        }

        private void MainWindow_Load(object sender, System.EventArgs e)
        {
        }

        private void tbpOutput_Click(object sender, System.EventArgs e)
        {

        }
    }
}
