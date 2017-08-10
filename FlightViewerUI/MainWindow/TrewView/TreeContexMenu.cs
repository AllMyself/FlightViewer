using System;
using System.IO;
using System.Windows.Forms;
using BinHong.FlightViewerCore;
using BinHong.Utilities;

namespace BinHong.FlightViewerUI
{
    public class TreeDeviceContexMenu:ContextMenu
    {
        private readonly string _busName;
        private readonly string _deviceName;

        public TreeDeviceContexMenu(string busName,string deviceName)
        {
            _busName = busName;
            _deviceName = deviceName;
            this.MenuItems.Add("Import Bord Setting", OnImportBoardSetting);
            this.MenuItems.Add("Export Bord Setting", OnExportBoardSetting);
            this.MenuItems.Add("Reset Bord Setting", OnResetBoardSetting);
        }

        private void OnImportBoardSetting(object o, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = @"xml files(*.devcfg)|*.devcfg";
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                if (File.Exists(fileName))
                {
                    string content = File.ReadAllText(fileName);
                    AbstractDevice selectedDevice=StaticMethods.GetSeletecedDevice(_busName, _deviceName);
                    IParameter parameter = selectedDevice.Parameter;
                    IParameter para = SimpleSerializer.Deserialize(parameter.GetType(), content) as IParameter;
                    parameter.UpdateParameter(para);
                }
            }
        }

        private void OnExportBoardSetting(object o, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Filter = @"xml files(*.devcfg)|*.devcfg";
            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                AbstractDevice selectedDevice = StaticMethods.GetSeletecedDevice(_busName, _deviceName);
                IParameter parameter = selectedDevice.Parameter;
                string content = SimpleSerializer.Serialize(parameter);
                File.WriteAllText(fileName, content);
            }
        }

        private void OnResetBoardSetting(object o, EventArgs e)
        {
            AbstractDevice selectedDevice = StaticMethods.GetSeletecedDevice(_busName, _deviceName);
            selectedDevice.ResetParamter();
        }
    }
}
