
namespace BinHong.FlightViewerUI
{
    public class TreeChannels : AbstractTreeNode
    {
        public TreeChannels()
        {
            Name = "Channels";
            Text = "Channels";
            Path = PathString;
            Checked = true;
        }

        public static string PathString = "Channels";
    }

    public class TreeChannelNode : AbstractTreeNode
    {
        public TreeChannelNode(string channelNo)
        {
            ImageKey = TreeNodeImageName.ChannelImage;
            SelectedImageKey = TreeNodeImageName.ChannelImage;
            Text = "Channel" + channelNo;
            Name = channelNo;
        }
    }

    public class TreeReceiveNode : AbstractTreeNode
    {
        public TreeReceiveNode(string name)
        {
            ImageKey = TreeNodeImageName.ReceiveImage;
            SelectedImageKey = TreeNodeImageName.ReceiveImage;
            Text = name;
            Name = name;
        }
    }

    public class TreeSendNode : AbstractTreeNode
    {
        public TreeSendNode(string name)
        {
            ImageKey = TreeNodeImageName.SendImage;
            SelectedImageKey = TreeNodeImageName.SendImage;
            Text = name;
            Name = name;
        }
    }
}
