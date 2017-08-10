using System;
using C1.Win.C1Command;

namespace BinHong.FlightViewerUI
{
    /// <summary>
    /// 管理界面上的页面，映射TreeView和页面的关系
    /// </summary>
    public class PageManager
    {
        private readonly C1DockingTab _tabDetailPanel;
        public PageManager(C1DockingTab tabDetailPanel)
        {
            _tabDetailPanel = tabDetailPanel;
            _tabDetailPanel.SelectedIndexChanged += TabDetailPanel_SelectedIndexChanged;
        }

        private void TabDetailPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedDeviceChanged != null)
            {
                int index = _tabDetailPanel.SelectedIndex;
                if (index >= 0)
                {
                    string name = _tabDetailPanel.Controls[index].Name;
                    SelectedDeviceChanged(name);
                }
            }
        }

        public void ShowRightPage(string path)
        {
            C1DockingTabPage tabPage;
            if (!_tabDetailPanel.Controls.ContainsKey(path))
            {
                tabPage = new C1DockingTabPage();
                tabPage.Controls.Add(new DevicePage() { Name = path});
                tabPage.Name = path;
                _tabDetailPanel.Controls.Add(tabPage);
            }
            tabPage = (C1DockingTabPage)_tabDetailPanel.Controls[path];
            tabPage.Text = path.Replace(TreeLocalHost.PathString + "_", "");

            int index = _tabDetailPanel.Controls.IndexOfKey(path);
            _tabDetailPanel.SelectedIndex = index;
        }

        public bool CloseRightPage(string path)
        {
            if (!_tabDetailPanel.Controls.ContainsKey(path))
            {
                return false;
            }
            C1DockingTabPage tabPage = (C1DockingTabPage)_tabDetailPanel.Controls[path];
            _tabDetailPanel.Close(tabPage);
            return true;
        }

        public event Action<string> SelectedDeviceChanged;
    }
}
