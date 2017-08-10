using System.Windows.Forms;

namespace BinHong.FlightViewerUI
{
    public class A429ChanelTreeView : TreeView
    {
        public A429ChanelTreeView()
        {
            var imageList = new ImageList();
            imageList.Images.Add(TreeNodeImageName.ChannelImage, Properties.Resources.memory_view);
            imageList.Images.Add(TreeNodeImageName.ReceiveImage, Properties.Resources.MemImport);
            imageList.Images.Add(TreeNodeImageName.SendImage, Properties.Resources.MemOutport);
            this.ImageList = imageList;
        }
    }
}
