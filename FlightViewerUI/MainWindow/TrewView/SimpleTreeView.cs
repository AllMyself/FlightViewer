using System.Windows.Forms;
using BinHong.FlightViewerVM;
using UiControls.Tree;

namespace BinHong.FlightViewerUI
{
    public class SimpleTreeView : CstTreeView
    {
        public SimpleTreeView()
        {
            var imageList = new ImageList();
            imageList.Images.Add(TreeNodeImageName.BusImage, Properties.Resources.DeviceTree_gate);
            imageList.Images.Add(TreeNodeImageName.DeviceImage, Properties.Resources.Device);
            this.ImageList = imageList;
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            this.SelectedColor = VmColors.DarkBlue;
        }
    }
}
